using UnityEngine;

[CreateAssetMenu(fileName = "PurchaseAction", menuName = "ScriptableObjects/Actions/PurchaseAction")]
public class PurchaseAction : ItemAction
{
    [SerializeField] private int playerMoney = 1000; // ���̃v���C���[������

    public override void Execute(ItemData item)
    {
        if (playerMoney >= item.GetPrice())
        {
            playerMoney -= item.GetPrice();
            Debug.Log($"{item.GetItemName()} �� {item.GetPrice()}�~ �ōw�����܂����I");
            Debug.Log($"�c��: {playerMoney}�~");
        }
        else
        {
            Debug.Log("����������܂���I");
        }
    }
}