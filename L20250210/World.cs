using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L20250210
{
    class GameObject
    {

    }
    public class World
    {
        public World() 
        {
            GameObject[] gameObject = new GameObject[212];

            walls = new Wall[100];
            floors = new Floor[100];
            player = new Player();
            monsters = new Monster[10];
            goal = new Goal();
        }
        ~World()
        {

        }

        public Wall[] walls;

        public Floor[] floors;

        public Player player;

        public Monster[] monsters;

        public Goal goal;
    }
}
