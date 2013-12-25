using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChristmasTreeCoder
{
    public sealed class TreeLevel
    {
        public string levelString;
        public int levelSpaces;

        public TreeLevel(int spaces)
        {
            levelSpaces = spaces;
        }
    }
}
