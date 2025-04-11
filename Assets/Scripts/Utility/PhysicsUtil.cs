using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FeedMyKids1.Utilities
{
public class PhysicsUtil
{
    private static int bufferSize = 256;

    private static Collider[] buffer = new Collider[bufferSize];

    /// <summary>
    /// Return first object of type nearby
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T FindTypeNear<T>(Vector3 pos, float rad, int maxChildren = 99) 
    {
        Physics.OverlapSphereNonAlloc(pos, rad, buffer);

        T result = default (T);

        for (int i = 0; i < buffer.Length; i++)
            if (buffer[i] != null && TransformUtils.TryFindInTransform(buffer[i].transform, out result, maxChildren))
                return result;
        
        /*
        Parallel.For(0, buffer.Length, (i, state) =>
        {
            if (buffer[i] != null && buffer[i].gameObject.TryGetComponent(out T type))
            {
                result = type;
                state.Break();
            }
        });
        */

        return result;
    }


    /// <summary>
    /// Return first object of type nearby
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T FindClosestOfTypeNear<T>(Vector3 pos, float rad)
    {
        Physics.OverlapSphereNonAlloc(pos, rad, buffer);

        T result = default(T);
        float distance = 0f;
        float closestDistance = float.MaxValue;

        for (int i = 0; i < buffer.Length; i++)
        {
            if (buffer[i] != null && buffer[i].gameObject.TryGetComponent(out T type)
                && (distance = Vector3.Distance(buffer[i].transform.position, pos)) < closestDistance)
            {
                result = type;
                closestDistance = distance;
            }
        }
        /*
        Parallel.For(0, buffer.Length, (i, state) =>
        {
            if (buffer[i].gameObject.TryGetComponent(out T type)
                && (distance = Vector3.Distance(buffer[i].transform.position, pos)) < closestDistance)
            {
                result = type;
                closestDistance = distance;
            }
        });
        */
        return result;
    }

}
}
