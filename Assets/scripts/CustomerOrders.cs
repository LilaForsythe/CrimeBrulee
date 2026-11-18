using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Order
{
    //might delete if we only have time to make one customer design
    public int customerLook;

    public Recipes recipe;

    public Order(int customerLook, Recipes recipe)
    {
        this.customerLook = customerLook;
        this.recipe = recipe;
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
}