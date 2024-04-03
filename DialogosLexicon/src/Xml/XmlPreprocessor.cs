using System.Xml.Linq;

namespace DialogosLexicon
{
    public class XmlPreprocessor
    {
        public static List<XmlReferenceMetadata> ExtractReferences(string xmlContent)
        {
            var references = new List<XmlReferenceMetadata>();
            var xDocument = XDocument.Parse(xmlContent);

            foreach (var element in xDocument.Descendants("reference"))
            {
                var idAttribute = element.Attribute("id");
                var titleElement = element.Element("title");
                var urlElement = element.Element("url");
                var xpath = GetXPath(element);

                var reference = new XmlReferenceMetadata
                {
                    Id = idAttribute != null ? (int)idAttribute : -1,
                    Title = titleElement?.Value,
                    Url = urlElement?.Value,
                    XPath = xpath
                };

                references.Add(reference);
            }

            return references;
        }

        public static string GetXPath(XElement element)
        {
            var ancestors =
                from e in element.Ancestors()
                select $"{e.Name.LocalName}[{e.ElementsBeforeSelf().Count() + 1}]";

            var xpath = string.Join("/", ancestors.Reverse().ToArray());
            xpath = $"/{xpath}/{element.Name.LocalName}[{element.ElementsBeforeSelf().Count() + 1}]";

            return xpath;
        }
    }

}
