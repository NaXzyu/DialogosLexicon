using System;
using System.Collections.Generic;
using System.IO;

namespace DialogosLexicon
{
    public class ApplicationState
    {
        private const string k_AppStateFileName = "AppState";
        private const string k_AppStateFileExt = "bin";
        private Dictionary<string, object> _properties = [];

        public object this[string key]
        {
            get => _properties.TryGetValue(key, out var value) ? value : throw new KeyNotFoundException($"The given key '{key}' was not present in the dictionary.");
            set => _properties[key] = value ?? throw new ArgumentNullException(nameof(value), "Value cannot be null.");
        }

        private string GetAppStateFile()
        {
            return k_AppStateFileName + "." + k_AppStateFileExt;
        }

        public void SaveState()
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, GetAppStateFile());
            using var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None);
            using var writer = new BinaryWriter(stream);
            writer.Write(_properties.Count);

            foreach (var kvp in _properties)
            {
                writer.Write(kvp.Key);
                var value = kvp.Value?.ToString() ?? string.Empty;
                writer.Write(value);
            }
        }

        public void LoadState()
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, GetAppStateFile());
            if (!File.Exists(filePath))
                throw new FileNotFoundException("The state file does not exist.", filePath);

            using var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            using var reader = new BinaryReader(stream);
            int count = reader.ReadInt32();
            _properties.Clear();

            for (int i = 0; i < count; i++)
            {
                string key = reader.ReadString();
                string value = reader.ReadString();
                _properties[key] = value;
            }
        }
    }
}
