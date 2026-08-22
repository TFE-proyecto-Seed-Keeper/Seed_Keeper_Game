using System.Linq;
using UnityEngine;

public static class RandomExtentions
{
    private static int[] GenerateRandomIndexes(int length, int min, int max, bool uniqueIndexes)
    {
        int[] indexes = new int[length];
        System.Array.Fill(indexes, -1);

        for (int i = 0; i < length; i++)
        {
            int index; 
            do
            {
                index = Random.Range(min, max);
            }
            while (uniqueIndexes && indexes.Contains(index));

            indexes[i] = index;
        }

        return indexes;
    }

    private static T[] GetRandom<T>(this T[] array, int length, bool uniqueIndexes = false)
    {
        if (array == null || array.Length == 0 || length <= 0)
        {
            return default;
        }

        int[] indexes = GenerateRandomIndexes(length, 0, array.Length, uniqueIndexes);

        T[] result = new T[length];

        for (int i = 0; i < indexes.Length; i++)
        {
            result[i] = array[indexes[i]];
        }

        return result;
    }

    public static T[] GetRandomItems<T>(this T[] array, int length)
    {
        return array.GetRandom(length, false);
    }

    public static T[] GetUniqueRandomItems<T>(this T[] array, int length)
    {
        return array.GetRandom(length, true);
    }
}
