using Assets.SimpleLocalization.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class PrefabPanel : MonoBehaviour
{
    [SerializeField] private GameObject toyPoint;
    [SerializeField] private Image imageToy;
    AToy a;

    public void Init(AToy aToy)
    {
        a = aToy;

        imageToy.sprite = aToy.prefabToy.GetComponent<SpriteRenderer>().sprite;
        // if (a.count == 0)
        //     GetComponentInChildren<Button>().gameObject.SetActive(false);
    }

    // public void OnSale()
    // {
    //     a.count--;
    //     ManagerClick.ValueSOStatic.ModifyValue(a.baseInfo.rub, int.Parse(LocalizationManager.Localize(a.key, a.baseInfo.priceToy)));
    //     if (a.count == 0)
    //         GetComponentInChildren<Button>().gameObject.SetActive(false);
    // }
}
