using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public AudioSource audio;
    [SerializeField] private AudioClip fail1;
    [SerializeField] private AudioClip fail2;
    [SerializeField] private AudioClip success;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI phaseText;
    [SerializeField] private TextMeshProUGUI dayText;
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private Slider clockSlider;
    private int totalTime = GlobalVariables.shoppingTime;
    private int timeRemaining;
    private bool isTransitioning = false;
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
        
        if (GlobalVariables.currentPhase == Phases.Groceries)
        {
            clockMove();
            if (backgroundMusicScript.instance != null) //if the music is playing 
            {
                backgroundMusicScript.instance.StopMusic(); //stop the music
            }
        }
        else
        {
            if(phaseText != null)
            {
                phaseText.text = "Current Phase: " + GlobalVariables.currentPhase.ToString();
            }
            if (dayText != null)
            {
                int daysLeft = GlobalVariables.DaysUntilRent();
                if (daysLeft == 1)
                {
                    dayText.text = "Day " + GlobalVariables.day + " : Rent due tonight!";
                }
                else
                {
                    dayText.text = "Day " + GlobalVariables.day + " : Rent due in " + daysLeft + " days";
                }
            }
            if (moneyText != null)
            {
                moneyText.text = "Gold: " + GlobalVariables.money + " / " + GlobalVariables.RentDue();
            }
        }
        if (!isTransitioning && IngredientStock.HasEverything() && GlobalVariables.currentPhase == Phases.Groceries)
        {
            StartCoroutine(CompleteGroceriesPhase());
        }
    }



    IEnumerator Countdown()
    {
        
        if (timerText == null)
        {
            timerText = GetComponent<TextMeshProUGUI>();
        }
        while (timeRemaining > 0 && timerText != null)
        {
            timeRemaining--;
            timerText.text = "Time Remaining: " + timeRemaining;
            yield return new WaitForSeconds(1);
        }
        if (timeRemaining == 0) //player is caught
        {
            audio.PlayOneShot(fail1);
            //indicate that the phase is over, we can do this with text
            Debug.Log("TIMES UP");
            //wait a couple seconds
            audio.PlayOneShot(fail2);
            yield return new WaitForSeconds(5);
            if (backgroundMusicScript.instance == null) //if no music
            {
                backgroundMusicScript.instance.PlayMusic(); //play
            }
            GlobalVariables.currentPhase = Phases.Break;
            IngredientStock.ClearAll(); // clear all the ingredients you have
            //need to clear all the recipes that you have as well
            SceneManager.LoadScene("PastryShopScene");
        }
    }

    private IEnumerator CompleteGroceriesPhase()
    {
        while (timeRemaining > 0 && timerText != null && !isTransitioning)
        {
            isTransitioning = true;
            Debug.Log("YOU HAVE COLLECTED EVERYTHING YOU NEEDED");
            if (audio != null && success != null) //if there are no missing audio
            {
                audio.PlayOneShot(success); //play the success soundA
            }
            yield return new WaitForSeconds(5);
            if (backgroundMusicScript.instance == null) //if no music
            {
                backgroundMusicScript.instance.PlayMusic(); //play
            }
            GlobalVariables.currentPhase = Phases.Baking;
            SceneManager.LoadScene("PastryShopScene");
        }
    }

    public void clockMove()
    {
        if (clockSlider == null)
        {
            clockSlider = GetComponent<Slider>();
        }
        if (clockSlider != null)
        {
            clockSlider.value = timeRemaining;
        }
    }

    



}
