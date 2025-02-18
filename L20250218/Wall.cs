using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L20250218
{
    public class Wall : GameObject
    {
        public Wall(int inX, int inY, char inShape)
        {
            x = inX;
            y = inY;
            shape = inShape;
        }
    }
}
