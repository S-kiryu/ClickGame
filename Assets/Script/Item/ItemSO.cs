using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/EnemyData", order = 1)]
public class ItemData : ScriptableObject
{
    [SerializeField]private string itemName;

    [Tooltip("�A�C�e���̐���")]
    [SerializeField] private string description;

    [Tooltip("�A�C�e���̃C���[�W")]
    [SerializeField] private Sprite icon;

    [Tooltip("�A�C�e���̒l�i")]
    [SerializeField] private int price;

    [Tooltip("�A�C�e���̃��x��")]
    [SerializeField] private int itemLv;
}
