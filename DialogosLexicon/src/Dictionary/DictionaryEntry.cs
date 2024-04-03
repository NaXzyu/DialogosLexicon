namespace DialogosLexicon
{
    public enum WordLifecyclePhase
    {
        Unknown,
        Used,
        Duplicate,
        Deprecated,
        Retired
    }

    public struct DictionaryEntry
    {
        public int Id { get; }
        public string Word { get; }
        public int Frequency { get; set; }
        public int Version { get; }
        public WordLifecyclePhase LifecyclePhase { get; set; }

        public DictionaryEntry(int id, string word, int frequency, int version, WordLifecyclePhase lifecyclePhase)
        {
            Id = id;
            Word = word;
            Frequency = frequency;
            Version = version;
            LifecyclePhase = lifecyclePhase;
        }
    }
}
