using UnityEngine;

public class ingredientScript : MonoBehaviour
{
    [SerializeField] private Ingredients ingredient;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (IngredientStock.Add(ingredient))
            {
                Destroy(gameObject);
            }
        }
    }
}
