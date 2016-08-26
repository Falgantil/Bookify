using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ICSharpCode.SharpZipLib.Zip;

namespace EpubReader.Core
{
    public class Reader
    {
        private ZipFile zipFile;

        public Reader()
        {
        }

        public async Task Read(Stream stream)
        {
            this.zipFile = new ZipFile(stream);
        }

        private string GetFullPath()
        {
            var entry = this.zipFile.GetEntry("meta-inf/container.xml");
            var inputStream = this.zipFile.GetInputStream(entry);
            var doc = XDocument.Load(inputStream);
            var xContainer = doc.ElementsFirst("container");
            var xRootfiles = xContainer.ElementsFirst("rootfiles");
            var rootFile = xRootfiles.ElementsFirst("rootfile");
            var attrFullPath = rootFile.Attribute("full-path");
            return attrFullPath.Value;
        }
    }
}
