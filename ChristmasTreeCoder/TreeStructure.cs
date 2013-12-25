using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChristmasTreeCoder
{
    public sealed class TreeStructure
    {
        private TreeLevel[] levels;
        int centerDisplacement;
        int bottomLevel;

        private void CalculateNumLevels(int messageLength)
        {
            int increment = 1;
            int currentLevel = 0;
            while (messageLength > increment)
            {
                messageLength -= increment;
                increment += 2;
                currentLevel++;
            }
            bottomLevel = currentLevel - 1;
        }

        private void CalculateLevelLengths(int messageLength)
        {
            int increment = 1;
            int currentLevel = 0;
            while (messageLength > increment)
            {
                levels[currentLevel] = new TreeLevel(increment);
                messageLength -= increment;
                increment += 2;
                currentLevel++;
            }

            currentLevel = bottomLevel;
            // the remainder is the message length. We don't want a half finished bottom row, so we will spread it around
            while (messageLength > 0)
            {
                if (currentLevel < 0)
                {
                    currentLevel = bottomLevel;
                }
                levels[currentLevel].levelSpaces += 2;
                currentLevel--;
                messageLength -= 2;
            }

            centerDisplacement = levels[bottomLevel].levelSpaces / 2;
        }
        
        private void FillTreeLevels(string secretMessage)
        {
            int start = 0;
            int displacement = 0;
            for (int i = 0; i <= bottomLevel; i++)
            {
                string substring = secretMessage.Substring(start, start + levels[i].levelSpaces);
                start = levels[i].levelSpaces;
                levels[i].levelString = EncodedLevel(ref displacement, substring);
            }
        }

        private string EncodedLevel(ref int displacement, string substring)
        {
            char[] alphabet = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
            StringWriter writer = new StringWriter();
            foreach (char uncodedChar in substring)
            {
                writer.Write(alphabet[(uncodedChar - 'a' + displacement) % 26]);
                displacement++;
            }
            return writer.ToString();
        }
        private void PrintSpaces(int spaces)
        {
            for (int i = 0; i < spaces*2; i++ )
                Console.Write(" ");
        }

        private void PrintLine(int level)
        {
            ConsoleColor[] ornaments = { ConsoleColor.Red, ConsoleColor.Blue, ConsoleColor.White };
            Random random = new Random();
            foreach (char letter in levels[level].levelString)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                int rand = random.Next(0, 5);
                if (rand == 0)
                {
                    rand = random.Next(0, 3);
                    Console.ForegroundColor = ornaments[rand];
                }
                Console.Write(letter);
            }
            Console.WriteLine();
        }
        public void PrintTree()
        {
            for (int i = 0; i <= bottomLevel; i++)
            {
                PrintSpaces(centerDisplacement - levels[i].levelSpaces / 2);
                PrintLine(i);
            }
        }

        public TreeStructure(string secretMessage)
        {
            int messageLength = secretMessage.Length;
            CalculateNumLevels(messageLength);
            levels = new TreeLevel[bottomLevel + 1];
            CalculateLevelLengths(messageLength);
            FillTreeLevels(secretMessage.ToLower());
            PrintTree();
        }
    }
}
