using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopUIManager : MonoBehaviour
{
    [Header("�V���b�v�ݒ�")]
    [SerializeField] private ShopInventory shopInventory;

    [Header("UI�Q��")]
    [SerializeField] private Transform contentParent; // Scroll View��Content
    [SerializeField] private GameObject itemButtonPrefab; // �A�C�e���{�^���̃v���n�u

    [Header("�ڍ׃p�l��")]
    [SerializeField] private GameObject detailPanel;
    [SerializeField] private Image detailIcon;
    [SerializeField] private TextMeshProUGUI detailNameText;
    [SerializeField] private TextMeshProUGUI detailDescriptionText;
    [SerializeField] private TextMeshProUGUI detailPriceText;
    [SerializeField] private TextMeshProUGUI detailLevelText;
    [SerializeField] private Button buyButton;

    private ItemData selectedItem;

    void Start()
    {
        GenerateShopItems();

        if (detailPanel != null)
            detailPanel.SetActive(false);

        if (buyButton != null)
            buyButton.onClick.AddListener(OnBuyButtonClicked);
    }

    /// <summary>
    /// �V���b�v�A�C�e���̃{�^���𐶐�
    /// </summary>
    void GenerateShopItems()
    {
        if (shopInventory == null || shopInventory.items == null)
        {
            Debug.LogWarning("ShopInventory���ݒ肳��Ă��܂���");
            return;
        }

        // �����̃{�^�����N���A
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }

        // �e�A�C�e���̃{�^���𐶐�
        foreach (ItemData item in shopInventory.items)
        {
            if (item == null) continue;

            GameObject buttonObj = Instantiate(itemButtonPrefab, contentParent);
            ShopItemButton itemButton = buttonObj.GetComponent<ShopItemButton>();

            if (itemButton != null)
            {
                itemButton.Setup(item, this);
            }
            else
            {
                Debug.LogWarning($"ShopItemButton�R���|�[�l���g��������܂���: {buttonObj.name}");
            }
        }
    }

    /// <summary>
    /// �A�C�e�����I�����ꂽ�Ƃ��̏���
    /// </summary>
    public void OnItemSelected(ItemData item)
    {
        selectedItem = item;
        ShowItemDetail(item);
    }

    /// <summary>
    /// �A�C�e���̏ڍׂ�\��
    /// </summary>
    void ShowItemDetail(ItemData item)
    {
        if (detailPanel != null)
            detailPanel.SetActive(true);

        if (detailIcon != null)
            detailIcon.sprite = item.GetIcon();

        if (detailNameText != null)
            detailNameText.text = item.GetItemName();

        if (detailDescriptionText != null)
            detailDescriptionText.text = item.GetDescription();

        if (detailPriceText != null)
            detailPriceText.text = $"���i: {item.GetPrice()}G";

        if (detailLevelText != null)
            detailLevelText.text = $"���x��: {item.GetItemLv()}";
    }

    /// <summary>
    /// �w���{�^���������ꂽ�Ƃ��̏���
    /// </summary>
    void OnBuyButtonClicked()
    {
        if (selectedItem == null) return;

        // �����ɍw������������
        Debug.Log($"{selectedItem.GetItemName()}���w�����܂����I");

        // �w���������̏����������ɒǉ�
        // ��: �v���C���[�̏����������炷�A�A�C�e�����C���x���g���ɒǉ��Ȃ�
    }

    /// <summary>
    /// �ڍ׃p�l�������
    /// </summary>
    public void CloseDetailPanel()
    {
        if (detailPanel != null)
            detailPanel.SetActive(false);

        selectedItem = null;
    }
}