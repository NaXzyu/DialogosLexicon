using DialogosLexicon;

namespace DialogosLexiconTests
{
    [TestFixture]
    public class XmlPreprocessorTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ExtractReferences_ShouldReturnCorrectNumberOfReferences()
        {
            // Arrange
            var xmlContent = @"
                <references>
                    <reference id='1'>
                        <title>Article One</title>
                        <url>http://example.com/article1</url>
                    </reference>
                    <reference id='2'>
                        <title>Article Two</title>
                        <url>http://example.com/article2</url>
                    </reference>
                </references>";
            var expectedCount = 2;

            // Act
            var references = XmlPreprocessor.ExtractReferences(xmlContent);

            // Assert
            Assert.That(references, Is.Not.Null);
            Assert.That(references, Has.Count.EqualTo(expectedCount));
        }
    }
}
