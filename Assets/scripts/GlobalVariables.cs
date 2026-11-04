using UnityEngine;
public enum Phases {Menu, Baking, Serving, Break, TakingOrders, Closing, Groceries}
public static class GlobalVariables
{
    public static Phases currentPhase = Phases.Closing;

    public static int recipeIngredients[10][8] = {
        {1, 0, 0, 1, 1, 0, 0, 0, 0, 0},
        {1, 1, 1, 1, 1, 1, 1, 1, 1, 0},
        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
        {0, 0, 1, 0, 0, 1, 0, 0, 1, 0},
        {0, 0, 0, 1, 0, 1, 0, 1, 1, 1},
        {0, 0, 0, 1, 0, 1, 0, 1, 0, 1},
        {0, 0, 0, 0, 1, 0, 0, 1, 1, 0},
        {0, 0, 0, 0, 1, 0, 1, 1, 0, 1}
    };
}
