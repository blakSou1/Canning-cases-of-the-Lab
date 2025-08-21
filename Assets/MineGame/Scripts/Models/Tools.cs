using System;
using UnityEngine;

[CreateAssetMenu(menuName = "RT/Tools/Data")]
public class Tools : ScriptableObject
{
    public string key;
    public BaseInfo baseInfo;

    public Sprite icon;
    public bool isOwn = false;
    public int lvl = 0;
}