using UnityEngine;
public enum Phases {Menu, Baking, Serving, Break, TakingOrders, Closing, Groceries}
public enum Ingredients {MixedBerries, Butter, Flour, Chocolate, Eggs, Milk, Cinnamon, Sugar}
public enum Recipes {FruitTart, Croissant, ChocolateCroissant, BerryEnvelope, BerryStrudel, Eclair, Palmier, CinnamonRoll, ChocolateChipCookies, VanillaCake}
public class GlobalVariables : MonoBehaviour
{
    public static Phases currentPhase;


    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    static void ResetOnGameStart()
    {
        currentPhase = Phases.Break;
        Debug.Log("Phase reset to Break");
    }
    //upgradables
    [SerializeField] public static int shoppingTime = 10;
    [SerializeField] public static int playerSpeed = 10;
    [SerializeField] public static int multiplierChance = 1;

    public static readonly int[,] recipeIngredients = {
        //Fruit tart, Croissant, Chocolate Croissant, Berry Envelope, Berry Strudel, Eclair, Palmier, Cinnnamon Roll, Chocolate Chip Cookies, Vanilla Cake
        {1, 0, 0, 1, 1, 0, 0, 0, 0, 0}, //Mixed Berries
        {1, 1, 1, 1, 1, 1, 1, 1, 1, 0}, //Butter
        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1}, //Flour
        {0, 0, 1, 0, 0, 1, 0, 0, 1, 0}, //Chocolate
        {0, 0, 0, 1, 0, 1, 0, 1, 1, 1}, //Eggs
        {0, 0, 0, 1, 0, 1, 0, 1, 0, 1}, //Milk
        {0, 0, 0, 0, 1, 0, 0, 1, 1, 0}, //
        {0, 0, 0, 0, 1, 0, 1, 1, 0, 1}  //Sugar
    };
}
