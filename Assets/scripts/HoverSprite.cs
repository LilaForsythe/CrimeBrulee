using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class HoverSprite : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler,
    IBeginDragHandler,
    IEndDragHandler
{
    [SerializeField] private Sprite hoverSprite;

    private Image image;
    private Sprite normalSprite;
    private bool isDragging;

    private void Awake()
    {
        image = GetComponent<Image>();
        normalSprite = image.sprite;

        if (hoverSprite == null)
            Debug.LogWarning($"[Hover] {name}: Hover Sprite is NOT assigned in the inspector");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log($"[Hover] ENTER {name}");
        if (hoverSprite != null) image.sprite = hoverSprite;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // во время перетаскивания Exit приходит из-за отключённых raycast, игнорируем его
        if (isDragging) return;

        Debug.Log($"[Hover] EXIT {name}");
        image.sprite = normalSprite;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDragging = true;
        if (hoverSprite != null) image.sprite = hoverSprite;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;
        image.sprite = normalSprite;
        Debug.Log($"[Hover] END DRAG {name}, sprite reset");
    }

    private void OnDisable()
    {
        isDragging = false;
        if (image != null) image.sprite = normalSprite;
    }
}