using UnityEngine;
using TMPro;

public class groceryListScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public TextMeshProUGUI groceryListText;

   


    void Start()
    {
        if (groceryListText == null)
        {
            groceryListText = GetComponent<TextMeshProUGUI>();
        }
        
        UpdateGroceryListDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    class Ingredient
    {
        public string mName;
        public int mValNeeded;
        public int mValOwned;

        public Ingredient(string name, int valOwned, int valNeeded)
        {
            mName = name;
            mValNeeded = valNeeded;
            mValOwned = valOwned;
        }


    }
    private Ingredient ingredient1 = new Ingredient("Flour", 4, 32);
    private Ingredient ingredient2 = new Ingredient("Sugar", 3, 12);
    private Ingredient ingredient3;

   
    public void UpdateGroceryListDisplay()
    {
        if (groceryListText == null) { 
            groceryListText= GetComponent<TextMeshProUGUI>();
        }
        if (groceryListText != null) {
            groceryListText.text = "Grocery List: \n"
                + ingredient1.mName + " " + ingredient1.mValOwned + " / " + ingredient1.mValNeeded + "\n"
                + ingredient2.mName + " " + ingredient2.mValOwned + " / " + ingredient2.mValNeeded + "\n";
                }
    }

    public void ingredientCollected(string ingCollected)
    {
        gameManager manager = FindAnyObjectByType<gameManager>();
        ingredient1.mValOwned += 1;

        if (manager != null)
        {
            manager.totalCollectibles -= 1;
        }

        UpdateGroceryListDisplay();
        //spawn a new ingredient
    }
}
