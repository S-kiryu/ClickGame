using UnityEngine;

[CreateAssetMenu(fileName = "PurchaseAction", menuName = "ScriptableObjects/Actions/PurchaseAction")]
public class PurchaseAction : ItemAction
{

    public override void Execute(ItemData item)
    {
        if (Items._coin >= item.GetPrice())
        {
            Items._coin -= item.GetPrice();
            Debug.Log($"{item.GetItemName()} ‚ğ {item.GetPrice()}‰~ ‚Åw“ü‚µ‚Ü‚µ‚½I");
            Debug.Log($"c‹à: {Items._coin}G");
        }
        else
        {
            Debug.Log("‚¨‹à‚ª‘«‚è‚Ü‚¹‚ñI");
        }
    }
}