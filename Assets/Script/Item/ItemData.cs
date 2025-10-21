using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "ScriptableObjects/ItemData", order = 1)]
public class ItemData : ScriptableObject
{
    [SerializeField] private string itemName;
    [Tooltip("�A�C�e���̐���")]
    [SerializeField] private string description;
    [Tooltip("�A�C�e���̃C���[�W")]
    [SerializeField] private Sprite icon;
    [Tooltip("�A�C�e���̒l�i")]
    [SerializeField] private int price;
    [Tooltip("�A�C�e���̃��x��")]
    [SerializeField] private int itemLv;

    // �Q�b�^�[���\�b�h
    public string GetItemName()
    {
        return itemName != null ? itemName : "���O�Ȃ�";
    }

    public string GetDescription()
    {
        return description != null ? description : "�����Ȃ�";
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
}