using UnityEngine;
public enum Phases {Menu, Baking, Serving, Break, TakingOrders, Closing, Groceries}
public static class GlobalVariables
{
    public static Phases currentPhase = Phases.Menu;
}
