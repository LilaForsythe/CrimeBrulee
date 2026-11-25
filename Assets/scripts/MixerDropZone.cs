using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class MixerDropZone : MonoBehaviour, IDropHandler
{
    [SerializeField] private UnityEvent onAllIngredientsAdded;

    // сколько каждого продукта лежит в миксере
    private readonly Dictionary<IngredientType, int> contents = new Dictionary<IngredientType, int>();
    private readonly int requiredTypes = System.Enum.GetValues(typeof(IngredientType)).Length;
    private bool started;

    private void Start()
    {
        Debug.Log($"[Mixer] Ready. Starts when all {requiredTypes} types are inside (any amount)");
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("[Mixer] OnDrop received");

        if (eventData.pointerDrag == null) return;

        var item = eventData.pointerDrag.GetComponent<ButtonDrag>();
        if (item == null)
        {
            Debug.LogWarning($"[Mixer] {eventData.pointerDrag.name} has no ButtonDrag, ignored");
            return;
        }

        if (!Inventory.Instance.TryConsume(item.Ingredient))
        {
            Debug.LogWarning($"[Mixer] No {item.Ingredient} in inventory, rejected");
            return;
        }

        contents[item.Ingredient] = contents.TryGetValue(item.Ingredient, out int c) ? c + 1 : 1;
        Debug.Log($"[Mixer] ACCEPTED {item.Ingredient}, now {contents[item.Ingredient]} inside. Types: {contents.Count}/{requiredTypes}");

        if (!started && contents.Count == requiredTypes)
        {
            started = true;
            Debug.Log("[Mixer] ALL TYPES PRESENT, firing event");
            onAllIngredientsAdded.Invoke();
        }
    }
}