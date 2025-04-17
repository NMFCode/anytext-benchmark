using BenchmarkDotNet.Attributes;
using Microsoft.VisualBasic;
using NMF.AnyText;
using NMF.AnyText.AnyMeta;
using NMF.AnyText.Grammars;
using NMF.AnyText.Rules;

namespace AnyText.PerformanceTests
{
    [SimpleJob(iterationCount: 50)]
    [MarkdownExporter]
    [HtmlExporter]
    [RPlotExporter]
    [CsvExporter]
    public class AnyMetaInitializeUpdateBenchmark
    {
        public static IEnumerable<string> Models => new[] { "schema", "KDM", "NMeta", "61850", "COSEM" };
        private string[] _lines;
        private readonly Grammar _anyMetaGrammar = new AnyMetaGrammar();
        private TokenInfo[] _tokenInfo;
        private Parser _parser;
        private readonly Random _random = new Random(42);
        private TokenInfo[] _tokensInOrder;

        [ParamsSource(nameof(Models))]
        public string? Model { get; set; }

        [Params(1, 10, 100)]
        public int TokenChanges { get; set; }

        public int TokenOffset { get; set; }

        [GlobalSetup]
        public void LoadText()
        {
            _lines = File.ReadAllLines($"{Model}.anymeta");
            _anyMetaGrammar.Initialize();
            var tokenSource = File.ReadAllLines($"{Model}.tokens");
            _tokenInfo = new TokenInfo[500];
            for (int i = 0; i < 500; i++)
            {
                _tokenInfo[i] = TokenInfo.FromString(tokenSource[i]);
            }
            _parser = _anyMetaGrammar.CreateParser();
            _parser.Initialize(_lines);
        }

        [IterationSetup]
        public void NextIteration()
        {
            TokenOffset = _random.Next(400);
            _tokensInOrder = _tokenInfo.Skip(TokenOffset).Take(TokenChanges).OrderBy(t => (t.line, t.col)).ToArray();
            var lastLine = -1;
            var accumulatedLen = 0;
            for (int i = 0; i < TokenChanges; i++)
            {
                var token = _tokensInOrder[i];
                if (token.line == lastLine)
                {
                    _tokensInOrder[i] = new TokenInfo(token.token, token.line, token.col - accumulatedLen);
                    accumulatedLen += token.token.Length;
                }
                else
                {
                    lastLine = token.line;
                    accumulatedLen = token.token.Length;
                }
            }
        }

        [Benchmark]
        public void ReInitialize()
        {
            for (int i = 0; i < TokenChanges; i++)
            {
                _tokensInOrder[i].Remove(_lines);
            }
            _parser.Initialize(_lines);
            for (int i = TokenChanges - 1; i >= 0; i--)
            {
                _tokensInOrder[i].Insert(_lines);
            }
            _parser.Initialize(_lines);
        }

        [Benchmark]
        public void Update()
        {
            var updates = new List<TextEdit>();
            for (int i = 0; i < TokenChanges; i++)
            {
                updates.Add(_tokensInOrder[i].AsRemoveTextEdit());
            }
            _parser.Update(updates);
            updates.Clear();
            for (int i = TokenChanges - 1; i >= 0; i--)
            {
                updates.Add(_tokensInOrder[i].AsTextEdit());
            }
            _parser.Update(updates);
        }

        [Benchmark]
        public void UpdateAlways()
        {
            for (int i = 0; i < TokenChanges; i++)
            {
                _parser.Update(_tokensInOrder[i].AsRemoveTextEdit());
            }
            for (int i = TokenChanges - 1; i >= 0; i--)
            {
                _parser.Update(_tokensInOrder[i].AsTextEdit());
            }
        }
    }
}
