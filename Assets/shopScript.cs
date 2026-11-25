using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using Unity.Tutorials.Editor;

public class shopScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private TextMeshProUGUI buyButton;
    [SerializeField] private string upgradeName;

    void Start()
    {
        if (buyButton == null)
        {
            buyButton = GetComponent<TextMeshProUGUI>();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnMouseDown()
    {
        if (GlobalVariables.money <= 0)
        {
            Debug.Log("cant buy, not enough money");
        }
    }

    public void upgrade(string upgradeName)
    {
        if (upgradeName == "playerSpeed")
        {
            if (GlobalVariables.money > 50)
            {
                GlobalVariables.money -= 50;
                GlobalVariables.playerSpeed += 1;
                Debug.Log("Bought Upgrade");

            }
            else
            {
                Debug.Log("Not enough money to buy upgrade");
            }
        }
        if (upgradeName == "shoppingTime")
        {
            if (GlobalVariables.money > 50)
            {
                GlobalVariables.money -= 50;
                GlobalVariables.shoppingTime += 1;
                Debug.Log("Bought Upgrade");
            }
            else
            {
                Debug.Log("Not enough money to buy upgrade");
            }
        }
        if(upgradeName == "multiplierChance")
        {
            if (GlobalVariables.money > 50)
            {
                GlobalVariables.money -= 50;
                GlobalVariables.multiplierChance += 0.5f;
                Debug.Log("Bought Upgrade");
            }
            else
            {
                Debug.Log("Not enough money to buy upgrade");
            }
        }
    }
}

