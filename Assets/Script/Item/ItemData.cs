using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "ItemData", menuName = "ScriptableObjects/ItemData", order = 1)]
public class ItemData : ScriptableObject
{
    [SerializeField] private string itemName;
    [Tooltip("アイテムの説明")]
    [SerializeField] private string description;
    [Tooltip("アイテムのイメージ")]
    [SerializeField] private Sprite icon;
    [Tooltip("アイテムの値段")]
    [SerializeField] private int price;
    [Tooltip("アイテムのレベル")]
    [SerializeField] private int itemLv;
    [Header("アクション設定")]
    [Tooltip("このアイテムがクリックされた時に実行されるスクリプト")]
    [SerializeField] private List<ItemAction> itemActions = new List<ItemAction>();

    // ゲッターメソッド
    public string GetItemName()
    {
        return itemName != null ? itemName : "名前なし";
    }

    public string GetDescription()
    {
        return description != null ? description : "説明なし";
    }

    public Sprite GetIcon()
    {
        return icon;
    }

    public int GetPrice()
    {
        return price;
    }

    public int GetItemLv()
    {
        return itemLv;
    }

    public void ExecuteAction()
    {
        if (itemActions != null && itemActions.Count > 0)
        {
            foreach (var action in itemActions)
            {
                if (action != null)
                {
                    action.Execute(this);
                }
            }
        }
    }
}