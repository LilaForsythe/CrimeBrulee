using UnityEngine;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class KitchenManager : MonoBehaviour
{
    private enum Stage {Empty, Selected, Mixing, Mixed, Shaping, Shaped, Baking, Baked}

    //configuring stage increments
    [SerializeField] private int clicksToMix = 8;
    [SerializeField] private int clicksToShape = 6;
    [SerializeField] private float bakeTime = 5f;

    //add ui for cookbook interactions
    [SerializeField] private GameObject cookbookUI;

    [SerializeField] private TextMeshProUGUI orderListText;
    [SerializeField] private TextMeshProUGUI ingredientsText;

    private List<Button> orderButtons = new List<Button>();
    private Order previewOrder;

    [SerializeField] private Transform orderButtonParent;
    [SerializeField] private Button orderButtonPrefab;
    [SerializeField] private Button makeOrderButton;

    [SerializeField] private Button closeBookButton;

    [SerializeField] private Slider mixerBar;
    [SerializeField] private Slider boardBar;
    [SerializeField] private Slider ovenBar;

    //instructions for the player
    [SerializeField] private TextMeshProUGUI instructionText;
    [SerializeField] private TextMeshProUGUI rackText;

    private Order currentOrder;
    private Stage stage = Stage.Empty;
    private int clicks;
    private float ovenTimer;

    //messages for the player
    private string message = "";
    private float messageTimer;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cookbookUI.SetActive(false);
        closeBookButton.interactable = false;
        ingredientsText.text = "";
        orderListText.text = "";
        ResetBars();
    }

    // Update is called once per frame
    void Update()
    {
        //updating the timer for the oven
        if (stage == Stage.Baking)
        {
            ovenTimer += Time.deltaTime;
            ovenBar.value = Mathf.Clamp01(ovenTimer / bakeTime);
            if (ovenTimer >= bakeTime)
            {
                stage = Stage.Baked;
            }
        }

        if (messageTimer > 0f)
        {
            messageTimer -= Time.deltaTime;
        }
        UpdateTexts();
    }

    void OnDestroy()
    {
        //if kitchen scene is left mid bake return items to player
        if (currentOrder == null)
        {
            return;
        }
        if (stage == Stage.Baked)
        {
            OrderBook.FinishOrder(currentOrder); 
        }
        else if (stage != Stage.Empty && stage != Stage.Selected)
        {
            OrderBook.CancelOrder(currentOrder);
        }
    }

    //buttons

    public void ClickCookbook()
    {
        if(GlobalVariables.currentPhase != Phases.Baking)
        {
            ShowMessage("Not the time for baking");
            return;
        }
        if(stage != Stage.Empty)
        {
            ShowMessage("Finish the current pastry first");
            return;
        }
        OpenCookbook();
        //if in the correct stage open the cookbook
    }

    void OpenCookbook()
    {
        cookbookUI.SetActive(true);
        ClearOrderButtons();
        previewOrder = null;
        ingredientsText.text = "";
        makeOrderButton.interactable = false;
        closeBookButton.interactable = true;

        List<Order> waiting = OrderBook.OrdersWithStatus(OrderStatus.Waiting);
        if(waiting.Count == 0)
        {
            orderListText.text = "No orders to make";
            return;
        }

        orderListText.text = "Select an order to make";
        foreach(Order order in waiting)
        {
            Button button = Instantiate(orderButtonPrefab, orderButtonParent);
            button.GetComponentInChildren<TextMeshProUGUI>().text = recipeName(order.recipe);

            Order clickedOrder = order;
            button.onClick.AddListener(() => PreviewOrder(clickedOrder));
            orderButtons.Add(button);
        }
    }

    void PreviewOrder(Order order)
    {
        previewOrder = order;
        ingredientsText.text = recipeName(order.recipe) + "\n\nIngredients:\n" + IngredientsFor(order.recipe);

        if(IngredientStock.CanBake(order.recipe))
        {
            makeOrderButton.interactable = true;
        }
        else
        {
            makeOrderButton.interactable = false;
            ingredientsText.text += "\n(missing ingredients)";
        }
    }

    public void ClickMakeOrder()
    {
        if(previewOrder == null)
        {
            return;
        }
        Order chosenOrder = previewOrder;
        CloseCookbook();
        SelectOrder(chosenOrder);
    }
    public void CloseCookbook()
    {
        cookbookUI.SetActive(false);
        ClearOrderButtons();
        previewOrder = null;
        orderListText.text = "";
        ingredientsText.text = "";
        //close the cookbook
    }

    void ClearOrderButtons()
    {
        foreach(Button button in orderButtons)
        {
            Destroy(button.gameObject);
        }
        orderButtons.Clear();
    }
    
    string IngredientsFor(Recipes recipe)
    {
        string text = "";
        foreach(Ingredients ingredient in System.Enum.GetValues(typeof(Ingredients)))
        {
            if(IngredientStock.AmountPerRecipe(recipe, ingredient) > 0)
            {
                text += recipeName(ingredient) + "\n";
            }
        }
        return text;
    }
    

    public bool HasSelectedOrder()
    {
        return stage == Stage.Selected && currentOrder != null;
    }

    public Recipes SelectedRecipe()
    {
        return currentOrder.recipe;
    }

    public void ClickMixer()
    {
        if(GlobalVariables.currentPhase != Phases.Baking)
        {
            ShowMessage("Not the time for baking");
            return;
        }
        if(stage == Stage.Selected)
        {
            if(!OrderBook.StartOrder(currentOrder))
            {
                ShowMessage("Couldn't start order");
                return;
            }
            stage = Stage.Mixing;
            clicks = 0;
            mixerBar.value = 0f;
        }
        if(stage != Stage.Mixing)
        {
            ShowMessage("Not ready to be mixed");
            return;
        }
        clicks ++;
        mixerBar.value = (float)clicks / clicksToMix;
        
        if(clicks >= clicksToMix)
        {
            stage = Stage.Mixed;
            ShowMessage("Order mixed");
            return;
        }
        //if in selected stage change to mixing stage
        //if not in mixing send a message
        //else increase clicks and update the mixer bar
        //if clicks is enough change to mixed stage
    }

    public void ClickBoard()
    {
        if(GlobalVariables.currentPhase != Phases.Baking)
        {
            ShowMessage("Not the time for baking");
            return;
        }
        if(stage == Stage.Mixed)
        {
            stage = Stage.Shaping;
            clicks = 0;
            boardBar.value = 0f;
        }
        if(stage != Stage.Shaping)
        {
            ShowMessage("order not ready to shape");
            return;
        }
        clicks++;
        boardBar.value = (float)clicks / clicksToShape;

        if(clicks >= clicksToShape)
        {
            stage = Stage.Shaped;
            ShowMessage("Order shaped");
            return;
        }
        //if in mixed move to shaping
        //if not in shaping send error message
        //else increase clicks and update the board bar
        //if clicks is enough change to shaped stage
    }

    public void ClickOven()
    {
        if(GlobalVariables.currentPhase != Phases.Baking)
        {
            ShowMessage("Not the time for baking");
            return;
        }
        if(stage == Stage.Shaped)
        {
            stage = Stage.Baking;
            ovenTimer = 0f;
            ovenBar.value = 0f;
            return;
        }
        if(stage == Stage.Baking)
        {
            ShowMessage("order is still baking");
            return;
        }
        if(stage == Stage.Baked)
        {
            OrderBook.FinishOrder(currentOrder);
            ShowMessage(recipeName(currentOrder.recipe) + " is on the rack!");
            currentOrder = null;
            stage = Stage.Empty;
            ResetBars();
            return;
        }
        ShowMessage("Not ready to bake");
        //if in shaped move to baking
        //if in baking send message
        //if stage is baked send message and change stage to empty
    }

    public void SelectOrder(Order order)
    {
        if(stage != Stage.Empty)
        {
            ShowMessage("order already selected or in progress");
            return;
        }
        if(!IngredientStock.CanBake(order.recipe))
        {
            ShowMessage("you do not have the ingredients for this order");
            return;
        }
        currentOrder = order;
        stage = Stage.Selected;
        clicks = 0;
        ResetBars();
        ShowMessage("order selected");
        //if not can bake send message
        //else set current order to order
        //change stage to selected
        //reset clicks and bars
    }

    //functions for messages and instructions

    void UpdateTexts()
    {
        if(instructionText != null)
        {
            if(messageTimer > 0f)
            {
                instructionText.text = message;
            }
            else
            {
                instructionText.text = NextStepText();
            }
        }
        if(rackText != null)
        {
            rackText.text = RackText();
        }
    }

    string NextStepText()
    {
        switch(stage)
        {
            case Stage.Selected:
                return "Drag ingredients into mixer";
            case Stage.Mixing:
                return "Continue clicking to mix";
            case Stage.Mixed:
                return "Click the pastry board to shape the pastry";
            case Stage.Shaping:
                return "Continue clicking to shape";
            case Stage.Shaped:
                return "Click the oven to bake it";
            case Stage.Baking:
                return "Baking the " + recipeName(currentOrder.recipe);
            case Stage.Baked:
                return "The " + recipeName(currentOrder.recipe) + " is done! Click the oven to take it out";
            default:
                if(OrderBook.OrdersWithStatus(OrderStatus.Waiting).Count > 0)
                {
                    return "Click the cookbook to pick an order";
                }
                return "No orders to make";
        }
    }

    string RackText()
    {
        List<Order> ready = OrderBook.OrdersWithStatus(OrderStatus.Ready);
        string text = "Rack: \n";
        if(ready.Count == 0)
        {
            text+= "(empty)";
        }
        foreach(Order order in ready)
        {
            text += recipeName(order.recipe) + "\n";
        }
        return text;
    }
    void ShowMessage(string newMessage)
    {
        message = newMessage;
        messageTimer = 2f;
    }

    void ResetBars()
    {
        mixerBar.value = 0f;
        boardBar.value = 0f;
        ovenBar.value = 0f;
    }

    public static string recipeName(System.Enum value)
    {
        return Regex.Replace(value.ToString(), "(\\B[A-Z])", " $1");
    }
}
