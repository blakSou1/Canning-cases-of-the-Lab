using UnityEngine;

[CreateAssetMenu(menuName = "RT/BaseInfo")]
public class BaseInfo : ScriptableObject
{
    [Header("Stat")]
    public string rub;
    
    [Header("Tools")]
    #region Tools
    public string rareTools;
    public string priceTools;
    public string baseDamageTools;
    #endregion

    [Header("Banka")]
    #region Banka
    public string rareBanka;
    public string priceBanka;

    public string chanceABanka;
    public string chanceBBanka;
    public string chanceCBanka;
    public string chance2XBanka;
    public string hpBanka;
    #endregion

    [Header("Toy")]
    #region Toy
    public string rareToy;
    public string priceToy;
    #endregion

    [Header("Toy")]
    #region LvlTools
    public string lvlKey;
    public string DamagePerLvl;
    public string MaxLvl;
    #endregion

}