using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using TMPro;

public class CustomerManager : MonoBehaviour
{
    //only necessary if we end up having multiple customer designs
    [SerializeField] private Customer[] customerPrefabs;

    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform counterPoint;
    [SerializeField] private Transform exitPoint;

    [SerializeField] private float timeBetweenCustomers = 1.5f;

//this is if we want to limit the recipes customers can order
    [SerializeField] private Recipes[] menu;

    [SerializeField] private GameObject acceptButton;
    [SerializeField] private GameObject denyButton;


    //add a thing to drag finished items to customer

    [SerializeField] private TextMeshProUGUI speechText;
    [SerializeField] private GameObject speechBubble;

    private Customer currentCustomer;
    private Phases lastPhase;
    private Coroutine spawnRoutine;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        HideUI();
        lastPhase = GlobalVariables.currentPhase;
        OnPhaseEntered(lastPhase);
    }

    // Update is called once per frame
    void Update()
    {
        if (GlobalVariables.currentPhase != lastPhase)
        {
            OnPhaseExited(lastPhase);
            lastPhase = GlobalVariables.currentPhase;
            OnPhaseEntered(lastPhase);
        }
    }

    //handling phase changes

    void OnPhaseEntered(Phases phase)
    {
        //brings in customers for either phase
        if (phase == Phases.TakingOrders || phase == Phases.Serving)
        {
            QueueNextCustomer(0f);
        }
    }

    void OnPhaseExited(Phases phase)
    {
        if (phase == Phases.TakingOrders || phase == Phases.Serving)
        {
            if (spawnRoutine != null)
            {
                StopCoroutine(spawnRoutine);
                spawnRoutine = null;
            }
            if (currentCustomer != null)
            {
                SendCurrentAway();
            }
        }
    }

    //spawning in customers

    void QueueNextCustomer(float delay)
    {
        if (spawnRoutine != null)
        {
            StopCoroutine(spawnRoutine);
        }

        spawnRoutine = StartCoroutine(SpawnAfter(delay));
    }

    //after a certain delay spawn in the next customer
    IEnumerator SpawnAfter(float delay)
    {
        yield return new WaitForSeconds(delay);
        spawnRoutine = null;

        if(currentCustomer != null)
        {
            yield break;
        }

        if (GlobalVariables.currentPhase == Phases.TakingOrders)
        {
            SpawnCustomer(MakeRandomOrder());
        }
        else if (GlobalVariables.currentPhase == Phases.Serving)
        {
            if (OrderBook.acceptedOrders.Count > 0)
            {
                SpawnCustomer(OrderBook.acceptedOrders[0]);
            }
            else
            {
                Debug.Log("All orders have been served!");
            }
        }
    }

    //make a random order for the customer
    Order MakeRandomOrder()
    {
        Recipes recipe;
        if (menu != null && menu.Length > 0)
        {
            recipe = menu[Random.Range(0, menu.Length)];
        }
        else
        {
            Recipes[] all = (Recipes[])System.Enum.GetValues(typeof(Recipes));
            recipe = all[Random.Range(0, all.Length)];
        }

        return new Order(Random.Range(0, customerPrefabs.Length), recipe);
    }

    //actually spawning in the customer
    void SpawnCustomer(Order order)
    {
        currentCustomer = Instantiate(customerPrefabs[order.customerLook], spawnPoint.position, Quaternion.identity);
        currentCustomer.order = order;
        currentCustomer.WalkTo(counterPoint.position, OnCustomerArrived);
    }

    //displaying the customer interaction
    void OnCustomerArrived()
    {
        if (currentCustomer == null)
        {
            return;
        }

        if (GlobalVariables.currentPhase == Phases.TakingOrders)
        {
            speechBubble.SetActive(true);
            speechText.text = "Could I get a " + RecipeName(currentCustomer.order.recipe) + ", please?";
            acceptButton.SetActive(true);
            denyButton.SetActive(true);
        }
        else if (GlobalVariables.currentPhase == Phases.Serving)
        {
            speechText.text = "I'm here to pick up my " + RecipeName(currentCustomer.order.recipe) + "!";
        }
    }

    //accepting the order
    public void AcceptOrder()
    {
        if (currentCustomer == null || GlobalVariables.currentPhase != Phases.TakingOrders)
        {
            return;
        }
        OrderBook.acceptedOrders.Add(currentCustomer.order);
        Debug.Log("Accepted. Orders so far: " + OrderBook.acceptedOrders.Count);
        SendCurrentAway();
    }

    //denying orders
    public void DeclineOrder()
    {
        if (currentCustomer == null || GlobalVariables.currentPhase != Phases.TakingOrders)
        {
            return;
        }
        SendCurrentAway();
    }

    //code for serving recipes 
    //Im not sure yet but i think they will be in the inventory below and you can only
    //drag the correct one to the current customer to serve

    void SendCurrentAway()
    {
        HideUI();
        Customer leaving = currentCustomer;
        currentCustomer = null;
        Phases phaseWhenLeft = GlobalVariables.currentPhase;

        leaving.WalkTo(exitPoint.position, () =>
        {
            Destroy(leaving.gameObject);
            if (GlobalVariables.currentPhase == phaseWhenLeft)
            {
                QueueNextCustomer(timeBetweenCustomers);
            }
        });
    }

    void HideUI()
    {
        if (acceptButton != null && denyButton != null)
        {
            acceptButton.SetActive(false);
            denyButton.SetActive(false);
        } 
        if (speechText != null) 
        {
            speechText.text = "";
        }
        if (speechBubble != null)
        {
            speechBubble.SetActive(false);
        }
    }

    // "ChocolateCroissant" -> "Chocolate Croissant"
    static string RecipeName(Recipes recipe)
    {
        return Regex.Replace(recipe.ToString(), "(\\B[A-Z])", " $1");
    }

}
