using UnityEngine;
using TMPro;

public class shopScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private TextMeshProUGUI buyButton;

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
}


