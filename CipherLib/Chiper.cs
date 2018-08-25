using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherLib
{
    public abstract class Chiper
    {
        private string _ciphername, _input, _output;
        private int _shifting;
        private char[] _splittedText;
        protected Dictionary<char, int> alfabe = new Dictionary<char, int>()
        {
            {'a', 1 },{'b', 2 },{'c', 3 },{'ç', 4 },{'d', 5 },{'e', 6 },{'f', 7 },{'g', 8 },{'ğ', 9 },{'h', 10 },
            {'ı', 11 },{'i', 12 },{'j', 13 },{'k', 14 },{'l', 15 },{'m', 16 },{'n', 17 },{'o', 18 },{'ö', 19 },{'p', 20 },
            {'r', 21 },{'s', 22 },{'ş', 23 },{'t', 24 },{'u', 25 },{'ü', 26 },{'v', 27 },{'y', 28 },{'z', 29 }
        };

        public string Ciphername { get => _ciphername; protected set => _ciphername = value; }
        public string Input { get => _input; set => _input = value.Replace(" ", string.Empty); }
        public string Output { get => _output; protected set => _output = value; }
        public int Shifting { get => _shifting; protected set => _shifting = value; }
        public char[] SplittedText { get => _splittedText; protected set => _splittedText = value; }

        public abstract string CipherText();
        public abstract string DeCipherText(string output = null);

    }

    public class Sezar : Chiper
    {
        public Sezar()
        {
            Ciphername = "Sezar";
            Shifting = 3;
        }

        public override string CipherText()
        {
            SplittedText = Input.ToCharArray();
            for (int i = 0; i < SplittedText.Length; i++)
            {
                alfabe.TryGetValue(SplittedText[i], out int a);
                a = (a + Shifting) % 29;
                if (a == 0) a = 29;

                SplittedText[i] = alfabe.SingleOrDefault(x => x.Value == a).Key;
            }
            Output = new string(SplittedText);

            return Output;
        }

        public override string DeCipherText(string newText = null)
        {
            if (newText != null) Output = newText;
            SplittedText = Output.ToCharArray();
            for (int i = 0; i < SplittedText.Length; i++)
            {
                alfabe.TryGetValue(SplittedText[i], out int a);
                a = (a - Shifting) % 29;
                if (a == 0) a = 29;
                if (a < 0) a += 29;

                SplittedText[i] = alfabe.SingleOrDefault(x => x.Value == a).Key;
            }
            Output = new string(SplittedText);

            return Output;
        }
    }

    public class Shift : Chiper
    {
        public Shift(int shift = 1)
        {
            Ciphername = "Shift";
            Shifting = shift;
        }

        public override string CipherText()
        {
            SplittedText = Input.ToCharArray();
            for (int i = 0; i < SplittedText.Length; i++)
            {
                alfabe.TryGetValue(SplittedText[i], out int a);
                a = (a + Shifting) % 29;
                if (a == 0) a = 29;
                SplittedText[i] = alfabe.SingleOrDefault(x => x.Value == a).Key;
            }
            Output = new string(SplittedText);

            return Output;
        }

        public override string DeCipherText(string newText = null)
        {
            if (newText != null) Output = newText;
            SplittedText = Output.ToCharArray();
            for (int i = 0; i < SplittedText.Length; i++)
            {
                alfabe.TryGetValue(SplittedText[i], out int a);
                a = (a - Shifting) % 29;
                if (a == 0) a = 29;
                if (a < 0) a += 29;
                SplittedText[i] = alfabe.SingleOrDefault(x => x.Value == a).Key;
            }
            Output = new string(SplittedText);

            return Output;
        }
    }

    public class Affine : Chiper
    {
        private int _a, _b;

        public int A { get => _a; set => _a = value; }
        public int B { get => _b; set => _b = value; }

        public Affine(int a = 3, int b = 2)
        {   // A sayisi ile alfabedeki harf sayisi aralarında asal olmalıdır.
            Ciphername = "Affine/Linear";
            A = a; B = b;

        }

        public override string CipherText()
        {
            SplittedText = Input.ToCharArray();
            for (int i = 0; i < SplittedText.Length; i++)
            {
                alfabe.TryGetValue(SplittedText[i], out int z);
                z = (A * z + B) % 29; //Kongrüans ?

                SplittedText[i] = alfabe.SingleOrDefault(x => x.Value == z).Key;
            }
            Output = new string(SplittedText);

            return Output;
        }

        public override string DeCipherText(string newText = null)
        {
            if (newText != null) Output = newText;
            SplittedText = Output.ToCharArray();
            for (int i = 0; i < SplittedText.Length; i++)
            {
                alfabe.TryGetValue(SplittedText[i], out int z);
                while ((z - B) % A != 0) z += 29;
                z = (z - B) / A;

                SplittedText[i] = alfabe.SingleOrDefault(x => x.Value == z).Key;
            }
            Output = new string(SplittedText);

            return Output;
        }
    }

    public class Matrix : Chiper
    {
        new string alfabe = "abcçdefgğhıijklmnoöprsştuüvyzx";
        char[][,] squares = new char[3][,]
            {
                new char[6,5],
                new char[6,5],
                new char[6,5]
            };
        List<string> divided = new List<string>();
        int[] rand = { 0, 3, 1, 4, 2 }; //variables for random filling
        int[] rand2 = { 3, 1, 2, 5, 0, 4 };

        public Matrix()
        {
            PrepTheMatrices();
            #region dev
            //foreach (var item in squares)
            //{
            //    for (int i = 0; i < item.GetLength(0); i++)
            //    {
            //        for (int j = 0; j < item.GetLength(1); j++)
            //        {
            //            Console.Write(item[i, j]);

            //        }
            //        Console.WriteLine("");
            //    }
            //    Console.WriteLine("---------------");
            //}
            //Console.WriteLine(CipherText());
            // Console.WriteLine(DeCipherText());

            //foreach (var item in divided)
            //{
            //    Console.WriteLine(item);
            //    var a = IndexOfGiven(squares[0], item[0]);
            //    var b = IndexOfGiven(squares[0], item[1]);
            //    Console.WriteLine(a.ToString());
            //    Console.WriteLine(b.ToString());
            //}
            #endregion
        }

        /// <summary>
        /// Fill the 4 matrix >>> 0.-2. matrices in order, 1.-3. matrices shuffled.
        /// </summary>
        private void PrepTheMatrices()
        {
            var ctr = 0;
            var splitter = alfabe.ToCharArray();
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    squares[0][i, j] = splitter[ctr];
                    squares[1][rand2[i], rand[j]] = splitter[ctr];
                    squares[2][rand2[5 - i], rand[4 - j]] = splitter[ctr];
                    ctr++;
                }
            }

        }

        /// <summary>
        /// Prepare the input for encryption(tuples of characters etc.)
        /// </summary>
        private void PrepTheInput()
        {
            if (Input.Length % 2 == 1) Input += "x";

            for (int i = 0; i < Input.Length; i += 2)
            {
                divided.Add(Input.Substring(i, 2));
            }
        }

        /// <summary>
        /// Returns the index of given char on given 2d matrix
        /// </summary>
        /// <param name="incMatrix">Search this matrix</param>
        /// <param name="ch">for this char</param>
        /// <returns>row and column values, -1,-1 if non present</returns>
        private Tuple<int,int> IndexOfGiven(char[,] incMatrix, char ch)
        {
            int a = incMatrix.GetLength(0);
            int b = incMatrix.GetLength(1);
            for (int i = 0; i < a; i++)
            {
                for (int j = 0; j < b; j++)
                {
                    if (incMatrix[i, j].Equals(ch))
                        return Tuple.Create(i, j);
                }
            }
            return Tuple.Create(-1, -1);
        }

        
        public override string CipherText()
        {
            PrepTheInput();
            Tuple<int,int> first, second;
            foreach (var item in divided)
            {
                first = IndexOfGiven(squares[0],item[0]);
                second = IndexOfGiven(squares[0], item[1]);
                Output = Output + squares[1][first.Item1, second.Item2] + squares[2][second.Item1, first.Item2];
            }
            return Output;
        }

        public override string DeCipherText(string output = null)
        {
            if (output != null) Output = output;
            divided.Clear();
            for (int i = 0; i < Output.Length; i += 2)
            {
                divided.Add(Output.Substring(i, 2));
            }
            Output = string.Empty;
            Tuple<int, int> first, second;
            foreach (var item in divided)
            {
                first = IndexOfGiven(squares[1], item[0]);
                second = IndexOfGiven(squares[2], item[1]);
                Output = Output + squares[0][first.Item1, second.Item2] + squares[0][second.Item1, first.Item2];
            }
            return Output;
        }
    }
}
