using UnityEngine;
using TMPro;

[RequireComponent(typeof(ButtonDrag), typeof(CanvasGroup))]
public class InventorySlot : MonoBehaviour
{
    [SerializeField] private int startCount = 1;   // сколько этого продукта в начале игры
    [SerializeField] private TMP_Text countText;   // можно оставить пустым

    private ButtonDrag drag;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        drag = GetComponent<ButtonDrag>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        Inventory.Instance.EnsureInitialized(drag.Ingredient, startCount);
        Inventory.Instance.Changed += OnChanged;
        drag.DragEnded += Refresh;
        Refresh();
    }

    private void OnDisable()
    {
        if (Inventory.Instance != null)
            Inventory.Instance.Changed -= OnChanged;
        drag.DragEnded -= Refresh;
    }

    private void OnChanged(IngredientType type)
    {
        if (type == drag.Ingredient) Refresh();
    }

    private void Refresh()
    {
        int count = Inventory.Instance.Get(drag.Ingredient);

        if (countText != null)
            countText.text = count.ToString();

        if (drag.IsDragging) return;

        bool has = count > 0;
        canvasGroup.alpha = has ? 1f : 0f;
        canvasGroup.blocksRaycasts = has;
        Debug.Log($"[Slot] {name} ({drag.Ingredient}) count = {count}, visible = {has}");
    }
}