namespace DialogosLexicon
{
    class Lexicon
    {
        public static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                string mode = args[0];
                // Process the mode or action
                Console.WriteLine($"Mode selected: {mode}");
            }
            else
            {
                Console.WriteLine("Please specify a mode.");
            }
        }
    }
}
