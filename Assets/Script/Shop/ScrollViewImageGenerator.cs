using UnityEngine;
using UnityEngine.UI;

public class ItemDataScrollViewGenerator : MonoBehaviour
{
    [SerializeField] private ScrollRect _scrollRect;
    [SerializeField] private ShopInventory shopInventory;
    [SerializeField] private Vector2 itemSize = new Vector2(300, 120);
    [SerializeField] private float spacing = 10f;

    private void Start()
    {
        if (_scrollRect == null)
        {
            Debug.LogError("ScrollRect が設定されていません!");
            return;
        }

        if (shopInventory == null || shopInventory.items == null)
        {
            Debug.LogError("ShopInventory が設定されていません!");
            return;
        }

        SetupContent();
        GenerateItems();
    }

    private void SetupContent()
    {
        Transform content = _scrollRect.content;

        // 既存のLayoutGroupとSizeFitterをクリア
        VerticalLayoutGroup existingLayout = content.GetComponent<VerticalLayoutGroup>();
        if (existingLayout != null) Destroy(existingLayout);

        ContentSizeFitter existingFitter = content.GetComponent<ContentSizeFitter>();
        if (existingFitter != null) Destroy(existingFitter);

        // 縦並びレイアウト
        VerticalLayoutGroup layoutGroup = content.gameObject.AddComponent<VerticalLayoutGroup>();
        layoutGroup.spacing = spacing;
        layoutGroup.childAlignment = TextAnchor.UpperCenter;
        layoutGroup.childControlWidth = false;
        layoutGroup.childControlHeight = false;
        layoutGroup.childForceExpandWidth = false;
        layoutGroup.childForceExpandHeight = false;
        layoutGroup.padding = new RectOffset(10, 10, 10, 10);

        // ContentSizeFitter で高さを自動調整
        ContentSizeFitter sizeFitter = content.gameObject.AddComponent<ContentSizeFitter>();
        sizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
        sizeFitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
    }

    private void GenerateItems()
    {
        Transform content = _scrollRect.content;

        // 既存の子要素を削除
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < shopInventory.items.Count; i++)
        {
            ItemData item = shopInventory.items[i];
            if (item == null)
            {
                Debug.LogWarning($"Item {i} が null です");
                continue;
            }

            // アイテム表示用のオブジェクトを生成
            GameObject itemObj = CreateItemUI(item, i);
            itemObj.transform.SetParent(content, false);

            // LayoutElement 設定
            LayoutElement layoutElement = itemObj.AddComponent<LayoutElement>();
            layoutElement.minWidth = itemSize.x;
            layoutElement.minHeight = itemSize.y;
            layoutElement.preferredWidth = itemSize.x;
            layoutElement.preferredHeight = itemSize.y;
        }

        // レイアウトを強制的に更新
        LayoutRebuilder.ForceRebuildLayoutImmediate(content as RectTransform);
        Canvas.ForceUpdateCanvases();

        // 一番上から表示開始
        _scrollRect.verticalNormalizedPosition = 1f;
    }

    private GameObject CreateItemUI(ItemData item, int index)
    {
        // データ取得
        string itemName = item.GetItemName();
        string description = item.GetDescription();
        int price = item.GetPrice();
        int itemLv = item.GetItemLv();
        Sprite icon = item.GetIcon();

        // ベースとなるパネル
        GameObject itemObj = new GameObject("Item_" + index);
        RectTransform itemRect = itemObj.AddComponent<RectTransform>();
        itemRect.sizeDelta = itemSize;

        // 背景
        Image bgImage = itemObj.AddComponent<Image>();
        bgImage.color = new Color(0.9f, 0.9f, 0.9f, 1f);

        // アイコン画像
        GameObject iconObj = new GameObject("Icon");
        RectTransform iconRect = iconObj.AddComponent<RectTransform>();
        iconObj.transform.SetParent(itemObj.transform, false);

        iconRect.anchorMin = new Vector2(0, 0.5f);
        iconRect.anchorMax = new Vector2(0, 0.5f);
        iconRect.pivot = new Vector2(0, 0.5f);
        iconRect.anchoredPosition = new Vector2(10, 0);
        iconRect.sizeDelta = new Vector2(80, 80);

        Image iconImage = iconObj.AddComponent<Image>();
        if (icon != null)
        {
            iconImage.sprite = icon;
            iconImage.preserveAspect = true;
        }
        else
        {
            iconImage.color = Color.gray;
        }

        // フォント取得
        Font font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        if (font == null)
        {
            font = Font.CreateDynamicFontFromOSFont("Arial", 14);
        }

        // アイテム名
        GameObject nameObj = new GameObject("Name");
        RectTransform nameRect = nameObj.AddComponent<RectTransform>();
        nameObj.transform.SetParent(itemObj.transform, false);

        nameRect.anchorMin = new Vector2(0, 1);
        nameRect.anchorMax = new Vector2(1, 1);
        nameRect.pivot = new Vector2(0, 1);
        nameRect.anchoredPosition = new Vector2(100, -10);
        nameRect.sizeDelta = new Vector2(-110, 30);

        Text nameText = nameObj.AddComponent<Text>();
        nameText.text = itemName;
        nameText.font = font;
        nameText.fontSize = 16;
        nameText.fontStyle = FontStyle.Bold;
        nameText.color = Color.black;
        nameText.alignment = TextAnchor.MiddleLeft;

        // 説明
        GameObject descObj = new GameObject("Description");
        RectTransform descRect = descObj.AddComponent<RectTransform>();
        descObj.transform.SetParent(itemObj.transform, false);

        descRect.anchorMin = new Vector2(0, 0.5f);
        descRect.anchorMax = new Vector2(1, 0.5f);
        descRect.pivot = new Vector2(0, 0.5f);
        descRect.anchoredPosition = new Vector2(100, 0);
        descRect.sizeDelta = new Vector2(-110, 40);

        Text descText = descObj.AddComponent<Text>();
        descText.text = description;
        descText.font = font;
        descText.fontSize = 11;
        descText.color = new Color(0.3f, 0.3f, 0.3f);
        descText.alignment = TextAnchor.UpperLeft;

        // 価格とレベル
        GameObject priceObj = new GameObject("PriceLevel");
        RectTransform priceRect = priceObj.AddComponent<RectTransform>();
        priceObj.transform.SetParent(itemObj.transform, false);

        priceRect.anchorMin = new Vector2(0, 0);
        priceRect.anchorMax = new Vector2(1, 0);
        priceRect.pivot = new Vector2(0, 0);
        priceRect.anchoredPosition = new Vector2(100, 5);  // 10 → 5 に変更（下の余白を減らす）
        priceRect.sizeDelta = new Vector2(-110, 30);  // 25 → 30 に変更（高さを増やす）

        Text priceText = priceObj.AddComponent<Text>();
        priceText.text = $"価格: {price}G  Lv: {itemLv}";
        priceText.font = font;
        priceText.fontSize = 13;
        priceText.color = new Color(0.2f, 0.5f, 0.8f);
        priceText.alignment = TextAnchor.MiddleLeft;  // または TextAnchor.LowerLeft でもOK

        return itemObj;
    }
}