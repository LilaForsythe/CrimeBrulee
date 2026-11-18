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
        //if in the correct stage open the cookbook
    }

    public void ClickMixer()
    {
        //if in selected stage change to mixing stage
        //if not in mixing send a message
        //else increase clicks and update the mixer bar
        //if clicks is enough change to mixed stage
    }

    public void ClickBoard()
    {
        //if in mixed move to shaping
        //if not in shaping send error message
        //else increase clicks and update the board bar
        //if clicks is enough change to shaped stage
    }

    public void ClickOven()
    {
        //if in shaped move to baking
        //if in baking send message
        //if stage is baked send message and change stage to empty
    }

    public void SelectOrder(Order order)
    {
        //if not can bake send message
        //else set current order to order
        //change stage to selected
        //reset clicks and bars
    }

    //functions for messages and instructions
}
