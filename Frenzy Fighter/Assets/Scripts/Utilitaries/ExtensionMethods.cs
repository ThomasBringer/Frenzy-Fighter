using UnityEngine;

public static class ExtensionMethods
{
    public static T RandomItem<T>(this T[] array)
    {
        return array[Random.Range(0, array.Length)];
    }
}