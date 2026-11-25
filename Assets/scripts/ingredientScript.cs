using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;


public class ingredientScript : MonoBehaviour
{
    [SerializeField] private GameObject ingredientPrefab;
    [SerializeField] private AudioSource audio;
    [SerializeField] private AudioClip pickupSound1;
    [SerializeField] private AudioClip pickupSound2;
    [SerializeField] private Ingredients ingredient;
    [SerializeField] private Transform[] possibleLocations;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (IngredientStock.Add(ingredient))
            {
                choosePickUpSound(); //play the sound again
                respawnIngredient(); //ceck if needed again
                //Destroy(gameObject); //destroy the ingredient object

            }
        }
    }


    public void respawnIngredient()
    {
        if (IngredientStock.Needed(ingredient) > IngredientStock.Owned(ingredient))
        {   
            gameObject.SetActive(true);
            randomLocation();
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    void Start()
    {
        int needed = IngredientStock.Needed(ingredient);

        if (needed <= 0)
        {
            gameObject.SetActive(false); // Hides ingredients not on the list
            return;
        }
        
        // If needed, move it to a random reachable location

        randomLocation();
    }

    private void randomLocation()
    {
        if (possibleLocations == null || possibleLocations.Length == 0) //if no locations
        {
            return;
        }
        
        int randomIndex = Random.Range(0, possibleLocations.Length); //choose a random location
        transform.position = possibleLocations[randomIndex].position; //move the ingredient to random location
    }

    private void choosePickUpSound()
    {
        int randomSound = Random.Range(0, 2); // Randomly choose between 0 and 1
        if(randomSound == 0)
        {
            audio.PlayOneShot(pickupSound1);
        }
        else
        {
            audio.PlayOneShot(pickupSound2);
        }
    }

}