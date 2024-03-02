using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class CustomerBehaviour : MonoBehaviour
{

    private Transform cafeDoor;
    public NavMeshAgent agent;
    

    void Start()
    {
        cafeDoor = GameObject.Find("Door").GetComponent<Transform>();

        if (cafeDoor!=null)
        {
            Vector3 door = new Vector3(cafeDoor.position.x, cafeDoor.position.y, cafeDoor.position.z);
            agent.SetDestination(door);
        }
        else
        {
            Debug.Log("cant find door object");
            
        }
        
    }

}
