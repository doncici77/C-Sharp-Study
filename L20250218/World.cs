using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L20250218
{
    public class World
    {
        public GameObject[] gameObject = new GameObject[100];

        public int useGameObjectCount = 0;

        public void Instanciate(GameObject getGameObjects)
        {
            gameObject[useGameObjectCount] = getGameObjects;
            useGameObjectCount++;
        }

        public void Update()
        {
            for (int i = 0; i < gameObject.Length; i++)
            {
                gameObject[i].Update();
            }
        }

        public void Rander()
        {
            for(int i = 0;i < gameObject.Length;i++)
            {
                gameObject[i].Rander();
            }
        }

    }
}
