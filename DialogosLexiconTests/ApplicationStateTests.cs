using DialogosLexicon;

namespace DialogosLexiconTests
{
    [TestFixture]
    public class ApplicationStateTests
    {
        private ApplicationState _appState;
        private string _filePath;

        [SetUp]
        public void Setup()
        {
            _appState = new ApplicationState();
            _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "AppState.bin");
            // Ensure any existing state file is deleted before a test starts
            if (File.Exists(_filePath))
            {
                File.Delete(_filePath);
            }
        }

        [Test]
        public void SaveState_ShouldWriteToFile()
        {
            // Arrange
            _appState["testKey"] = "testValue";

            // Act
            _appState.SaveState();

            // Assert
            Assert.IsTrue(File.Exists(_filePath));
        }

        [Test]
        public void LoadState_ShouldLoadFromFile()
        {
            // Arrange
            _appState["testKey"] = "testValue";
            _appState.SaveState();

            var loadedAppState = new ApplicationState();

            // Act
            loadedAppState.LoadState();

            // Assert
            Assert.That(loadedAppState["testKey"], Is.EqualTo("testValue"));
        }

        [Test]
        public void LoadState_NoStateFile_ShouldThrowFileNotFoundException()
        {
            // Arrange
            var loadedAppState = new ApplicationState();

            // Act & Assert
            Assert.Throws<FileNotFoundException>(() => loadedAppState.LoadState());
        }

        [TearDown]
        public void TearDown()
        {
            // Clean up after each test
            if (File.Exists(_filePath))
            {
                File.Delete(_filePath);
            }
        }
    }
}
