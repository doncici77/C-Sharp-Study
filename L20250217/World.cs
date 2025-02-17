using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L20250217
{
    public class World
    {
        public GameObject[] gameObjects = new GameObject[100]; // 100개의 GameObject를 담을 수 있는 배열
        public int useGameObjectCount = 0; // 현재 사용중인 GameObject의 개수

        public void Instanciate(GameObject gameObject)
        {
            gameObjects[useGameObjectCount] = gameObject;
            useGameObjectCount++;
        }

        internal void Update()
        {
            for (int i = 0; i < gameObjects.Length; i++)
            {
                gameObjects[i].Update();
            }
        }

        internal void Rander()
        {
            for (int i = 0; i < gameObjects.Length; i++)
            {
                gameObjects[i].Rander();
            }
        }
    }
}
