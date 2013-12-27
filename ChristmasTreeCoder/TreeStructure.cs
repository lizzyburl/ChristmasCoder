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
            for (int i = 0; i < bottomLevel; i++)
            {
                string substring;
                int subLength = secretMessage.Length;
                int end = start + levels[i].levelSpaces;
                substring = secretMessage.Substring(start, levels[i].levelSpaces);
                start += levels[i].levelSpaces;
                levels[i].levelString = substring;
            }
            levels[bottomLevel].levelString = secretMessage.Substring(start);
        }

        private void PrintSpaces(int spaces)
        {
            for (int i = 0; i < spaces; i++ )
                Console.Write(" ");
        }

        private void PrintLine(int level, Random random)
        {
            ConsoleColor[] ornaments = { ConsoleColor.Red, ConsoleColor.Blue, ConsoleColor.White };
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
            Random random = new Random();
            for (int i = 0; i <= bottomLevel; i++)
            {
                PrintSpaces(centerDisplacement - levels[i].levelSpaces / 2);
                PrintLine(i, random);
            }
        }

        public void WriteToTextFile(System.IO.StreamWriter outputFile)
        {
            for (int i = 0; i <= bottomLevel; i++)
                outputFile.WriteLine(levels[i].levelString);
        }
        public TreeStructure(string secretMessage)
        {
            int messageLength = secretMessage.Length;
            int disp = 0;
            CalculateNumLevels(messageLength);
            levels = new TreeLevel[bottomLevel + 1];
            CalculateLevelLengths(messageLength);
            FillTreeLevels(secretMessage.ToLower());
            PrintTree();
        }
    }
}
