using UnityEngine;

[CreateAssetMenu(fileName = "ingredientobjScript", menuName = "Scriptable Objects/ingredientobjScript")]
public class ingredientobjScript : ScriptableObject
{
    
    public string ingredientName; // Name of the ingredient
    public int valueNeeded; // Amount of the ingredient needed
    public int valueOwned; // Amount of the ingredient owned

    public GameObject ingredientPrefab; // Prefab of the ingredient object

    
}
