using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChristmasTreeCoder
{
    class Program
    {
        static void Main(string[] args)
        {
            // Read in text
            String text = System.IO.File.ReadAllText(@"C:\users\lizzy\documents\visual studio 2013\Projects\ChristmasTreeCoder\ChristmasTreeCoder\SecretMessageToCode.txt");
            Console.WriteLine(text.Length );

        }


    }
}
