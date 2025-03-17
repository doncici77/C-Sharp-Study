using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L20250317
{
    class GameObject
    {
        public GameObject(int inGold = 100, int inHp = 100, int inMp = 100)
        {
            Gold = inGold;
            Hp = inHp;
            Mp = inMp;
        }

        public int Gold;
        public int Mp;
        public int Hp;
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<GameObject> gameObjects = new List<GameObject>();
            gameObjects.Add(new GameObject(10, 20, 30));
            gameObjects.Add(new GameObject(100, 20, 30));
            gameObjects.Add(new GameObject(10, 200, 30));
            gameObjects.Add(new GameObject(10, 20, 300));
            gameObjects.Add(new GameObject(10, 200, 300));
            gameObjects.Add(new GameObject(100, 200, 30));

            string jsonData2 = JsonConvert.SerializeObject(gameObjects);

            Console.WriteLine(jsonData2);

            List<GameObject> gameObjects2 = JsonConvert.DeserializeObject<List<GameObject>>(jsonData2);


            foreach (var go in gameObjects2)
            {
                Console.WriteLine(go.Gold);
            }

            // json 하나
            GameObject g = new GameObject(10, 20, 30);
            string jsonData = JsonConvert.SerializeObject(g);

            Console.WriteLine(jsonData);

            GameObject g2 = JsonConvert.DeserializeObject<GameObject>(jsonData);

            Console.WriteLine(g2.Gold);

            // json 파싱
            string Data = "{Gold : 10, HP : 20, MP : 30}";

            JObject json = JObject.Parse(Data);
            Console.WriteLine(json.Value<int>("Gold"));
            Console.WriteLine(json.Value<int>("HP"));
            Console.WriteLine(json.Value<int>("MP"));
        }
    }
}
