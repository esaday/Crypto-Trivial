using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CipherLib;

namespace Apps
{
    class Program
    {
        static void Main(string[] args)
        {
            //List<Chiper> algos = new List<Chiper>(4);
            //algos.Add(new Sezar());
            //algos.Add(new Shift(3));
            //algos.Add(new Affine(1,3));
            //algos.Add(new Matrix());
            //foreach (var item in algos)
            //{
            //    item.Input = "teknoloji";
            //    Console.WriteLine(item.CipherText());
            //    Console.WriteLine(item.DeCipherText());
            //}

            YerDeg yd = new YerDeg();
            Console.WriteLine(yd.Cipher("abcd"));
            Console.WriteLine(yd.DeCipher(yd.CipheredText));

            Permutasyon p = new Permutasyon();
            Console.WriteLine(p.Cipher("abdurrahman"));
            Console.WriteLine(p.Decipher(p.output));

            Vinegere c = new Vinegere("kabak");
            Console.WriteLine(c.Cipher("teknoloji"));
            Console.WriteLine(c.Decipher(c.Output));

            Console.ReadKey();
        }
    }
}
