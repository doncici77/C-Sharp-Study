using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L20250217
{
    public class World
    {
        // DynamicArray
        List<GameObject> gameObjects = new List<GameObject>();

        public void Instanciate(GameObject gameObject)
        {
            gameObjects.Add(gameObject);
        }

        internal void Update()
        {
            for (int i = 0; i < gameObjects.Count; i++)
            {
                gameObjects[i].Update();
            }
        }

        internal void Rander()
        {
            for (int i = 0; i < gameObjects.Count; i++)
            {
                gameObjects[i].Rander();
            }
        }

        public void Sort()
        {
            for(int i = 0;i < gameObjects.Count;i++)
            {
                for (int j = 0; j < gameObjects.Count; j++)
                {
                    if (gameObjects[i].orderlayer -  gameObjects[j].orderlayer > 0)
                    {
                        GameObject temp = gameObjects[i];
                        gameObjects[i] = gameObjects[j];
                        gameObjects[j] = temp;
                    }
                }
            }
        }
    }
}
