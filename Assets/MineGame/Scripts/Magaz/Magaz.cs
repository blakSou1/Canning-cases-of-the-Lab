using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Magaz : MonoBehaviour
{
    [SerializeField] private GameObject prefabTovar;
    [SerializeField] private List<ScriptableObject> tovar;
    [SerializeField] private Transform parent;

    private void OnEnable()
    {
        foreach (ScriptableObject tool in tovar)
        {
            GameObject gameObject = Instantiate(prefabTovar, parent);
            gameObject.GetComponent<IPrefabTowar>().Instantiate(tool);
        }
    }
    public void UpdatePanel()
    {
        List<IPrefabTowar> tow = parent.GetComponentsInChildren<IPrefabTowar>().ToList();

        foreach (IPrefabTowar tool in tow)
            tool.Instantiate((tool as PrefabScriptTools).t);

    }
    private void OnDisable()
    {
        for (int i = 0; i < parent.childCount; i++)
            Destroy(parent.GetChild(i).gameObject);
    }
}