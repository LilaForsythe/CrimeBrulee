using UnityEngine;

public class Register : MonoBehaviour
{
    //public GlobalVariables globalVariables;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMouseDown()
    {
        Debug.Log("Clicking on the register");
        switch((int)(GlobalVariables.currentPhase))
        {
            case 1: GlobalVariables.currentPhase = Phases.Serving;
            //initiate serving phase
                break;
            case 2: GlobalVariables.currentPhase = Phases.Break;
            //stop any continuous serving actions
                break;
            case 3: GlobalVariables.currentPhase = Phases.TakingOrders;
            //initiate taking orders phase
                break;
            case 4: GlobalVariables.currentPhase = Phases.Closing;
            //stop any continous taking orders actions
                break;
        }

        Debug.Log("Current Phase: " + GlobalVariables.currentPhase.ToString());
    }
}
