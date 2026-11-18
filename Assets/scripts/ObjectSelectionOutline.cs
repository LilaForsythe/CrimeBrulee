
using UnityEngine;

public class HoverOutline : MonoBehaviour
{
    [SerializeField] private float outlineScale = 1.1f;
    [SerializeField] private Color outlineColor = Color.yellow;

    private SpriteRenderer original;
    private SpriteRenderer outline;

    private void Awake()
    {
        // Получаем оригинальный спрайт
        original = GetComponent<SpriteRenderer>();

        // Создаём копию
        GameObject copy = new GameObject("Outline");

        copy.transform.SetParent(transform);
        copy.transform.localPosition = Vector3.zero;
        copy.transform.localRotation = Quaternion.identity;
        copy.transform.localScale = Vector3.one * outlineScale;

        // Настраиваем Sprite Renderer копии
        outline = copy.AddComponent<SpriteRenderer>();

        outline.sprite = original.sprite;
        outline.color = outlineColor;

        outline.sortingLayerID = original.sortingLayerID;
        outline.sortingOrder = original.sortingOrder - 1;

        // Изначально скрываем
        outline.enabled = false;
    }

    private void OnMouseEnter()
    {
        outline.enabled = true;
    }

    private void OnMouseExit()
    {
        outline.enabled = false;
    }
}
