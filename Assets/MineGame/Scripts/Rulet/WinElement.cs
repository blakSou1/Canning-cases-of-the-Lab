using UnityEngine;
using UnityEngine.UI;

public class WinElement : MonoBehaviour
{
    [SerializeField] private Image myMainImage;

    public void SetMainImage(Sprite image) => myMainImage.sprite = image;
}
