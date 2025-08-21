using Assets.SimpleLocalization.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PrefabScriptBankMagaz : MonoBehaviour, IPrefabTowar
{
    [SerializeField] private Button buttonPrice;

    public void Instantiate(ScriptableObject to)
    {
        BankaObject t = to as BankaObject;
        gameObject.GetComponentInChildren<Image>().sprite = t.icon;

        int coin = int.Parse(LocalizationManager.Localize(t.key, t.baseInfo.priceBanka));
        if (ManagerClick.managerClick.bankaObject == null && ManagerClick.ValueSO.GetValue(ManagerClick.ValueRubName) > coin)
            buttonPrice.onClick.AddListener(() => ClickSalery(coin, t));
        else
            buttonPrice.interactable = false;

        buttonPrice.GetComponentInChildren<TextMeshProUGUI>().text = LocalizationManager.Localize(t.key, t.baseInfo.priceBanka);
    }
    private void ClickSalery(int rub, BankaObject tools)
    {
        ManagerClick.ValueSO.ModifyValue(ManagerClick.ValueRubName, -rub);
        ManagerClick.managerClick.UpdateBanka(tools);
    }
}
