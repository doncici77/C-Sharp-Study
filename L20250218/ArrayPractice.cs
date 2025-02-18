using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L20250218
{
    class ArrayPractice
    {
        public ArrayPractice() { }
        ~ArrayPractice() { }

        public int[] array = new int[5];
        public int count = 0;

        public void Add(int addNum)
        {
            if(count >= array.Length)
            {
                int[] newArray = new int[array.Length];

                for(int i = 0; i < array.Length; i++)
                {
                    newArray[i] = array[i];
                }
                array = newArray;
            }
            array[count] = addNum;
            count++;
        }

        public void Remove(int removeNum)
        {
            for(int i = 0; i < count; i++)
            {
                if(array[i] == removeNum)
                {
                    RemoveAt(i);
                    return;
                }
            }
        }

        public void RemoveAt(int reamoveNumIndex)
        {
            if(reamoveNumIndex >= 0 && reamoveNumIndex < count)
            {
                for (int i = reamoveNumIndex; i < count - 1; i++)
                {
                    array[i] = array[i + 1];
                }
            }
            count--;
        }
    }
}
