using UnityEngine;
using UnityEngine.SceneManagement;
public enum Phases {Menu, Baking, Serving, Break, TakingOrders, Closing, Groceries}
public enum Ingredients {MixedBerries, Butter, Flour, Chocolate, Eggs, Milk, Cinnamon, Sugar}
public enum Recipes {FruitTart, Croissant, ChocolateCroissant, BerryEnvelope, BerryStrudel, Eclair, Palmier, CinnamonRoll, ChocolateChipCookies, VanillaCake}
public class GlobalVariables : MonoBehaviour
{
    public static Phases currentPhase;
    public const int baseRent = 1000;
    //maybe increase each time caught to allow for some leyway at the beginning
    public const int CaughtFine = 100;
    public const int PricePerIngredient = 25;

    public static int day = 1;
    public static int money = 0;
    public static int timesCaught = 0;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    static void ResetOnGameStart()
    {
        ResetAll();
    }

    public static void ResetAll()
    {
        currentPhase = Phases.Break;
        Debug.Log("Phase reset to Break");
        day = 1;
        money = 0;
        timesCaught = 0;
        OrderBook.acceptedOrders.Clear();
        IngredientStock.ClearAll(); 
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
        {0, 0, 0, 0, 1, 0, 0, 1, 0, 0}, //Cinnamon
        {0, 0, 0, 0, 1, 0, 1, 1, 0, 1}  //Sugar
    };

    public static int Week()
    {
        return (day - 1) / 7 + 1;
    }
    public static int RentDue()
    {
        return (int)(baseRent * Mathf.Sqrt(Week()));
    }

    public static int DaysUntilRent()
    {
        return 7 - ((day - 1) % 7);
    }

    public static int PriceOf(Recipes recipe)
    {
        int ingredientCount = 0;
        foreach(Ingredients ingredient in System.Enum.GetValues(typeof(Ingredients)))
        {
            ingredientCount += IngredientStock.AmountPerRecipe(recipe, ingredient);
        }
        return ingredientCount * PricePerIngredient;
    }

    //adds money to the amount of money the player has
    public static void AddMoney(int amount)
    {
        money += amount;
    }

    public static void PayFine()
    {
        timesCaught++;
        money = Mathf.Max(0, money - CaughtFine);
    }

    //called at the end of every day after the robbery
    public static bool EndDay()
    {
        if(day % 7 == 0)
        {
            if(money < RentDue())
            {
                return false;
            }
            money -= RentDue();
        }
        day++;
        return true;
    }

    //add to script that is called when robbery is done
    //if esacped call FinishDay(Phases.Baking)
    //if caught call FinishDay(Phases.Break)
    //if caught also pay fine and clear ingredients and orders

    // Ran out of time: lose orders and ingredients, pay a fine, no baking/serving tomorrow
    public static void GetCaught()
    {
        Debug.Log("Caught by the police!");
        OrderBook.acceptedOrders.Clear();
        IngredientStock.ClearAll();
        PayFine();
        FinishDay(Phases.Break);
    }

    // Collected everything: bake tomorrow (or skip to Break if no orders were taken)
    public static void Escape()
    {
        Debug.Log("Got away with everything!");
        if (OrderBook.acceptedOrders.Count > 0)
        {
            FinishDay(Phases.Baking);
        }
        else
        {
            FinishDay(Phases.Break);
        }
    }
    public static void FinishDay(Phases nextPhase)
    {
        if(!GlobalVariables.EndDay())
        {
            GlobalVariables.currentPhase = Phases.Menu;
            SceneManager.LoadScene("GameOverScene");
            return;
        }
        GlobalVariables.currentPhase = nextPhase;
        SceneManager.LoadScene("pastryShopScene");
    }
}
