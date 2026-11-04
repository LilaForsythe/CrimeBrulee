using UnityEngine;
public enum Phases {Menu, Baking, Serving, Break, TakingOrders, Closing, Groceries}
public enum Ingredients {MixedBerries, Butter, Flour, Chocolate, Eggs, Milk, Cinnamon, Sugar}
public enum Recipes {FruitTart, Croissant, ChocolateCroissant, BerryEnvelope, BerryStrudel, Eclair, Palmier, CinnamonRoll, ChocolateChipCookies, VanillaCake}
public class GlobalVariables : MonoBehaviour
{
    public static Phases currentPhase = Phases.TakingOrders;

    void Start()
    {
        currentPhase = Phases.TakingOrders;
    }

    public int [,] recipeIngredients = {
        //Fruit tart, Croissant, Chocolate Croissant, Berry Envelope, Berry Strudel, Eclair, Palmier, Cinnamon Roll, Chocolate Chip Cookies, Vanilla Cake
        {1, 0, 0, 1, 1, 0, 0, 0, 0, 0}, //Mixed Berries
        {1, 1, 1, 1, 1, 1, 1, 1, 1, 0}, //Butter
        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1}, //Flour
        {0, 0, 1, 0, 0, 1, 0, 0, 1, 0}, //Chocolate
        {0, 0, 0, 1, 0, 1, 0, 1, 1, 1}, //Eggs
        {0, 0, 0, 1, 0, 1, 0, 1, 0, 1}, //Milk
        {0, 0, 0, 0, 1, 0, 0, 1, 1, 0}, //Cinnamon
        {0, 0, 0, 0, 1, 0, 1, 1, 0, 1}  //Sugar
    };
}
