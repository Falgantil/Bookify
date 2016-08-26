using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EpubReader.Core
{
    public static class XmlHelper
    {
        public static XElement ElementsFirst<T>(this T source, string localName)
       where T : XContainer
        {
            return source.Elements().First(e => e.Name.LocalName == localName);
        }
    }
}
