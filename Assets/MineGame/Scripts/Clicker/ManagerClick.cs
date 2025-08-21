using System.Collections.Generic;
using System.Linq;
using Assets.SimpleLocalization.Scripts;
using RGame.ScriptableCoreKit;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

public class ManagerClick : MonoBehaviour
{
    #region Param

    #region Click
    private bool isClick = true;
    private Vector2 position;
    public LayerMask uiLayer;
    #endregion

    [SerializeField] private CommonStatRuntimeSO ValueSOE;
    public static CommonStatRuntimeSO ValueSO;

    [SerializeField] private string valueName;
    public static string ValueRubName;

    public static ManagerClick managerClick;

    public static Inventory InventoryStat;
    [SerializeField] private Inventory inventory;
    [SerializeField] private string RareToyA, RareToyB, RareToyC;
    [Inject] private InputPlayer input;
    private AdvancedHPBar advancedHPBar;

    #region panelPickUp
    public static GameObject openPanelButtonStat;
    [SerializeField] private GameObject openPanelButton;
    [SerializeField] private Button ButtonPuckUp;
    #endregion

    #region Tool
    public static Tools tools;
    [SerializeField] private Tools Tools;
    private static int damage;
    #endregion

    #region Pot
    [Header("Pot")]
    public BankaObject bankaObject;
    private PrefabScriptBank objectPot;
    [SerializeField] private Transform spawnPotPos;
    #endregion
    #endregion

    private void Awake()
    {
        ValueRubName = valueName;
        managerClick = this;

        openPanelButton.SetActive(false);

        ValueSO = ValueSOE;
        openPanelButtonStat = openPanelButton;
        tools = Tools;

        InventoryStat = inventory;

        advancedHPBar = FindAnyObjectByType<AdvancedHPBar>();

        input.Player.Attack.started += i => Click();
        input.Player.Position.performed += i => position = i.ReadValue<Vector2>();

        advancedHPBar.OpenBank += OpenBank;

        UpdateBanka(bankaObject);

        UpdateTools(tools);
    }
    public void UpdateBanka(BankaObject banka)
    {
        isClick = true;

        bankaObject = banka;

        advancedHPBar.bankaObject = bankaObject;
        advancedHPBar.UpdateBanka();
        CreatePot(bankaObject);
    }
    private void CreatePot(BankaObject banka)
    {
        ButtonPuckUp.onClick.RemoveAllListeners();

        bankaObject = banka;
        Destroy(objectPot);
        objectPot = Instantiate(banka.prefabBank, spawnPotPos);
    }
    public static void UpdateTools(Tools tool)
    {
        tools = tool;

        string key = tools.baseInfo.lvlKey + LocalizationManager.Localize(tools.key, tools.baseInfo.rareTools);
        damage = int.Parse(LocalizationManager.Localize(tools.key, tools.baseInfo.baseDamageTools)) + int.Parse(LocalizationManager.Localize(key, tools.baseInfo.DamagePerLvl));
    }
    private void OnDestroy()
    {
        advancedHPBar.OpenBank -= OpenBank;
        input.Player.Attack.started -= i => Click();
        input.Player.Position.performed -= i => position = i.ReadValue<Vector2>();
        input.Disable();
    }
    private void Click()
    {
        if (isClick)
            if(RaycastUI())
                advancedHPBar.TakeDamage(damage);
    }
    private bool RaycastUI()
    {
        RaycastHit2D hit = Physics2D.Raycast(
            origin: Camera.main.ScreenToWorldPoint(position), 
            direction: Vector2.zero, 
            distance: 0.1f, 
            layerMask: uiLayer
        );
        if(hit.collider != null)
            return true;

        return false;
    }
    private void OpenBank()
    {
        string rare = GetRandomLoot();
        bool is2X = IsDoubleLoot();

        List<AToy> items = InventoryStat.Listitem.Where(item => LocalizationManager.Localize(item.key, item.baseInfo.rareToy)
        == rare).ToList();
        AToy aToy = items[Random.Range(0, items.Count)];

        inventory.collections.Add(aToy);

        ButtonPuckUp.onClick.AddListener(objectPot.ButtonPuckUp);

        objectPot.finalToy = aToy;
        objectPot.StartAnimation(aToy);
        isClick = false;

        bankaObject = null;
    }
    
    // Метод для получения случайного лута
    private string GetRandomLoot()
    {
        int randomValue = Random.Range(1, 101);

        if (randomValue <= int.Parse(LocalizationManager.Localize(bankaObject.key, bankaObject.baseInfo.chanceCBanka)))
            return RareToyC;

        if (randomValue <= int.Parse(LocalizationManager.Localize(bankaObject.key, bankaObject.baseInfo.chanceBBanka)))
            return RareToyB;

        if (randomValue <= int.Parse(LocalizationManager.Localize(bankaObject.key, bankaObject.baseInfo.chanceABanka)))
            return RareToyA;

        return RareToyA;
    }

    // Метод для проверки удвоения лута
    private bool IsDoubleLoot()
    {
        if (int.Parse(LocalizationManager.Localize(bankaObject.key, bankaObject.baseInfo.chance2XBanka)) == 0)
            return false;

        int randomValue = Random.Range(1, 101);

        if (randomValue <= int.Parse(LocalizationManager.Localize(bankaObject.key, bankaObject.baseInfo.chance2XBanka)))
            return true;

        return false;
    }
}
