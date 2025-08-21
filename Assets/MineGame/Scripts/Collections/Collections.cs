using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class Collections : MonoBehaviour
{
    [SerializeField] private GameObject prefabPanel;
    private GridLayoutGroup gridLayoutGroup;

    private void Awake()
    {
        gridLayoutGroup = GetComponentInChildren<GridLayoutGroup>();
        gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        foreach (AToy aToy in ManagerClick.InventoryStat.collections)
        {
            GameObject gameObject = Instantiate(prefabPanel, gridLayoutGroup.transform);
            gameObject.GetComponent<PrefabPanel>().Init(aToy);
        }
    }
    private void OnDisable()
    {
        for (int i = 0; i < gridLayoutGroup.transform.childCount; i++)
            Destroy(gridLayoutGroup.transform.GetChild(i).gameObject);
    }

}
