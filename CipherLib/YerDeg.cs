using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherLib
{
    public class YerDeg
    {
        string alfabe_o = "abcçdefgğhıijklmnoöprsştuüvyz";
        string alfabe_m = "ğtjfgszdüohöpbkuvrenmclıyçiaş";
        string textToCipher, cipheredText;


        public string TextToCipher
        {
            get => textToCipher;
            set
            {
                textToCipher = value.Replace(" ", "").Replace("q", "").Replace("w", "").
                Replace("x", "").Replace("Q", "").Replace("W", "").Replace("X", "").ToLower();
            }
        }
        public string CipheredText { get => cipheredText; set => cipheredText = value; }

        public string Cipher(string input = null)
        {
            if (input != null) TextToCipher = input;

            foreach (var item in TextToCipher)
            {
                CipheredText += alfabe_m[alfabe_o.IndexOf(item)];
            }

            return CipheredText;
        }

        public string DeCipher(string input = null)
        {
            TextToCipher = string.Empty;
            if (input != null) CipheredText = input;

            foreach (var item in CipheredText)
            {
                TextToCipher += alfabe_o[alfabe_m.IndexOf(item)];
            }

            return TextToCipher;
        }
    }

    public class Permutasyon
    {
        int k;
        public string input, output;
        List<int> shuffledKeys = new List<int>();

        public Permutasyon(int count = 4)
        {
            k = count;

            for (int i = 0; i < k; i++)
            {
                shuffledKeys.Add(i);
            }
            Random rnd = new Random();
            shuffledKeys = shuffledKeys.OrderBy(x => rnd.Next()).ToList();

        }

        public string Cipher(string inc = null)
        {
            if (inc != null) input = inc;

            while (input.Length % k != 0) input += "x";

            for (int i = 0; i < input.Length; i += k)
            {
                string part = input.Substring(i, k);
                for (int j = 0; j < part.Length; j++)
                {
                    output += part[shuffledKeys[j]];
                }
            }
            return output;
        }

        public string Decipher(string inc = null)
        {
            input = string.Empty;
            char[] abc = new char[inc.Length];

            for (int i = 0; i < inc.Length; i += k)
            {
                string part = inc.Substring(i, k);

                for (int j = 0; j < part.Length; j++)
                {
                    abc[i + shuffledKeys[j]] = part[j];
                }
            }

            input = new string(abc);
            return input;
        }
    }

    public class Vinegere
    {
        private string _input, _output;
        private string _shifting;
        protected Dictionary<char, int> alfabe = new Dictionary<char, int>()
        {
            {'a', 1 },{'b', 2 },{'c', 3 },{'ç', 4 },{'d', 5 },{'e', 6 },{'f', 7 },{'g', 8 },{'ğ', 9 },{'h', 10 },
            {'ı', 11 },{'i', 12 },{'j', 13 },{'k', 14 },{'l', 15 },{'m', 16 },{'n', 17 },{'o', 18 },{'ö', 19 },{'p', 20 },
            {'r', 21 },{'s', 22 },{'ş', 23 },{'t', 24 },{'u', 25 },{'ü', 26 },{'v', 27 },{'y', 28 },{'z', 29 }
        };

        public string Input { get => _input; set => _input = value.Replace(" ", string.Empty); }
        public string Output { get => _output; protected set => _output = value; }
        public string Shifting { get => _shifting; protected set => _shifting = value; }

        public Vinegere(string key)
        {
            Shifting = key;
        }

        public string Cipher(string input = null)
        {
            if (input != null) Input = input;
            int val = 1;
            while (Input.Length % Shifting.Length != 0) Input += "a";

            for (int i = 0; i < Input.Length; i += Shifting.Length)
            {
                for (int j = 0; j < Shifting.Length; j++)
                {
                    val = alfabe[Input[i + j]];
                    val = (val + alfabe[Shifting[j]]) % 29;
                    if (val == 0) val = 29;
                    Output += alfabe.SingleOrDefault(x => x.Value == val).Key;
                }

            }
            return Output;
        }

        public string Decipher(string inc)
        {
            int val = 1; Output = string.Empty;
            for (int i = 0; i < inc.Length; i += Shifting.Length)
            {
                for (int j = 0; j < Shifting.Length; j++)
                {
                    val = alfabe[inc[i + j]];
                    val = (val - alfabe[Shifting[j]]) % 29;
                    if (val == 0) val = 29;
                    if (val < 0) val += 29;
                    Output += alfabe.SingleOrDefault(x => x.Value == val).Key;
                }
            }

            return Output;
        }
    }
}
