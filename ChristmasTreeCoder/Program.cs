using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChristmasTreeCoder
{
    class Program
    {
        public static string CodedMessage(string unCodedMessage)
        {
            int displacement = 0;
            char[] alphabet = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
            StringWriter writer = new StringWriter();
            foreach (char uncodedChar in unCodedMessage)
            {
                writer.Write(alphabet[(uncodedChar - 'a' + displacement) % 26]);
                displacement++;
            }
            return writer.ToString();
        }

        static void Main(string[] args)
        {
            // Read in text
            string text = System.IO.File.ReadAllText(@"C:\users\lizzy\documents\visual studio 2013\Projects\ChristmasTreeCoder\ChristmasTreeCoder\SecretMessageToCode.txt");
            TreeStructure tree = new TreeStructure(CodedMessage(text));
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\users\lizzy\documents\visual studio 2013\Projects\ChristmasTreeCoder\ChristmasTreeCoder\outputFile.txt")) { 
            tree.WriteToTextFile(file);
        }
        }


    }
}
