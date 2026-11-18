using UnityEngine;
using TMPro;

public class ShoppingListDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI groceryListText;

    // Update is called once per frame
    void Update()
    {
        if(groceryListText != null)
        {
            groceryListText.text = "Grocery List: \n" + IngredientStock.ShoppingListText();
        }
    }
}
