using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ManagerOpeningCases : MonoBehaviour
{
    public int rub = 100;
    [Header("Prefabs")] [SerializeField] private GameObject itemPrefab;
    [SerializeField] private GameObject mainPanel;

    [SerializeField] private TextMeshProUGUI startScrollButtonText;

    [Header("Win panel elements")] [SerializeField]
    private GameObject winnerPanel;

    [SerializeField] private WinElement winnerItemPrefab;

    [Header("Buttons")] [SerializeField] private Button startScrollButton;

    [Header("Settings")] [SerializeField] private float scrollSpeed;
    [SerializeField] private float startPositionPanelLeft;
    [SerializeField] private float spacingBetweenElements;

    private RectTransform m_RectTransformPanel;
    private List<GameObject> listPrefabs;
    public List<BankaObject> items = new();
    private float speed;
    private bool canScroll;
    private float widthPrefab;
    [SerializeField] private int weightItems;

    private void Awake()
    {
        widthPrefab = itemPrefab.GetComponent<ListElement>().Width();
        m_RectTransformPanel = mainPanel.GetComponent<RectTransform>();
        listPrefabs = new List<GameObject>();
    }
    private void OnEnable()
    {
        startScrollButtonText.text = "Открыть (" + rub + ")";
        if(ManagerClick.ValueSO.GetValue(ManagerClick.ValueRubName) > rub && ManagerClick.managerClick.bankaObject == null)
            startScrollButton.interactable = true;
            else
            startScrollButton.interactable = false;
            
        GenerateScrollLine();
        GenerateRandomItem();
    }

    private void FixedUpdate()
    {
        if (!canScroll) return;
        speed = Mathf.MoveTowards(speed, 0, Random.Range(190, 250) * Time.deltaTime);
        m_RectTransformPanel.offsetMin -= new Vector2(speed, 0) * Time.deltaTime;
        if (speed <= 0)
        {
            canScroll = false;
            ShowPanelWinItem(listPrefabs[GetWinItemIndex()]);
        }
    }

    public void StartScroll()
    {
        if (!canScroll)
        {
            ManagerClick.ValueSO.ModifyValue(ManagerClick.ValueRubName, -rub);

            canScroll = true;
            startScrollButton.interactable = false;
        }
    }

    private int GetWinItemIndex()
    {
        var finishPositionPanelLeft = m_RectTransformPanel.offsetMin.x;
        var rangeScrollLine = (finishPositionPanelLeft - startPositionPanelLeft) * (-1);
        var result = (int)((rangeScrollLine + 3 * widthPrefab + 2 * spacingBetweenElements +
                            spacingBetweenElements / 2) /
                            (widthPrefab + spacingBetweenElements));
        return result;
    }

    private void ShowPanelWinItem(GameObject winItem)
    {
        winnerPanel.SetActive(true);
        var winElement = winItem.GetComponent<ListElementRouletteItem>();
        var item = winElement.GetItem();
        winnerItemPrefab.SetMainImage(item.icon);
        ManagerClick.managerClick.UpdateBanka(winElement.GetItem());
    }

    public void ClickOnCLose()
    {
        ClosePanelWinner();
        GenerateRandomItem();
        startScrollButton.interactable = ManagerClick.ValueSO.GetValue(ManagerClick.ValueRubName) > rub;
    }

    public void ClosePanelWinner()
    {
        if (!winnerPanel.activeSelf) return;
        canScroll = false;
        winnerPanel.SetActive(false);
    }

    public void ExtraFinishScroll()
    {
        canScroll = false;
    }

    private void GenerateScrollLine()
    {
        if (listPrefabs.Count != 0) return;

        for (var i = 0; i < 56; i++)
        {
            var instantiate = Instantiate(itemPrefab, mainPanel.transform);
            listPrefabs.Add(instantiate);
        }
    }

    private void GenerateRandomItem()
    {
        speed = scrollSpeed;
        m_RectTransformPanel.offsetMin = new Vector2(startPositionPanelLeft, 20);

        foreach (var elementMeta in listPrefabs.Select(prefab => prefab.GetComponent<ListElementRouletteItem>()))
        {
            int indexItem = GetRandomIndex();
            elementMeta.SetItem(items[indexItem]);
            elementMeta.SetMainImage(items[indexItem].icon);
        }
    }

    private int GetRandomIndex()
    {
        int index;
        int rndWeight = Random.Range(0, weightItems);
        for (index = 0; index < items.Count && rndWeight >= 0; index++)
            rndWeight -= items[index].weight;

        if (index == items.Count) index -= 1;

        return index;
    }

    // public void ClickOnCase(List<BankaObject> items, int weightItems)
    // {
    //     this.weightItems = weightItems;
    //     startScrollButtonText.text = "Открыть (" + this.priceCase + ")";
    //     this.items = items;
    //     startScrollButton.enabled = Magaz.ValueSO.GetValue(Magaz.ValueRubName) > rub;
    //     GenerateScrollLine();
    //     GenerateRandomItem();
    // }

    public void ClearPrefabs()
    {
        if (listPrefabs == null || listPrefabs.Count == 0) return;

        foreach (var prefab in listPrefabs)
            Destroy(prefab);

        listPrefabs.Clear();
    }
}
