using NBench;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuTR
{
    class Program
    {
        static int Main(string[] args)
        {
            return NBenchRunner.Run<TextExtractionBenchmark>();
        }
    }
}
