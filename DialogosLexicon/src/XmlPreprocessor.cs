using System.Xml.Linq;

namespace DialogosLexicon
{
    public class XmlPreprocessor
    {
        public static List<ReferenceMetadata> ExtractReferences(string xmlContent)
        {
            var references = new List<ReferenceMetadata>();
            var xDocument = XDocument.Parse(xmlContent);

            foreach (var element in xDocument.Descendants("reference"))
            {
                var idAttribute = element.Attribute("id");
                var titleElement = element.Element("title");
                var urlElement = element.Element("url");

                var reference = new ReferenceMetadata
                {
                    Id = idAttribute != null ? (int)idAttribute : -1,
                    Title = titleElement?.Value,
                    Url = urlElement?.Value,
                };

                references.Add(reference);
            }

            return references;
        }
    }
}
