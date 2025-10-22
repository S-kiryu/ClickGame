using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopUIManager : MonoBehaviour
{
    [Header("ショップ設定")]
    [SerializeField] private ShopInventory shopInventory;

    [Header("UI参照")]
    [SerializeField] private Transform contentParent; // Scroll ViewのContent
    [SerializeField] private GameObject itemButtonPrefab; // アイテムボタンのプレハブ

    [Header("詳細パネル")]
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
    /// ショップアイテムのボタンを生成
    /// </summary>
    void GenerateShopItems()
    {
        if (shopInventory == null || shopInventory.items == null)
        {
            Debug.LogWarning("ShopInventoryが設定されていません");
            return;
        }

        // 既存のボタンをクリア
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }

        // 各アイテムのボタンを生成
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
                Debug.LogWarning($"ShopItemButtonコンポーネントが見つかりません: {buttonObj.name}");
            }
        }
    }

    /// <summary>
    /// アイテムが選択されたときの処理
    /// </summary>
    public void OnItemSelected(ItemData item)
    {
        selectedItem = item;
        ShowItemDetail(item);
    }

    /// <summary>
    /// アイテムの詳細を表示
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
            detailPriceText.text = $"価格: {item.GetPrice()}G";

        if (detailLevelText != null)
            detailLevelText.text = $"レベル: {item.GetItemLv()}";
    }

    /// <summary>
    /// 購入ボタンが押されたときの処理
    /// </summary>
    void OnBuyButtonClicked()
    {
        if (selectedItem == null) return;

        // ここに購入処理を実装
        Debug.Log($"{selectedItem.GetItemName()}を購入しました！");

        // 購入成功時の処理をここに追加
        // 例: プレイヤーの所持金を減らす、アイテムをインベントリに追加など
    }

    /// <summary>
    /// 詳細パネルを閉じる
    /// </summary>
    public void CloseDetailPanel()
    {
        if (detailPanel != null)
            detailPanel.SetActive(false);

        selectedItem = null;
    }
}