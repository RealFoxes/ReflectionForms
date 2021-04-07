using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionForms.EntitiesForms
{
	[Serializable]
	class MethodGetEntitiesIsNotImplementedException : Exception
	{
		public MethodGetEntitiesIsNotImplementedException() { }
	}
	class ContorlHasNoChildren : Exception
	{
		public ContorlHasNoChildren() { }
	}

}
