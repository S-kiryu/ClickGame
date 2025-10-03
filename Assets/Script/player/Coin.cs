using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    [SerializeField] static private int _coin = 0;
    public List<ItemData> ownedItems = new List<ItemData>();

    public bool CanAfford(int price) => _coin >= price;

    static public void CoinUp() 
    {
        _coin++;
        Debug.Log(_coin);
    }
}
