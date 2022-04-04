using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace schw3de.ld
{
    public static class Randomizer
    {
        public static T[] ShuffleArray<T>(this T[] originalArray)
        {
            var result = (T[])originalArray.Clone();
            int arrrayLength = result.Length;
            while (arrrayLength > 1)
            {
                arrrayLength--;
                int randomIndex = UnityEngine.Random.Range(0, arrrayLength);
                T value = result[randomIndex];
                result[randomIndex] = result[arrrayLength];
                result[arrrayLength] = value;
            }

            return result;
        }

        public static void ShuffleList<T>(this List<T> list)
        {
            int count = list.Count;
            while (count > 1)
            {
                count--;
                int randomIndex = UnityEngine.Random.Range(0, count);
                T value = list[randomIndex];
                list[randomIndex] = list[count];
                list[count] = value;
            }
        }

        public static T GetRandomItem<T>(this List<T> list)
        {
            var index = UnityEngine.Random.Range(0, list.Count);
            return list[index];
        }

        public static T GetRandomItem<T>(this T[] array)
        {
            var index = UnityEngine.Random.Range(0, array.Length);
            return array[index];
        }
    }
}
