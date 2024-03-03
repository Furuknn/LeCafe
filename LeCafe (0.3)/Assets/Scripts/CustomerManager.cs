using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    public static CustomerManager Instance;

    public List<Transform> emptyChairs = new List<Transform>();

    void Awake()
    {
        Instance = this;
    }

    public void AddEmptyChair(Transform chair)
    {
        if (!emptyChairs.Contains(chair))
        {
            emptyChairs.Add(chair);
        }
    }
    public void RemoveEmptyChair(Transform chair)
    {
        if (emptyChairs.Contains(chair))
        {
            emptyChairs.Remove(chair);
        }
    }
    public Transform FindNearestEmptyChair(Vector3 position)
    {
        Transform nearestChair = null;
        float minDistance = Mathf.Infinity;

        foreach (Transform chair in emptyChairs)
        {
            float distance = Vector3.Distance(position, chair.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestChair = chair;
            }
        }

        return nearestChair;
    }
}
