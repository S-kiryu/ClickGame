using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "ScriptableObjects/ItemData", order = 1)]
public class ItemData : ScriptableObject
{
    [SerializeField]private string itemName;

    [Tooltip("アイテムの説明")]
    [SerializeField] private string description;

    [Tooltip("アイテムのイメージ")]
    [SerializeField] private Sprite icon;

    [Tooltip("アイテムの値段")]
    [SerializeField] private int price;

    [Tooltip("アイテムのレベル")]
    [SerializeField] private int itemLv;
}
