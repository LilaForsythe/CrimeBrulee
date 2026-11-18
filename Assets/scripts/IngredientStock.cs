using UnityEngine;
using System.Text;
using System.Text.RegularExpressions;

public static class IngredientStock
{
    private static readonly int ingredientCount = System.Enum.GetValues(typeof(Ingredients)).Length;
    public static int[] owned = new int[ingredientCount];

    //clears ingredients
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    static void ResetOnGameStart()
    {
        owned = new int[ingredientCount];
    }

    //if we want to make recipes harder we can change the number of each ingredient
    public static int AmountPerRecipe(Recipes recipe, Ingredients ingredient)
    {
        return GlobalVariables.recipeIngredients[(int)ingredient, (int)recipe];
    }

    //total for each ingredient
    // Total needed for every accepted order that hasn't been baked yet
    public static int Needed(Ingredients ingredient)
    {
        int total = 0;

        foreach (Order order in OrderBook.acceptedOrders)
        {
            if (order.status == OrderStatus.Waiting)
            {
                total += AmountPerRecipe(order.recipe, ingredient);
            }
        }

        return total;
    }

    public static int Owned(Ingredients ingredient)
    {
        return owned[(int)ingredient];
    }

    public static int StillNeeded(Ingredients ingredient)
    {
        return Mathf.Max(0, Needed(ingredient) - Owned(ingredient));
    }

    //returns true if all ingredients are collected - should end robbery phase
    public static bool HasEverything()
    {
        foreach (Ingredients ingredient in System.Enum.GetValues(typeof(Ingredients)))
        {
            if (StillNeeded(ingredient) > 0)
            {
                return false;
            }
        }
        return true;
    }

    //adds ingredient to owned
    public static bool Add(Ingredients ingredient)
    {
        if (StillNeeded(ingredient) <= 0)
        {
            return false;
        }
        owned[(int)ingredient] += 1;
        return true;
    }

    //clears inventory when caught
    public static void ClearAll()
    {
        for (int i = 0; i < owned.Length; i++)
        {
            owned[i] = 0;
        }
    }

    //can this recipe be made with ingredients owned
    public static bool CanBake(Recipes recipe)
    {
        foreach (Ingredients ingredient in System.Enum.GetValues(typeof(Ingredients)))
        {
            if (Owned(ingredient) < AmountPerRecipe(recipe, ingredient))
            {
                return false;
            }
        }
        return true;
    }


    //use up ingredients for recipe
    public static bool UseIngredientsFor(Recipes recipe)
    {
        if (!CanBake(recipe))
        {
            return false;
        }
        foreach (Ingredients ingredient in System.Enum.GetValues(typeof(Ingredients)))
        {
            owned[(int)ingredient] -= AmountPerRecipe(recipe, ingredient);
        }
        return true;
    }

    //gives back ingredients if scene changes mid bake
    public static void Refund(Recipes recipe)
    {
        foreach (Ingredients ingredient in System.Enum.GetValues(typeof(Ingredients)))
        {
            owned[(int)ingredient] += AmountPerRecipe(recipe, ingredient);
        }
    }

    //displays the shopping list text
    public static string ShoppingListText()
    {
        StringBuilder text = new StringBuilder();
        foreach (Ingredients ingredient in System.Enum.GetValues(typeof(Ingredients)))
        {
            int needed = Needed(ingredient);
            if (needed > 0)
            {
                text.Append(ingredientName(ingredient) + " " + Owned(ingredient) + " / " + needed);
                if (Owned(ingredient) >= needed)
                {
                    text.Append(" \u2713");
                }
                text.Append("\n");
            }
        }
        if (text.Length == 0)
        {
            return "Nothing needed yet";
        }
        return text.ToString();
    }

    //displays the ingredients with spaces between words
    static string ingredientName(Ingredients ingredient)
    {
        return Regex.Replace(ingredient.ToString(), "(\\B[A-Z])", " $1");
    }

}
