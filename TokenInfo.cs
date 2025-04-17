using NMF.AnyText;

namespace AnyText.PerformanceTests
{
    public record struct TokenInfo(string token, int line, int col)
    {
        public static TokenInfo FromString(string line)
        {
            var split = line.Split(';');
            var col = int.Parse(split[^1]);
            var lin = int.Parse(split[^2]);
            var lit = split.Length == 3 ? split[0] : ";";
            return new TokenInfo(lit, lin, col);
        }

        public void Remove(string[] lines)
        {
            lines[line] = Remove(lines[line]);
        }

        public void Insert(string[] lines)
        {
            lines[line] = Insert(lines[line]);
        }

        public string Remove(string text)
        {
            return text.Substring(0, col) + text.Substring(col + token.Length);
        }

        public string Insert(string text)
        {
            return text.Substring(0, col) + token + text.Substring(col);
        }

        public TextEdit AsTextEdit()
        {
            return new TextEdit(new ParsePosition(line, col), new ParsePosition(line, col), new[] { token} );
        }

        public TextEdit AsRemoveTextEdit()
        {
            return new TextEdit(new ParsePosition(line, col), new ParsePosition(line, col + token.Length), new[] {string.Empty} );
        }
    }
}
