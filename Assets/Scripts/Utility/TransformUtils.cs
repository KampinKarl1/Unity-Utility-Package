using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FeedMyKids1.Utilities
{
public static class TransformUtils 
{
    public static bool TryFindInTransform <T>(Transform parent, out T type) 
    {
        Transform curParent = parent;

        int size = curParent.childCount;

        for (int i = 0; i < size; i++) 
        {
            Transform child = curParent.GetChild(i);

            if (child.TryGetComponent(out type))
                return true;

            else if (child.childCount > 0)
                return TryFindInTransform<T>(child, out type);
        }

        type = default(T);
        return false;
    }

    public static bool TryFindInTransform<T>(Transform parent, out T type, int maxChildren)
    {
        Transform curParent = parent;

        int size = curParent.childCount;

        for (int i = 0; i < size; i++)
        {
            if (i == maxChildren)
                break;

            Transform child = curParent.GetChild(i);

            if (child.TryGetComponent(out type))
                return true;

            else if (child.childCount > 0)
                return TryFindInTransform<T>(child, out type, maxChildren - i);
        }

        type = default(T);
        return false;
    }

    public static T FindInTransform<T>(string name, Transform parent)
    {
        name = name.ToLower();

        Transform curParent = parent;

        int size = curParent.childCount;

        for (int i = 0; i < size; i++)
        {
            Transform child = curParent.GetChild(i);

            if (child.name.Contains(name)
                && child.TryGetComponent(out T type))
                return type;

            else if (child.childCount > 0)
                return FindInTransform<T>(name, child);
        }

        return default(T);
    }
    public static Transform[] GetAllChildren(Transform parent) 
    {
        List<Transform> result = new List<Transform>();

        Transform curParent = parent;

        int size = curParent.childCount;

        for (int i = 0; i < size; i++)
        {
            Transform child = curParent.GetChild(i);

            result.Add(child);

            if (child.childCount > 0)
                result.AddRange(GetAllChildren(child));
        }

        return result.ToArray();
    }

    public static T[] GetAllInChildren<T>(Transform parent) 
    {
        List<T> result = new List<T>();

        for (int i = 0; i < parent.childCount; i++)
        {
            Transform child = parent.GetChild(i);

            if (child.childCount > 0)
                result.AddRange(GetAllInChildren<T>(child));

            if (child.TryGetComponent(out T type))
                result.Add(type);
        }

        return result.ToArray();
    }
}
}
