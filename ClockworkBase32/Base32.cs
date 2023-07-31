using System;
using System.Collections.Generic;
using System.Linq;

namespace ClockworkBase32
{
    /// <summary>
    /// Clockwork Base32 encoding and decoding
    /// </summary>
    public static class Base32
    {
        /// <summary>
        /// Encode bytes
        /// </summary>
        /// <param name="data">Bytes to be encoded</param>
        /// <returns>Encoded string</returns>
        public static string Encode(in byte[] data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));
            if (!data.Any())
                return string.Empty;

            var encodedChars =
                data.
                Concat(Enumerable.Repeat((byte)0, 5 - data.Length % 5)).
                Select((x, n) => (x, n)).
                GroupBy(x => x.n / 5).
                Select(x => x.Select(y => y.x).ToArray()).
                SelectMany(
                    x =>
                    new[]
                    {
                        EncodeSymbols[(x[0] >> 3) & 0b11111],
                        EncodeSymbols[(x[0] << 2 | x[1] >> 6) & 0b11111],
                        EncodeSymbols[(x[1] >> 1) & 0b11111],
                        EncodeSymbols[(x[1] << 4 | x[2] >> 4) & 0b11111],
                        EncodeSymbols[(x[2] << 1 | x[3] >> 7) & 0b11111],
                        EncodeSymbols[(x[3] >> 2) & 0b11111],
                        EncodeSymbols[(x[3] << 3 | x[4] >> 5) & 0b11111],
                        EncodeSymbols[(x[4]) & 0b11111],
                    }).
                Take((int)Math.Ceiling((decimal)data.Length * 8 / 5)).
                ToArray();
            return new string(encodedChars);
        }

        /// <summary>
        /// Decode string
        /// </summary>
        /// <param name="data">String to be decoded</param>
        /// <returns>Decoded bytes</returns>
        public static byte[] Decode(in string data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));
            if (data.Length <= 1)
                return new byte[0];

            var decodedBytes =
                data.
                Select(c => DecodeSymbolTable.TryGetValue(c, out var x) ? x : throw new ArgumentException($"Invalid symbol value {c}", nameof(data))).
                Concat(Enumerable.Repeat(0, 8 - data.Length % 8)).
                Select((x, n) => (x, n)).
                GroupBy(x => x.n / 8).
                Select(x => x.Select(y => y.x).ToArray()).
                SelectMany(
                    x =>
                    new[]
                    {
                        (byte)((x[0] << 3 | x[1] >> 2) & 0xFF),
                        (byte)((x[1] << 6 | x[2] << 1 | x[3] >> 4) & 0xFF),
                        (byte)((x[3] << 4 | x[4] >> 1) & 0xFF),
                        (byte)((x[4] << 7 | x[5] << 2 | x[6] >> 3) & 0xFF),
                        (byte)((x[6] << 5 | x[7] << 0) & 0xFF),
                    }).
                Take(data.Length * 5 / 8).
                ToArray();
            return decodedBytes;
        }

        private static readonly char[] EncodeSymbols =
            new[]
            {
                '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
                'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K',
                'M', 'N', 'P', 'Q', 'R', 'S', 'T', 'V', 'W', 'X',
                'Y', 'Z',
            };

        private static readonly Dictionary<char, int> DecodeSymbolTable =
            new Dictionary<char, int>
            {
                { '0', 0 }, { 'O', 0 }, { 'o', 0 },
                { '1', 1 }, { 'I', 1 }, { 'i', 1 }, { 'L', 1 }, { 'l', 1 },
                { '2', 2 },
                { '3', 3 },
                { '4', 4 },
                { '5', 5 },
                { '6', 6 },
                { '7', 7 },
                { '8', 8 },
                { '9', 9 },
                { 'A', 10 }, { 'a', 10 },
                { 'B', 11 }, { 'b', 11 },
                { 'C', 12 }, { 'c', 12 },
                { 'D', 13 }, { 'd', 13 },
                { 'E', 14 }, { 'e', 14 },
                { 'F', 15 }, { 'f', 15 },
                { 'G', 16 }, { 'g', 16 },
                { 'H', 17 }, { 'h', 17 },
                { 'J', 18 }, { 'j', 18 },
                { 'K', 19 }, { 'k', 19 },
                { 'M', 20 }, { 'm', 20 },
                { 'N', 21 }, { 'n', 21 },
                { 'P', 22 }, { 'p', 22 },
                { 'Q', 23 }, { 'q', 23 },
                { 'R', 24 }, { 'r', 24 },
                { 'S', 25 }, { 's', 25 },
                { 'T', 26 }, { 't', 26 },
                { 'V', 27 }, { 'v', 27 },
                { 'W', 28 }, { 'w', 28 },
                { 'X', 29 }, { 'x', 29 },
                { 'Y', 30 }, { 'y', 30 },
                { 'Z', 31 }, { 'z', 31 },
            };
    }
}
