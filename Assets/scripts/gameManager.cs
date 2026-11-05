using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class gameManager : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    [SerializeField] private Slider clockSlider; // create a slider variable to hold the reference to the slider component
    //Global Variables
    [SerializeField] private int totaltime = 10;
    [SerializeField] private int timeRemaining;

    private int ingredient1Count;
    private int ingredient2Count;
    private int ingredient3Count;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        if (clockSlider != null) // check if the slider variable is null
        {
            clockSlider.maxValue = totaltime; // set the maximum value of the slider to the total time
            clockSlider.value = totaltime; // set the current value of the slider to the total time
        }
        if(clockSlider == null)
        {
            clockSlider = GetComponent<Slider>(); // get the slider component from the game object and assign it to the variable

        }
        timeRemaining = totaltime; // set the time remaining to the total time
        StartCoroutine(Countdown());

    }

    // Update is called once per frame
    void Update()
    {
        clockMove();
    }

    IEnumerator Countdown()
    {
        while (timeRemaining > 0)
        {
            timeRemaining--;
            timerText.text = "Time remaining: " + timeRemaining;
            yield return new WaitForSeconds(1);
        }
    }

    public void clockMove()
    {
        if (clockSlider != null) // check if the slider variable is null
        {
            clockSlider = GetComponent<Slider>(); // get the slider component from the game object and assign it to the variable
        }
        if (clockSlider != null)
        {
            clockSlider.value = timeRemaining;
        }
    }
}
