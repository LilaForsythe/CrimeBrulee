using System;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CanvasGroup))]
public class ButtonDrag : MonoBehaviour,
    IBeginDragHandler,
    IDragHandler,
    IEndDragHandler
{
    [SerializeField] private IngredientType ingredient;
    public IngredientType Ingredient => ingredient;

    public bool IsDragging { get; private set; }
    public event Action DragEnded;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector2 originalPosition;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        Debug.Log($"[Drag] {name} ready, ingredient = {ingredient}");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        IsDragging = true;
        originalPosition = rectTransform.anchoredPosition;
        canvasGroup.blocksRaycasts = false;
        Debug.Log($"[Drag] BEGIN {name} ({ingredient}), start position {originalPosition}");
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        IsDragging = false;
        rectTransform.anchoredPosition = originalPosition;
        canvasGroup.blocksRaycasts = true;

        string under = eventData.pointerEnter != null ? eventData.pointerEnter.name : "NOTHING";
        Debug.Log($"[Drag] END {name}, released over: {under}. Returned to {originalPosition}");

        DragEnded?.Invoke();
    }
}