using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L20250217
{
    public class World
    {
        public delegate int SortCompare(GameObject forst, GameObject second);
        public SortCompare sortCompare;

        // DynamicArray
        List<GameObject> gameObjects = new List<GameObject>();

        public List<GameObject> GetAllGameObjects
        {
            get
            {
                return gameObjects;
            }
        }

        public void Instanciate(GameObject gameObject)
        {
            gameObjects.Add(gameObject);
        }

        internal void Update()
        {
            for (int i = 0; i < gameObjects.Count; i++)
            {
                foreach(Component component in gameObjects[i].components)
                {
                    component.Update();
                }
            }
        }

        internal void Rander()
        {
            for (int i = 0; i < gameObjects.Count; i++)
            {
                SpriteRenderer spriteRender = gameObjects[i].GetComponent<SpriteRenderer>();
                if (spriteRender != null)
                {
                    spriteRender.Rander();
                }
            }
        }

        public void Sort()
        {
            for (int i = 0; i < gameObjects.Count; i++)
            {
                for (int j = i + 1; j < gameObjects.Count; j++)
                {
                    //if (gameObjects[i].GetComponent<SpriteRenderer>().orderlayer -
                    //    gameObjects[j].GetComponent<SpriteRenderer>().orderlayer > 0)

                        if (sortCompare(gameObjects[i], gameObjects[j]) > 0)
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
