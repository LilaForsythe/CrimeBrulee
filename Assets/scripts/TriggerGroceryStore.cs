using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerGroceryStore : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToGroceryStore()
    {
        if (GlobalVariables.currentPhase == Phases.Closing)
        {
           GlobalVariables.currentPhase = Phases.Groceries;
           SceneManager.LoadScene("groceryStoreScene");
        }
    }
}
