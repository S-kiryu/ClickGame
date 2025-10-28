using UnityEngine;

[CreateAssetMenu(fileName = "CriticalRateAction", menuName = "ScriptableObjects/Actions/CriticalRateAction")]
public class CriticalRateAction : ItemAction
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
            _consecutiveHits.ModifyStat("CriticalRate", 1f);
            _count++;
        }
        else
        {
            _consecutiveHits.ModifyStat("CriticalRate", 10f);
            _count = 1;
        }
    }
}