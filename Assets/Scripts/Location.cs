using UnityEngine;

[System.Serializable]
public class Location
{
    public string name;
    public Sprite sky;
    public Sprite ground;
    public Sprite trees;
    public Sprite mountain;
    public Cloud[] clouds;
    public int price;

    public bool isPurchased;
}
