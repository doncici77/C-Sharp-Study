using System.Collections;

namespace L20250218
{
    class DynamicArray
    {
        public DynamicArray()
        {

        }

        ~DynamicArray()
        {

        }

        //objects
        //[1][2][3]
        // ^  ^  ^  ^
        //newObjects
        //[1][2][3][][][]
        //          ^
        //objects <- newObjects 
        //[1][2][3][4][][]
        //          ^

        public void Add(Object inObject)
        {
            if (count >= objects.Length)
            {
                ExtendSpace();
            }
            objects[count] = inObject;
            count++;
        }

        protected void ExtendSpace()
        {
            //배열 늘이기
            //이전 정보 옮기기
            Object[] newObject = new Object[objects.Length * 2];
            //이전값 이동
            for (int i = 0; i < objects.Length; ++i)
            {
                newObject[i] = objects[i];
            }
            objects = null;
            objects = newObject;
        }

        //[][][][][]
        public void Remove(Object removObject)
        {
            for(int i = 0; i < Count; ++i)
            {
                if(removObject == objects[i])
                {
                    RemoveAt(i);
                    return;
                }
            }
        }

        //[][][][][][]
        public void RemoveAt(int index)
        {   
            if(index >= 0 && index < Count)
            {
                for (int i = index; i < Count - 1; ++i)
                {
                    objects[i] = objects[i + 1];
                }

                count--;
            }
        }

        public void Insert(int insertIndex, Object value)
        {
            if(objects.Length == count)
            {
                ExtendSpace();
            }

            for(int i = count; i > insertIndex; i--)
            {
                objects[i] = objects[i - 1];
            }
            objects[insertIndex + 1] = value; 
            count++;
        }

        /*//내버전
        public void Insert(int insertIndex, Object value)
        {
            Object[] newObject = new Object[objects.Length + 1];
            for (int i = 0; i < objects.Length; ++i)
            {
                if (i <= insertIndex)
                {
                    newObject[i] = objects[i];
                }
                else if (i > insertIndex)
                {
                    newObject[i + 1] = objects[i];
                }
            }
            newObject[insertIndex + 1] = value;
            objects = null;
            objects = newObject;
            count++;
        }*/

        protected Object[] objects = new Object[3];

        protected int count = 0;

        public int Count
        {
            get
            {
                return count;
            }
        }

        public Object this[int index]
        {
            get
            {
                return objects[index];
            }
            set
            {
                if (index < objects.Length)
                {
                    objects[index] = value;
                }
            }
        }
    }


    internal class Program
    {
        static public void PrintI(int[] data)
        {
            for(int i = 0; i < data.Length; i++)
            {
                Console.WriteLine(data[i]);
            }
        }
         
        static void Main(string[] args)
        {

            //[] ->                  variable
            //[][][][][]             array -> Array
            //[][][][][][][][][][]   DynamicArray
            //DataStructure          자료구조
            //

            DynamiArray<int> a = new DynamiArray<int>();
            for (int i = 0; i < 10; ++i)
            {
                a.Add(i);
            }

            a[1] = 11;
            a[9] = 29;

            a.RemoveAt(9);
            a.RemoveAt(1);
            a.RemoveAt(3);

            a.Insert(1, 2);
            a.Insert(5, 3);

            for (int i = 0; i < a.Count; ++i)
            {
                Console.Write(a[i] + ", ");
            }


            Engine.Instance.Load();

            Engine.Instance.Run();
        }
    }
}
