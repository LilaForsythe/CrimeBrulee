using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] public int totalCollectibles = 5;
    public TextMeshProUGUI timerText;
<<<<<<< HEAD
    public TextMeshProUGUI phaseText;
    [SerializeField] private Slider clockSlider; // create a slider variable to hold the reference to the slider component
    //Global Variables
    [SerializeField] private int totaltime = 10;
    [SerializeField] private int timeRemaining;
=======
    [SerializeField] private Slider clockSlider;
    [SerializeField] private int totalTime = 10;
    private int timeRemaining;
>>>>>>> KobePadillaGroceryStore

    private int ingredient1Count;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timeRemaining = totalTime;
        if (clockSlider == null)
        {
            clockSlider = GetComponent<Slider>();
        }
        if (clockSlider != null)
        {
            clockSlider.maxValue = totalTime;
            clockSlider.value = timeRemaining;
        }
        
        
        StartCoroutine(Countdown());
    }

    // Update is called once per frame
    void Update()
    {
        phaseText.text = "Current Phase: " + GlobalVariables.currentPhase.ToString();
        clockMove();
    }


    IEnumerator Countdown()
    {
        if (timerText == null)
        {
            timerText = GetComponent<TextMeshProUGUI>();
        }
        while(timeRemaining > 0 && timerText != null)
        {
            timeRemaining--;
            timerText.text = "Time Remaining: " + timeRemaining;
            yield return new WaitForSeconds(1);
        }
    }

    public void clockMove()
    {
        if (clockSlider == null) {
            clockSlider = GetComponent<Slider>();
        }
        if (clockSlider != null) {
            clockSlider.value = timeRemaining;
        }
    }
    

}
