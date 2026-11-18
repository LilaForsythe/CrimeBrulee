using UnityEngine;
using System.Collections.Generic;

public enum OrderStatus {Waiting, InProgress, Ready}

[System.Serializable]
public class Order
{
    //might delete if we only have time to make one customer design
    public int customerLook;

    public Recipes recipe;
    public OrderStatus status;

    public Order(int customerLook, Recipes recipe)
    {
        this.customerLook = customerLook;
        this.recipe = recipe;
        this.status = OrderStatus.Waiting;
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


    public static List<Order> OrdersWithStatus(OrderStatus status)
    {
        List<Order> result = new List<Order>();
        foreach(Order order in acceptedOrders)
        {
            if(order.status == status)
            {
                result.Add(order);
            }
        }
        return result;
    }


    //starts the order and uses up the ingredients
    public static bool StartOrder(Order order)
    {
        if (order == null || order.status != OrderStatus.Waiting || !acceptedOrders.Contains(order))
        {
            return false;
        }
        if (!IngredientStock.UseIngredientsFor(order.recipe))
        {
            return false;
        }
        order.status = OrderStatus.InProgress;
        return true;
    }

    //changing the status to reeady once it is taken out of the oven
    public static void FinishOrder(Order order)
    {
        if (order != null && order.status == OrderStatus.InProgress)
        {
            order.status = OrderStatus.Ready;
        }
    }

    //if the player changes scenes mid bake
    public static void CancelOrder(Order order)
    {
        if (order != null && order.status == OrderStatus.InProgress)
        {
            IngredientStock.Refund(order.recipe);
            order.status = OrderStatus.Waiting;
        }
    }
}