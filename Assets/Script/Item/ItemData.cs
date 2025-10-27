using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
    [Header("�A�N�V�����ݒ�")]
    [Tooltip("���̃A�C�e�����N���b�N���ꂽ���Ɏ��s�����X�N���v�g")]
    [SerializeField] private List<ItemAction> itemActions = new List<ItemAction>();

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