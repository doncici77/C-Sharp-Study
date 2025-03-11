using SDL2;
using System;

namespace L20250217
{
    public class GameObject
    {
        public List<Component> components = new List<Component>();

        public string Name;

        protected static int gameObjectCount = 0;

        public Transform transform;

        public GameObject()
        {
            Init();
            gameObjectCount++;
            Name = $"GameObject({gameObjectCount})";
        }

        ~GameObject()
        {
            gameObjectCount--;
        }

        public T AddComponent<T>(T inComponent) where T : Component
        {
            components.Add(inComponent);
            inComponent.gameObject = this;

            return inComponent;
        }

        public void Init()
        {
            transform = AddComponent<Transform>(new Transform());
        }

        public bool PredictCollision(int newX, int newY)
        {
            /*for (int i = 0; i < Engine.Instance.world.GetAllGameObjects.Count; i++)
            {
                if (Engine.Instance.world.GetAllGameObjects[i].isCollide == true &&
                   Engine.Instance.world.GetAllGameObjects[i].X == newX &&
                   Engine.Instance.world.GetAllGameObjects[i].Y == newY)
                {
                    return true;
                }
            }*/
            return false;
        }

        public T GetComponent<T>() where T : Component
        {
            foreach (Component component in components)
            {
                if (component is T)
                {
                    return component as T;
                }
            }

            return null;
        }

        public virtual void Update()
        {
            // 모든 컴포넌트의 update 함수 실행해줘.
        }
    }
}
