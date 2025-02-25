using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L20250225
{
    public class Array<T>
    {
        public T[] data = new T[3];
        int count = 0;

        public void Add(T index)
        {
            if (count < data.Length)
            {
                data[count] = index;
            }
            else if (count >= data.Length)
            {
                T[] dataTemp = new T[data.Length + 1];
                for (int i = 0; i < data.Length; i++)
                {
                    dataTemp[i] = data[i];
                }
                dataTemp[count] = index;
                data = dataTemp;
            }
            count++;
        }

        public void RemoveAt(int ordernum)
        {
            T[] dataTemp = new T[data.Length - 1];

            for (int i = 0; i < ordernum - 1; i++)
            {
                dataTemp[i] = data[i];
            }

            for (int i = ordernum; i < count; i++)
            {
                dataTemp[i - 1] = data[i];
            }
            data = dataTemp;
            count--;
        }

        public int Count
        {
            get { return count; }
            set { count = value; }
        }

        public T this[int index]
        {
            get
            {
                return data[index];
            }
            set
            {
                if (index < data.Length)
                {
                    data[index] = (T)value;
                }
            }
        }
    }
}
