using System;
using System.IO;
using System.Xml.Linq;
using Microsoft.Extensions.Configuration;
using XMindAPI.Logging;

namespace XMindAPI
{
    internal class XMindDocumentBuilder
    {
         private static readonly ILog Logger = LogProvider.GetCurrentClassLogger();
        public XMindDocumentBuilder()
        {
        }
        public XDocument CreateDefaultMetaFile()
        {
            XDocument metaFile = new XDocument();
            metaFile.Declaration = new XDeclaration("1.0", "UTF-8", "no");
            metaFile.Add(
                new XElement(
                    XNamespace.Get(XMindConfigurationCache.Configuration.XMindConfigCollection["metaNamespace"]) + "meta",
                    new XAttribute("version", "2.0")
                )
            );
            // var isLoggingOutputNeeded = XMindConfigurationCache.Configuration.XMindConfigCollection["FileOutPut:LogFileOutPutCondition"];
            // if(bool.TryParse(isLoggingOutputNeeded, out var isLoggingNeeded) && isLoggingNeeded)
            // {
            //     using (StringWriter sw = new StringWriter())
            //     {
            //         metaFile.Save(sw);
            //         Logger.Info($"metaFile: {Environment.NewLine} {sw.ToString()}");
            //     }
            // }
            return metaFile;
        }

        public XDocument CreateDefaultManifestFile()
        {
            var manifest = new XDocument();
            manifest.Declaration = new XDeclaration("1.0", "UTF-8", "no");
            var manifestNamespace = XNamespace.Get(XMindConfigurationCache.Configuration.XMindConfigCollection["manifestNamespace"]);
            var manifestFileEntryToken = manifestNamespace + "file-entry";
            XElement rootElement = new XElement(manifestNamespace + "manifest");
            rootElement.Add(
                new XElement(manifestFileEntryToken,
                    new XAttribute("full-path", "content.xml"),
                    new XAttribute("media-type", "text/xml")
                ));

            rootElement.Add(
                new XElement(manifestFileEntryToken,
                    new XAttribute("full-path", "META-INF/"),
                    new XAttribute("media-type", "")
                ));

            rootElement.Add(
                new XElement(manifestFileEntryToken,
                    new XAttribute("full-path", "META-INF/manifest.xml"),
                    new XAttribute("media-type", "text/xml")
                ));

            rootElement.Add(
                new XElement(manifestFileEntryToken,
                    new XAttribute("full-path", "Thumbnails/"),
                    new XAttribute("media-type", "")
                ));

            manifest.Add(rootElement);
            return manifest;
        }

        public XDocument CreateDefaultContentFile()
        {
            var content = new XDocument();
            IConfiguration xmindConfig = XMindConfigurationCache.Configuration.XMindConfigCollection;
            XNamespace ns2 = XNamespace.Get(xmindConfig["standardContentNamespaces:xsl"]);
            XNamespace ns3 = XNamespace.Get(xmindConfig["standardContentNamespaces:svg"]);
            XNamespace ns4 = XNamespace.Get(xmindConfig["standardContentNamespaces:xhtml"]);

            content.Add(new XElement(
                XNamespace.Get(xmindConfig["contentNamespace"]) + "xmap-content",
                new XAttribute(XNamespace.Xmlns + "fo", ns2),
                new XAttribute(XNamespace.Xmlns + "svg", ns3),
                new XAttribute(XNamespace.Xmlns + "xhtml", ns4),
                new XAttribute(XNamespace.Xmlns + "xlink", XNamespace.Get(xmindConfig["xlinkNamespace"])),
                new XAttribute("version", "2.0")
            ));
            return content;
        }
    }
}