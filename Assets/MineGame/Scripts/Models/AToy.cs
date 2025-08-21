using UnityEngine;

[CreateAssetMenu(menuName = "RT/Toy/Data")]
public class AToy : BaseObj
{
    [System.NonSerialized] public int count;
    public GameObject prefabToy;
}