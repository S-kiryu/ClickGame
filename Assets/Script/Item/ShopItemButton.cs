using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItemButton : MonoBehaviour
{
    [Header("UI�v�f")]
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private Button button;

    private ItemData itemData;
    private ShopUIManager shopManager;

    void Awake()
    {
        if (button == null)
            button = GetComponent<Button>();

        if (button != null)
            button.onClick.AddListener(OnButtonClicked);
    }

    /// <summary>
    /// �A�C�e���{�^���̏����ݒ�
    /// </summary>
    public void Setup(ItemData data, ShopUIManager manager)
    {
        itemData = data;
        shopManager = manager;

        if (iconImage != null)
            iconImage.sprite = data.GetIcon();

        if (nameText != null)
            nameText.text = data.GetItemName();

        if (priceText != null)
            priceText.text = $"{data.GetPrice()}G";

        if (levelText != null)
            levelText.text = $"Lv.{data.GetItemLv()}";
    }

    /// <summary>
    /// �{�^�����N���b�N���ꂽ�Ƃ��̏���
    /// </summary>
    void OnButtonClicked()
    {
        if (shopManager != null && itemData != null)
        {
            shopManager.OnItemSelected(itemData);
        }
    }
}