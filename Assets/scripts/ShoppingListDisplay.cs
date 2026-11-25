using UnityEngine;
using TMPro;

public class ShoppingListDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI groceryListText;

    void Start()
    {
        if (groceryListText == null)
        {
            groceryListText = GetComponent<TextMeshProUGUI>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(groceryListText != null)
        {
            groceryListText.text = "Grocery List: \n" + IngredientStock.ShoppingListText();
        }
    }
}
