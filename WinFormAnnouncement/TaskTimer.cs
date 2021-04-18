using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace WinFormAnnouncement
{
    public interface ITaskTimer : IDisposable, IEnumerable<Task>
    {
    }
    public class TaskTimer
    {
        private readonly TimeSpan _period;
        private CancellationToken _cancellationToken = CancellationToken.None;

        public TaskTimer(int periodMs)
        {
            _period = TimeSpan.FromMilliseconds(periodMs);
        }

        public TaskTimer(TimeSpan period)
        {
            _period = period;
        }

        public TaskTimer CancelWith(CancellationToken token)
        {
            _cancellationToken = token;
            return this;
        }
        public ITaskTimer Start(int delayMs = 0)
        {
            return Start(TimeSpan.FromMilliseconds(delayMs));
        }
        public ITaskTimer Start(TimeSpan delay)
        {
            Func<Action, IDisposable> createTimer = callback => new Timer(state => callback(), null, delay, _period);
            return StartOnTimer(createTimer);
        }
        public ITaskTimer StartAt(DateTime when)
        {
            var delay = when - DateTime.UtcNow;
            return Start(delay);
        }
        public ITaskTimer StartOnTimer(Func<Action, IDisposable> createTimer)
        {
            return new DisposableEnumerable(new TaskTimerImpl(createTimer, _cancellationToken));
        }

        private class DisposableEnumerable : ITaskTimer
        {
            private readonly TaskTimerImpl _impl;
            private IEnumerable<Task> _tasks;

            public DisposableEnumerable(TaskTimerImpl impl)
            {
                _impl = impl;
            }

            public IEnumerator<Task> GetEnumerator()
            {
                if (_tasks != null) throw new InvalidOperationException("Timer cannot be enumerated twice");
                _tasks = _impl.GetTasks();
                return _tasks.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public void Dispose()
            {
                _impl.Dispose();
            }
        }

        private class TaskTimerImpl : IDisposable
        {
            private readonly Queue<Record> _pendingTasks = new Queue<Record>();
            private readonly IDisposable _timer;
            private long _ticks;
            private long _nTasks;
            private bool _isCanceled;

            private struct Void
            {
                public static readonly Void Value = new Void();
            }

            private struct Record
            {
                public long Tick;
                public TaskCompletionSource<Void> Promise;
            }

            public TaskTimerImpl(Func<Action, IDisposable> createTimer, CancellationToken token)
            {
                _timer = createTimer(OnTimer);
                token.Register(Cancel);
            }

            private void OnTimer()
            {
                var toComplete = new List<TaskCompletionSource<Void>>();

                lock (_pendingTasks)
                {
                    ++_ticks;
                    while (_pendingTasks.Count > 0)
                    {
                        var next = _pendingTasks.Peek();
                        if (next.Tick <= _ticks)
                        {
                            toComplete.Add(_pendingTasks.Dequeue().Promise);
                        }
                        else
                        {
                            break;
                        }
                    }
                }

                foreach (var promise in toComplete) promise.SetResult(default(Void));
            }

            public IEnumerable<Task> GetTasks()
            {
                while (true)
                {
                    yield return NextTask();
                }
                // ReSharper disable once IteratorNeverReturns
            }

            public void Dispose()
            {
                _timer.Dispose();
            }

            private Task NextTask()
            {
                lock (_pendingTasks)
                {
                    var tick = ++_nTasks;
                    bool isLate = tick <= _ticks;

                    if (_isCanceled) return CreateCanceledTask();

                    if (isLate)
                    {
                        return Task.FromResult(Void.Value);
                    }
                    else
                    {
                        var promise = new TaskCompletionSource<Void>();
                        _pendingTasks.Enqueue(new Record {Tick = tick, Promise = promise});
                        return promise.Task;
                    }
                }
            }

            private void Cancel()
            {
                lock (_pendingTasks)
                {
                    _isCanceled = true;
                    foreach (var record in _pendingTasks)
                    {
                        record.Promise.TrySetCanceled();
                    }
                }
            }

            private static Task CreateCanceledTask()
            {
                var promise = new TaskCompletionSource<Void>();
                promise.TrySetCanceled();
                return promise.Task;
            }
        }
    }
}
