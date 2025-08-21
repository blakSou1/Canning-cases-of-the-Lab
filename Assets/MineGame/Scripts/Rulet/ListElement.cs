using UnityEngine;
using UnityEngine.UI;

public abstract class ListElement : MonoBehaviour
{
    [Header("Images")] [SerializeField] private Image mainImage;

    public void SetMainImage(Sprite image) => mainImage.sprite = image;

    public float Width() => GetComponent<RectTransform>().rect.width;
}
