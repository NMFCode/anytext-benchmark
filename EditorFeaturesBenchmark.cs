using BenchmarkDotNet.Attributes;
using NMF.AnyText;
using NMF.AnyText.AnyMeta;
using NMF.AnyText.Grammars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyText.PerformanceTests
{
    [SimpleJob]
    [MarkdownExporter]
    [HtmlExporter]
    [RPlotExporter]
    [CsvExporter]
    public class EditorFeaturesBenchmark
    {
        public static IEnumerable<string> Models => new[] { "schema", "KDM", "NMeta", "61850", "COSEM" };
        private string[] _lines;
        private readonly Grammar _anyMetaGrammar = new AnyMetaGrammar();
        private Parser _parser;

        [ParamsSource(nameof(Models))]
        public string? Model { get; set; }

        [GlobalSetup]
        public void LoadText()
        {
            _lines = File.ReadAllLines($"{Model}.anymeta");
            _anyMetaGrammar.Initialize();
            _parser = _anyMetaGrammar.CreateParser();
            _parser.Initialize(_lines, skipValidation: true);
        }

        /// <summary>
        /// Calculates all semantic tokens in the given file
        /// </summary>
        [Benchmark]
        public void SemanticTokens()
        {
            _ = _parser.GetSemanticElementsFromRoot().ToArray();
        }

        /// <summary>
        /// Calculates all folding ranges
        /// </summary>
        [Benchmark]
        public void FoldingRanges()
        {
            _ = _parser.GetFoldingRangesFromRoot().ToArray();
        }

        /// <summary>
        /// Calculates the text changes to format the document
        /// </summary>
        [Benchmark]
        public void Formatting()
        {
            _ = _parser.Format();
        }
    }
}
