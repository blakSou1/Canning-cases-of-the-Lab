using Assets.SimpleLocalization.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PrefabScriptTools : MonoBehaviour, IPrefabTowar
{
    [SerializeField] private Button buttonPrice;
    [SerializeField] private Button buttonV;
    public Tools t;

    public void Instantiate(ScriptableObject to)
    {
        buttonV.onClick.RemoveAllListeners();
        buttonPrice.onClick.RemoveAllListeners();
        
        t = to as Tools;

        gameObject.GetComponentInChildren<Image>().sprite = t.icon;

        int coin = int.Parse(LocalizationManager.Localize(t.key, t.baseInfo.priceTools));

        if (!t.isOwn)
        {
            buttonV.interactable = false;
            buttonPrice.GetComponentInChildren<TextMeshProUGUI>().text = LocalizationManager.Localize(t.key, t.baseInfo.priceTools);

            if (ManagerClick.ValueSO.GetValue(ManagerClick.ValueRubName) < coin)
            {
                buttonPrice.interactable = false;
                return;
            }
            buttonPrice.interactable = true;
            buttonPrice.onClick.AddListener(() => ClickSalery(coin, t));
        }
        else
        {
            string key = t.baseInfo.lvlKey + LocalizationManager.Localize(t.key, t.baseInfo.rareTools);
            if (int.Parse(LocalizationManager.Localize(key, t.baseInfo.MaxLvl)) < t.lvl && ManagerClick.ValueSO.GetValue(ManagerClick.ValueRubName) > coin)
            {
                buttonPrice.interactable = true;
                buttonPrice.onClick.AddListener(() => ClickUpdate(coin, t));
            }
            else
                buttonPrice.interactable = false;

            if (ManagerClick.tools != t)
            {
                buttonV.interactable = true;   
                buttonV.onClick.AddListener(() => ClickV(t));
            }
            else
                buttonV.interactable = false;
        }
    }
    private void ClickSalery(int rub, Tools tools)
    {
        tools.isOwn = true;
        ManagerClick.ValueSO.ModifyValue(ManagerClick.ValueRubName, -rub);
        UpdatePanel();
    }
    private void ClickUpdate(int rub, Tools tools)
    {
        tools.isOwn = true;
        ManagerClick.ValueSO.ModifyValue(ManagerClick.ValueRubName, -rub);
        tools.lvl++;
        ManagerClick.UpdateTools(tools);
        UpdatePanel();
    }
    private void ClickV(Tools tools)
    {
        ManagerClick.UpdateTools(tools);
        UpdatePanel();
    }

    private void UpdatePanel()
    {
        Magaz magaz = GetComponentInParent<Magaz>();
        magaz.UpdatePanel();
    }
}
public interface IPrefabTowar
{
    public abstract void Instantiate(ScriptableObject to);

}