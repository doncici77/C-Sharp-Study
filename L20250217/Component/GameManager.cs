using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L20250217
{
    public class GameManager : Component
    {
        public bool isGameOver = false;

        public bool isFinish = false;

        public override void Update()
        {
            if(isGameOver)
            {
                Console.WriteLine("실패!");
                Engine.Instance.Quit();
            }

            if (isFinish)
            {
                Console.WriteLine("성공@@@");
                Engine.Instance.Quit();
            }
        }
    }
}
