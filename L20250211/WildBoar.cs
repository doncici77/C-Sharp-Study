using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L20250211
{
    public class WildBoar : Monster
    {
        public WildBoar() { }
        ~WildBoar() { }
        public override void Move()
        {
            Console.WriteLine("멧돼지가 달림");
        }
    }
}
