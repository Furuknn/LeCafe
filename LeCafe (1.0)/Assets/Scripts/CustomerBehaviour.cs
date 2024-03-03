using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class CustomerBehaviour : MonoBehaviour
{

    private Transform doorTransform;
    private Transform exitTransform;

    public NavMeshAgent agent;
    private ChairSystem chairSystem;
    private Transform currentChair;

    private WaveSystem waveSystem;

    [SerializeField] private GameObject bubble;

    private bool isSitting=false;
    public bool isHungry;

    void Start()
    {
        waveSystem = FindObjectOfType<WaveSystem>();

        chairSystem = FindObjectOfType<ChairSystem>();

        doorTransform = GameObject.Find("DoorTransform").GetComponent<Transform>();
        exitTransform = GameObject.Find("Exit").GetComponent<Transform>();

        isHungry = true;

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
        Debug.Log(isHungry);
        if (isHungry)
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

        }
        else
        {
            Debug.Log("Leaving");
            
            isSitting=false;
            agent.SetDestination(exitTransform.position);
            LeaveCurrentChair();
            StartCoroutine(DestroyCustomer());
        }

        if (isSitting == true && isHungry)
        {
            bubble.SetActive(true);
            this.transform.position = new Vector3(this.transform.position.x, 3.3f, this.transform.position.z);
        }
        else if (!isHungry)
        {
            bubble.SetActive(false);
            this.transform.position = new Vector3(this.transform.position.x, 1.5f, this.transform.position.z);
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

    public void LeaveCurrentChair()
    {
        currentChair.GetComponent<ChairSystem>().Leave();
    }

    public void OccupyCurrentChair()
    {
        currentChair.GetComponent<ChairSystem>().Occupy();
        
    }

    IEnumerator DestroyCustomer()
    {
        yield return new WaitForSeconds(8f);

        waveSystem.customersSpawned--;
        Destroy(this.gameObject);
    }

}
