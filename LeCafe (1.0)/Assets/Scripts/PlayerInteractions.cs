using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    #region Variables

    private GameObject currentInteractable; // Currently interactable object within range

    [SerializeField] private GameObject holdPizza;
    [SerializeField] private GameObject holdCoffee;
    [SerializeField] private GameObject holdCake;

    [SerializeField] private GameObject timePanel;
    [SerializeField] private GameObject infoPanel;

    [SerializeField] private TextMeshProUGUI infoText;
    [SerializeField] private TextMeshProUGUI timeText;

    [SerializeField] private PlayerController playerController;
    [SerializeField] private CustomerBehaviour customerBehaviour;

    [SerializeField] private GameManager gameManager;

    private float pizzaHeatTime = 8f;
    private float coffeeHeatTime = 3f;

    private const int COFFEE_COST = 30;
    private const int DESSERT_COST = 20;
    private const int PIZZA_COST = 50;

    private bool handsFull=false;
    bool microwaveFull = false;
    bool pizzaHeated=false;
    bool coffeeHeated = false;
    bool dessertReady=false;

    #endregion

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            currentInteractable = other.gameObject;
            infoPanel.SetActive(true);
        }

    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            infoPanel.SetActive(false);
            currentInteractable = null;
        }
    }

    void Start()
    {
        
    }
    void Update()
    {
        // Check for player input to interact with objects
        if (Input.GetKeyDown(KeyCode.E) && currentInteractable != null)
        {
            InteractWithObject(currentInteractable);
        }
    }

    void InteractWithObject(GameObject interactable)
    {
        customerBehaviour = interactable.GetComponent<CustomerBehaviour>();

        if (customerBehaviour == null)
        {
            Debug.Log("Cannot find customer behaviour script");
        }

        if (interactable.name=="Pizza" &&!handsFull)
        {
            coffeeHeated = false;
            holdPizza.SetActive(true);
            handsFull=true;
            infoText.text = "Microwave the pizza";
        }
        if (interactable.name == "Coffee Machine" && !handsFull)
        {
            handsFull = true;
            playerController.enabled=false;
            StartCoroutine(Prepare(coffeeHeatTime));
        }
        if (interactable.name == "Cake" && !handsFull)
        {
            coffeeHeated = false;
            holdCake.SetActive(true);
            handsFull = true;
            dessertReady=true;
        }
        if (interactable.name == "Microwave")
        {
            //microwaving
            if (holdPizza.activeSelf && !microwaveFull && !pizzaHeated)
            {
                holdPizza.SetActive(false);
                handsFull = false;
                microwaveFull = true;
                StartCoroutine(Prepare(pizzaHeatTime));
                
            }
            else if(!timePanel.activeSelf && microwaveFull)//taking out from microwave
            {
                holdPizza.SetActive(true);
                handsFull = true;
                microwaveFull = false;
            }
        }

        if (interactable.name == "CoffeeCUSTOMER(Clone)" && handsFull && coffeeHeated)
        {
            holdCoffee.SetActive(false);
            handsFull=false;

            Pay(COFFEE_COST);
            

            Debug.Log("Coffee Given");
        }
        if (interactable.name == "DessertCUSTOMER(Clone)" && handsFull && dessertReady)
        {
            holdCake.SetActive(false);
            handsFull = false;

            Pay(DESSERT_COST);
            
        }
        if (interactable.name == "PizzaCUSTOMER(Clone)" && handsFull && pizzaHeated)
        {
            holdPizza.SetActive(false);
            handsFull = false;
            
            Pay(PIZZA_COST);
            
        }
    }

    IEnumerator Prepare(float time)
    {
        Debug.Log("Preparing");
        timePanel.SetActive(true);
        for (float i = time; i >= 0; i--)
        {
            timeText.text = "" + i;
            yield return new WaitForSeconds(1f);
            Debug.Log(i);
        }
        //yield return new WaitForSeconds(time);
        timePanel.SetActive(false);

        if (time==pizzaHeatTime)
        {
            pizzaHeated=true;
            infoText.text = "Pizza Heated!";
        }
        else if (time==coffeeHeatTime)
        {
            coffeeHeated=true;
            holdCoffee.SetActive(true);
            infoText.text = "Coffee Heated!";
            playerController.enabled = true;
        }

    }

    private void Pay(int cost)
    {
        gameManager.currentMoney += cost;
        customerBehaviour.isHungry = false;
    }
}