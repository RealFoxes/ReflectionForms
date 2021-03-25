using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionForms.EntitiesForms
{
	abstract public class ReflFormAtt : Attribute
	{

	}
	public class ReflFormName : ReflFormAtt
	{
		public string Name { get; set; }

		public ReflFormName(string Name) // Возмонжо передавать dictionary с ключем локализации и значением для этого языка 
		{
			this.Name = Name;		
		}
	}
	public class ReflFormNotVisible : ReflFormAtt
	{
	}
	public class ReflFormRef : ReflFormAtt
	{
		public string PropertyToShow { get; set; }
		public ReflFormRef(string PropertyToShow)
		{

		}
	}
}
