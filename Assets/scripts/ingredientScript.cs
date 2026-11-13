using UnityEngine;

public class ingredientScript : MonoBehaviour
{
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

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            groceryListScript groceryList = FindAnyObjectByType<groceryListScript>();
            if (groceryList != null)
            {
                groceryList.ingredientCollected(ingredient1.mName);
                Debug.Log("Collected Ingredient");  
            }
        }
        Destroy(gameObject);
    }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
