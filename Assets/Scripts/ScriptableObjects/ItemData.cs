using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemData : ScriptableObject {
    public string itemName;
    public string itemDescription;
    public Sprite itemSprite;
    public bool isStackable;
}
