using UnityEngine;

[CreateAssetMenu(fileName = "PurchaseAction", menuName = "ScriptableObjects/Actions/PurchaseAction")]
public class PurchaseAction : ItemAction
{

    public override void Execute(ItemData item)
    {
        if (Items._coin >= item.GetPrice())
        {
            Items._coin -= item.GetPrice();
            Debug.Log($"{item.GetItemName()} �� {item.GetPrice()}�~ �ōw�����܂����I");
            Debug.Log($"�c��: {Items._coin}G");
        }
        else
        {
            Debug.Log("����������܂���I");
        }
    }
}