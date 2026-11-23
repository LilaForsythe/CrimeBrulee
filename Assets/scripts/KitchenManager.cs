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
        ingredientsText.text = "";
        orderListText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        //updating the timer for the oven
    }

    void OnDestroy()
    {
        //if kitchen scene is left mid bake return items to player
    }

    //buttons

    public void ClickCookbook()
    {
        if(stage == Stage.Empty)
        {
            cookbookUI.SetActive(true);
            foreach (Order order in OrderBook.acceptedOrders)
            {
                orderListText.text += order.recipe.ToString() + "\n";
            }
            //add code to select a specific order from the cookbook
            return;
        }
        //if in the correct stage open the cookbook
    }

    public void CloseCookbook()
    {
        cookbookUI.SetActive(false);
        orderListText.text = "";
        ingredientsText.text = "";
        return;
        //close the cookbook
    }

    public void ClickMixer()
    {
        if(stage == Stage.Selected)
        {
            stage = Stage.Mixing;
            clicks = 0;
            mixerBar.value = 0f;
            return;
        }
        if(stage != Stage.Mixing)
        {
            message = "no order selected";
            return;
        }
        clicks ++;
        mixerBar.value = (float)clicks / clicksToMix;
        
        if(clicks >= clicksToMix)
        {
            stage = Stage.Mixed;
            message = "order mixed";
            return;
        }
        //if in selected stage change to mixing stage
        //if not in mixing send a message
        //else increase clicks and update the mixer bar
        //if clicks is enough change to mixed stage
    }

    public void ClickBoard()
    {
        if(stage == Stage.Mixed)
        {
            stage = Stage.Shaping;
            clicks = 0;
            boardBar.value = 0f;
            return;
        }
        if(stage != Stage.Shaping)
        {
            message = "order not ready to shape";
            return;
        }
        clicks++;
        boardBar.value = (float)clicks / clicksToShape;

        if(clicks >= clicksToShape)
        {
            stage = Stage.Shaped;
            message = "order shaped";
            return;
        }
        //if in mixed move to shaping
        //if not in shaping send error message
        //else increase clicks and update the board bar
        //if clicks is enough change to shaped stage
    }

    public void ClickOven()
    {
        if(stage == Stage.Shaped)
        {
            stage = Stage.Baking;
            ovenTimer = 0f;
            ovenBar.value = 0f;
            return;
        }
        if(stage == Stage.Baking)
        {
            message = "order is still baking";
            return;
        }
        if(stage == Stage.Baked)
        {
            stage = Stage.Empty;
            message = "order baked and ready to continue";
            return;
        }
        //if in shaped move to baking
        //if in baking send message
        //if stage is baked send message and change stage to empty
    }

    public void SelectOrder(Order order)
    {
        if(stage != Stage.Empty)
        {
            message = "order already selected or in progress";
            return;
        }
        if(!IngredientStock.CanBake(order.recipe))
        {
            message = "you do not have the ingredients for this order";
            return;
        }
        currentOrder = order;
        stage = Stage.Selected;
        clicks = 0;
        mixerBar.value = 0f;
        boardBar.value = 0f;
        ovenBar.value = 0f;
        message = "order selected";
        //if not can bake send message
        //else set current order to order
        //change stage to selected
        //reset clicks and bars
    }

    //functions for messages and instructions
}
