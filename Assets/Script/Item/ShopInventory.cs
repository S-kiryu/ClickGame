using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "ShopInventory", menuName = "ScriptableObjects/ShopInventory")]
public class ShopInventory : ScriptableObject
{
    public List<ItemData> items = new List<ItemData>();
}
