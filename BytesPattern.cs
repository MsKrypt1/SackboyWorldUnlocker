using System.Text;

namespace SackboySaveFix
{
    public sealed class BytesPattern
    {
        public BytesPattern(string pattern)
        {
            Pattern = pattern;
            Bytes = Encoding.ASCII.GetBytes(pattern);
        }

        public string Pattern { get; }

        public byte[] Bytes { get; }

        public int? TryFindPattern(byte[] input, int searchStart = 0)
        {
            var lookupUntil = input.Length - Bytes.Length;
            for (var i = searchStart; i < lookupUntil; i++)
            {
                var j = 0;
                for (; j < Bytes.Length; j++)
                {
                    if (Bytes[j] != input[i + j]) break;
                }

                if (j == Bytes.Length) return i;
            }

            return null;
        }

        public int FindPattern(byte[] input, int searchStart = 0)
        {
            var result = TryFindPattern(input, searchStart);

            if (result == null)
            {
                throw new BytesPatternException(this);
            }

            return result.Value;
        }
    }
}
