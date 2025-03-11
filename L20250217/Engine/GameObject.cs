using SDL2;
using System;
using System.Reflection;

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

        /// <summary>
        /// 컴포넌트 추가 함수(초기화?)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T AddComponent<T>(T inComponent) where T : Component, new()
        {
            components.Add(inComponent);
            inComponent.gameObject = this;
            inComponent.transform = transform;
            return inComponent;
        }

        public T AddComponent<T>() where T : Component, new()
        {
            T inComponent = new T();
            AddComponent<T>(inComponent);

            return inComponent;
        }

        public void Init()
        {
            transform = AddComponent<Transform>();
            AddComponent<Transform>();
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

        public virtual void Update()
        {
            // 모든 컴포넌트의 update 함수 실행해줘.
        }

        public void ExecuteMethod(string methodName, Object[] parameters)
        {
            foreach (var component in components)
            {
                Type type = component.GetType();
                MethodInfo[] methodInfos = type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                foreach (var methodInfo in methodInfos)
                {
                    if (methodInfo.Name.CompareTo(methodName) == 0)
                    {
                        methodInfo.Invoke(component, parameters);
                    }
                }
            }
        }
    }
}
