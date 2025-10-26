using UnityEngine;

[CreateAssetMenu(fileName = "PurchaseAction", menuName = "ScriptableObjects/Actions/PurchaseAction")]
public class PurchaseAction : ItemAction
{
    [SerializeField] private int playerMoney = 1000; // 仮のプレイヤー所持金

    public override void Execute(ItemData item)
    {
        if (playerMoney >= item.GetPrice())
        {
            playerMoney -= item.GetPrice();
            Debug.Log($"{item.GetItemName()} を {item.GetPrice()}円 で購入しました！");
            Debug.Log($"残金: {playerMoney}円");
        }
        else
        {
            Debug.Log("お金が足りません！");
        }
    }
}