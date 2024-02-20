namespace DocuTR.TextExtractionBenchmark
{
    public class TextFragment
    {
        public BoundingBox BoundingBox { get; }
        public string Text { get; }

        public TextFragment(BoundingBox boundingBox, string text)
        {
            BoundingBox = boundingBox;
            Text = text;
        }
    }

    public class BoundingBox
    {
        public int X1 { get; }
        public int Y1 { get; }
        public int X2 { get; }
        public int Y2 { get; }

        public BoundingBox(int x1, int y1, int x2, int y2)
        {
            X1 = x1;
            Y1 = y1;
            X2 = x2;
            Y2 = y2;
        }
    }
}
