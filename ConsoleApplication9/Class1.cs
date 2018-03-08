using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication9
{
    class HANYOU
    {

        public int tileClr(int xs)
        {
            int i = -1;
            if ((xs > 34) || (xs <= 0)) return -1;
            i = (int)((xs - 1) / 9);
            return i;
        }

        public int tileNum(int xs)
        {
            int i = -1;
            if ((xs > 34) || (xs <= 0)) return -1;
            i = ((xs - 1) % 9) + 1;
            return i;
        }

        public bool isYaochuXS(int xs) 
        {

            if (tileClr(xs) == 3) return true;
            if (tileNum(xs) == 1 || tileNum(xs) == 9) return true;

            return false;
        
        }

    }
}
