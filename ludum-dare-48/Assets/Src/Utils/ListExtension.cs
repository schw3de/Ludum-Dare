using System.Collections.Generic;

namespace schw3de.ld48
{
    public static class ListExtension
    {
        public static T GetRandomItem<T>(this List<T> list)
        {
            int randomIndex = UnityEngine.Random.Range(0, list.Count);
            return list[randomIndex];
        }
    }
}
