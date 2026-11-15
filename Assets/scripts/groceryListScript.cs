using UnityEngine;
using TMPro;

public class groceryListScript : MonoBehaviour
{
    public TextMeshProUGUI groceryListText;

    class Ingredient
    {
        public string type;
        public int valueNeeded;
        public int valueOwned;
         
        public Ingredient(string type, int valueNeeded, int valueOwned)
        {
            this.type = type;
            this.valueNeeded = valueNeeded;
            this.valueOwned = valueOwned;
        }
       
    }
    private Ingredient ingredient1;
    private Ingredient ingredient2;
    private Ingredient ingredient3;
    private Ingredient ingredient4;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        if (groceryListText == null)
        {
            groceryListText = GetComponent<TextMeshProUGUI>();
        }

        ingredient1 = new Ingredient("Tomatoes", 5, 0);
        ingredient2 = new Ingredient("Onions", 3, 1);
        UpdateGroceryListDisplay(); // Refresh the text on the screen!
        }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void UpdateGroceryListDisplay()
    {
        if (groceryListText != null)
        {
            groceryListText.text = "Grocery List: \n"
                + ingredient1.type + " " + ingredient1.valueOwned + " / " + ingredient1.valueNeeded + "\n"
                + ingredient2.type + " " + ingredient2.valueOwned + " / " + ingredient2.valueNeeded;
        }
    }

    public void collectTomato() //change the function to be able to collect any ingredient, not just tomatoes
    {
        ingredient1.valueOwned++; // Increase the owned count
        UpdateGroceryListDisplay(); // Refresh the text on the screen!
    }
}

