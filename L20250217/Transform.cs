using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L20250217
{
    class Transform : Component
    {
        public int X; // X좌표
        public int Y; // Y좌표

        public override void Update()
        {

        }

        public void Translate(int addX, int addY)
        {
            X += addX;
            Y += addY;
        }
    }
}
