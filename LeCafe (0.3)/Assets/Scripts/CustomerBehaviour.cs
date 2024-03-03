using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class CustomerBehaviour : MonoBehaviour
{

    private Transform doorTransform;
    public NavMeshAgent agent;
    private ChairSystem chairSystem;
    private Transform currentChair;

    [SerializeField] private GameObject bubble;

    private bool isSitting=false;

    void Start()
    {
        chairSystem = FindObjectOfType<ChairSystem>();

        doorTransform = GameObject.Find("DoorTransform").GetComponent<Transform>();

        if (doorTransform!=null)
        {
            agent.SetDestination(doorTransform.position);
        }
        else
        {
            Debug.Log("cant find door object");
            
        }
        
    }

    void Update()
    {
        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                Debug.Log("Reached cafes door");
                // If the current chair is occupied or doesn't exist, find the nearest empty chair
                if (currentChair == null)
                {
                    FindNearestEmptyChair();
                }
                else
                {
                    OccupyCurrentChair();
                    isSitting = true;
                }
            }
        }


        if (isSitting==true)
        {
            bubble.SetActive(true);
            this.transform.position = new Vector3(this.transform.position.x, 3.3f, this.transform.position.z);
        }
    }
    void FindNearestEmptyChair()
    {
        Transform nearestChair = CustomerManager.Instance.FindNearestEmptyChair(transform.position);

        if (nearestChair != null)
        {
            currentChair = nearestChair;
            agent.SetDestination(nearestChair.position);
            
            OccupyCurrentChair();
        }
        else
        {
            agent.SetDestination(doorTransform.position);
        }
    }

    public void OccupyCurrentChair()
    {
        currentChair.GetComponent<ChairSystem>().Occupy();
        
    }

}
