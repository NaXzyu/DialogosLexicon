using DialogosLexicon;
using System.Xml.Linq;

namespace DialogosLexiconTests
{
    [TestFixture]
    public class XmlPreprocessorTests
    {
        private XElement _xmlContent;

        [SetUp]
        public void Setup()
        {
            _xmlContent = XElement.Parse(@"
                <references>
                    <reference id='1'>
                        <title>Article One</title>
                        <url>http://example.com/article1</url>
                    </reference>
                    <reference id='2'>
                        <title>Article Two</title>
                        <url>http://example.com/article2</url>
                    </reference>
                </references>");
        }

        [Test]
        public void ExtractReferences_ShouldReturnCorrectNumberOfReferences()
        {
            // Arrange
            var expectedCount = 2;

            // Act
            var references = XmlPreprocessor.ExtractReferences(_xmlContent.ToString());

            // Assert
            Assert.That(references, Is.Not.Null);
            Assert.That(references, Has.Count.EqualTo(expectedCount));
        }

        [Test]
        public void GetXPath_ShouldReturnCorrectXPath()
        {
            // Arrange
            var element = _xmlContent.Descendants("title").First();

            // Act
            var xpath = XmlPreprocessor.GetXPath(element);

            // Assert
            Assert.That(xpath, Is.EqualTo("/references[1]/reference[1]/title[1]"));
        }
    }
}
