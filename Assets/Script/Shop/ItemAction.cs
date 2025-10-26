using UnityEngine;

public abstract class ItemAction : ScriptableObject
{
    public abstract void Execute(ItemData item);
}