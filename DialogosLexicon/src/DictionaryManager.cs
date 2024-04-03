namespace DialogosLexicon
{
    public class DictionaryManager
    {
        public Dictionary<string, DictionaryEntry> WordDictionary = [];
        private const string k_DictionaryName = "dictionary";
        private const string k_DictionaryExt = "bin";
        private const string k_DateSuffixFormat = "yyMMdd";

        public void SaveDictionaryToBinaryFile(string directoryPath)
        {
            try
            {
                string fileName = k_DictionaryName + "." + k_DictionaryExt;
                string filePath = Path.Combine(directoryPath, fileName);

                Console.WriteLine($"Saving dictionary to binary file: {filePath}");

                RenameExistingFile(directoryPath, filePath);

                using FileStream stream = new(filePath, FileMode.Create, FileAccess.Write, FileShare.None);
                using BinaryWriter writer = new(stream);
                writer.Write(WordDictionary.Count);

                Console.WriteLine($"Writing {WordDictionary.Count} entries to the file.");

                WriteDictionaryEntries(writer);

                Console.WriteLine("Dictionary saved successfully.");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"An error occurred while saving the dictionary: {ex.Message}");
            }
        }

        private static void RenameExistingFile(string directoryPath, string filePath)
        {
            if (File.Exists(filePath))
            {
                Console.WriteLine($"Renaming file: {filePath}");
                string dateSuffix = DateTime.Now.ToString(k_DateSuffixFormat);
                string newFileName = $"{k_DictionaryName}.{dateSuffix}.{k_DictionaryExt}";
                string newFilePath = Path.Combine(directoryPath, newFileName);
                File.Move(filePath, newFilePath);
                Console.WriteLine($"Existing file renamed to: {newFileName}");
            }
        }

        private void WriteDictionaryEntries(BinaryWriter writer)
        {
            foreach (var entry in WordDictionary.Values)
            {
                writer.Write(entry.Id);
                writer.Write(entry.Word);
                writer.Write(entry.Frequency);
                writer.Write(entry.Version);
                writer.Write((int)entry.LifecyclePhase);
                Console.WriteLine($"Written word: {entry.Word}:{entry.Id}");
            }
        }
    }
}
