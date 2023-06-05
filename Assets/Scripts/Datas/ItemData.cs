using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Ball,
    Bomb,
    BowlingBall,
    Coin,
    Hat,
    Magnet,
    Max
}
[System.Serializable]
public struct ItemData
{
    public ItemType type;
    public int icon;
    public int price;
    public float value;
}
public struct ItemInfo
{
    public ItemData data;
    public int count;
}