using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherLib
{
    public class Kripto
    {
        string textToCipher;
        Chiper selectedAlgo;
        public string TextToCipher { get => textToCipher; set => textToCipher = value; }
        public Chiper SelectedAlgo { get => selectedAlgo; set => selectedAlgo = value; }


        public Kripto(string textToCipher, Chiper selectedAlgo)
        {
            TextToCipher = textToCipher;
            SelectedAlgo = selectedAlgo;
            SelectedAlgo.Input = textToCipher;
        }


    }
}
