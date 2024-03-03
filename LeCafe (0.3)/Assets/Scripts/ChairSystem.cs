using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairSystem : MonoBehaviour
{
    public bool isEmpty = true;

    void Start()
    {
        // If the chair is initially empty, add it to the list of empty chairs
        if (isEmpty)
        {
            CustomerManager.Instance.AddEmptyChair(transform);
        }
    }
    public void Occupy()
    {
        isEmpty = false;
        CustomerManager.Instance.RemoveEmptyChair(transform);
    }
    public void Leave()
    {
        isEmpty = true;
        CustomerManager.Instance.AddEmptyChair(transform);
    }

}
