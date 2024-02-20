using NBench;
using System.Diagnostics;
using System.Runtime.Intrinsics.X86;
using System.Text.RegularExpressions;

namespace DocuTR
{
    public class TextExtractionBenchmark
    {
        private const string TextFilePath = "example.txt";
        private const string SearchString = "consectetur";

        private List<TextFragment> _textFragments;
        private string? _concatenatedText;

        [PerfSetup]
        public void Setup(BenchmarkContext context)
        {
            // Load text from file
            string text = System.IO.File.ReadAllText(TextFilePath);

            // Extract text fragments
            _textFragments = ExtractTextFragments(text);

            // Concatenate text fragments
            _concatenatedText = ConcatenateTextFragments(_textFragments);
        }

        [PerfBenchmark(Description = "Benchmark search string matching.", NumberOfIterations = 5, RunMode = RunMode.Iterations, TestMode = TestMode.Measurement)]
        [CounterMeasurement("Search Time")]
        public void SearchBenchmark(BenchmarkContext context)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            int count = CountOccurrences(_concatenatedText, SearchString);
            stopwatch.Stop();
            Console.WriteLine($"Search string found {count} times.");
            Console.WriteLine($"Time taken: {stopwatch.Elapsed.TotalMilliseconds} milliseconds");
        }

        [PerfBenchmark(Description = "Benchmark search with Regex.", NumberOfIterations = 5, RunMode = RunMode.Iterations, TestMode = TestMode.Measurement)]
        [CounterMeasurement("Search Time")]
        public void SearchBenchmarkRegEx(BenchmarkContext context)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            int count = CountOccurrencesWithRegEx(_concatenatedText, SearchString);
            stopwatch.Stop();
            Console.WriteLine($"Search string found {count} times.");
            Console.WriteLine($"Time taken: {stopwatch.Elapsed.TotalMilliseconds} milliseconds");
        }

        private List<TextFragment> ExtractTextFragments(string text)
        {
            List<TextFragment> fragments = new List<TextFragment>();

            // Split text by lines assuming each line is a fragment
            string[] lines = text.Split('\n');

            // Create a text fragment for each line
            for (int i = 0; i < lines.Length; i++)
            {
                // Create a dummy bounding box for simplicity
                BoundingBox boundingBox = new BoundingBox(0, i * 20, 100, (i + 1) * 20);

                // Add the fragment to the list
                fragments.Add(new TextFragment(boundingBox, lines[i]));
            }

            return fragments;
        }

        private string ConcatenateTextFragments(List<TextFragment> textFragments)
        {
            // Concatenate text fragments into a single string
            return string.Join(" ", textFragments.ConvertAll(f => f.Text));
        }

        private int CountOccurrences(string input, string searchString)
        {
            // Count occurrences of search string in the input
            int count = 0;
            int index = 0;
            while ((index = input.IndexOf(searchString, index)) != -1)
            {
                index += searchString.Length;
                count++;
            }
            return count;
        }

        private int CountOccurrencesWithRegEx(string input, string searchString)
        {
            // Use regex to count occurrences of the search string
            Regex regex = new Regex(Regex.Escape(searchString));
            return regex.Matches(input).Count;
        }
    }
}
