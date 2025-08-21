using Assets.SimpleLocalization.Scripts;
using UnityEngine;

public class PrefabScriptBank : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private string nameAnimationOpenBank;
    [SerializeField] private Transform animationToy;
    [HideInInspector] public AToy finalToy;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void StartAnimation(AToy aToy)
    {
        GameObject gameObject = Instantiate(aToy.prefabToy, animationToy);
        animator.Play(nameAnimationOpenBank);
    }

    public void ButtonPuckUp()
    {
        ManagerClick.ValueSO.ModifyValue(finalToy.baseInfo.rub, int.Parse(LocalizationManager.Localize(finalToy.key, finalToy.baseInfo.priceToy)));
        ManagerClick.openPanelButtonStat.SetActive(false);
        Destroy(gameObject);
    }

    public void OpenPanelButton()
    {
        ManagerClick.openPanelButtonStat.SetActive(true);
    }
}
