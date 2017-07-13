using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictConverter
{
	static class XmlUtils
	{
		public static XElement SafeElement(this XElement element, XName name)
		{
			return element.Element(name) ?? new XElement(name);
		}

		public static XAttribute SafeAttribute(this XElement element, XName name)
		{
			return element.Attribute(name) ?? new XAttribute(name, string.Empty);
		}
	}
}
