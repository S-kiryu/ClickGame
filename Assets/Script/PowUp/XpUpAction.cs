using UnityEngine;

[CreateAssetMenu(fileName = "XpUpAction", menuName = "ScriptableObjects/Actions/XpUpAction")]
public class XpUpAction : ItemAction
{
    private int _count = 1;
    private ConsecutiveHits _consecutiveHits;

    public override void Execute(ItemData item)
    {
        if (_consecutiveHits == null)
        {
            _consecutiveHits = Object.FindFirstObjectByType<ConsecutiveHits>();
        }

        if (_count % 10 != 0)
        {
            _consecutiveHits.ModifyStat("BaseExpGain", 1f);
            _count++;
        }
        else
        {
            _consecutiveHits.ModifyStat("BaseExpGain", 10f);
            _count = 1;
        }
    }
}