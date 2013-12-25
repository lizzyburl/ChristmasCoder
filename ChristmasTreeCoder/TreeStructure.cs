using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChristmasTreeCoder
{
    public sealed class TreeStructure
    {
        private TreeLevel[] levels;
        int centerDisplacement = 0;
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
            }

            centerDisplacement = levels[bottomLevel].levelSpaces / 2;
        }


        public void PrintTree()
        {

        }

        public TreeStructure(string secretMessage)
        {
            int messageLength = secretMessage.Length;
            CalculateNumLevels(messageLength);
            levels = new TreeLevel[bottomLevel];
            CalculateLevelLengths(messageLength);
        }
    }
}
