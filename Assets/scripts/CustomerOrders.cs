using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Order
{
    //might delete if we only have time to make one customer design
    public int customerLook;

    public Recipes recipe;
    public bool baked;

    public Order(int customerLook, Recipes recipe)
    {
        this.customerLook = customerLook;
        this.recipe = recipe;
        this.baked = false;
    }
}

public static class OrderBook
{
    public static List<Order> acceptedOrders = new List<Order>();

    //resets once you restart the game

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    static void ResetOnGameStart()
    {
        acceptedOrders = new List<Order>();
    }

    //orders that havent been baked
    public static List<Order> UnbakedOrders()
    {
        List<Order> result = new List<Order>();
        foreach (Order order in acceptedOrders)
        {
            if (!order.baked)
            {
                result.Add(order);
            }
        }
        return result;
    }

    //the kitchen calls this to indicate that an order is being baked
    public static bool BakeOrder(Order order)
    {
        if (order == null || order.baked || !acceptedOrders.Contains(order))
        {
            return false;
        }
        if (!IngredientStock.UseIngredientsFor(order.recipe))
        {
            return false;
        }
        order.baked = true;
        return true;
    }

}