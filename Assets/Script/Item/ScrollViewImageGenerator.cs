using UnityEngine;
using UnityEngine.UI;

public class ItemDataScrollViewGenerator : MonoBehaviour
{
    [SerializeField] private ScrollRect _scrollRect;
    [SerializeField] private ShopInventory shopInventory;  // ShopInventoryを使用
    [SerializeField] private Vector2 itemSize = new Vector2(300, 120);
    [SerializeField] private float spacing = 10f;

    private void Start()
    {
        SetupContent();
        GenerateItems();
    }

    private void SetupContent()
    {
        Transform content = _scrollRect.content;

        // 縦並びレイアウト
        VerticalLayoutGroup layoutGroup = content.GetComponent<VerticalLayoutGroup>();
        if (layoutGroup == null)
        {
            layoutGroup = content.gameObject.AddComponent<VerticalLayoutGroup>();
        }

        //イメージの間隔
        layoutGroup.spacing = spacing;
        //上中央に寄せる
        layoutGroup.childAlignment = TextAnchor.UpperCenter;
        //画像が横幅に合わせて伸びるかどうか
        layoutGroup.childControlWidth = true;
        //高さを自動的に設定するかどうか
        layoutGroup.childControlHeight = false;
        //横を強制的に広げるか
        layoutGroup.childForceExpandWidth = false;
        //縦に広げるか
        layoutGroup.childForceExpandHeight = false;
        //左右上下に余白をどのくらい作るか(left, right, top, bottom)
        layoutGroup.padding = new RectOffset(10, 10, 10, 10);

        // ContentSizeFitter で高さを自動調整
        ContentSizeFitter sizeFitter = content.GetComponent<ContentSizeFitter>();
        if (sizeFitter == null)
        {
            sizeFitter = content.gameObject.AddComponent<ContentSizeFitter>();
        }
        //縦の自動調整
        sizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
        //動かさない
        sizeFitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
    }

    private void GenerateItems()
    {

        if (shopInventory == null)
        {
            Debug.LogError("shopInventory が Inspector に設定されていません！");
            return;
        }

        if (shopInventory.items == null)
        {
            Debug.LogError("shopInventory.items が初期化されていません！");
            return;
        }

        Debug.Log($"アイテム数: {shopInventory.items.Count}");

        Transform content = _scrollRect.content;

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

            // RectTransform 設定
            RectTransform rectTransform = itemObj.GetComponent<RectTransform>();
            rectTransform.sizeDelta = itemSize;

            // LayoutElement 設定
            LayoutElement layoutElement = itemObj.GetComponent<LayoutElement>();
            if (layoutElement == null)
            {
                layoutElement = itemObj.AddComponent<LayoutElement>();
            }
            Debug.Log(layoutElement.preferredHeight);
            Debug.Log(layoutElement.preferredWidth);
            layoutElement.preferredHeight = itemSize.y;
            layoutElement.preferredWidth = itemSize.x;
        }

        // レイアウトを強制的に更新
        Canvas.ForceUpdateCanvases();

        // 一番上から表示開始
        if (_scrollRect != null)
        {
            _scrollRect.verticalNormalizedPosition = 1f;
            Debug.Log("スクロール位置を設定しました");
        }

        Debug.Log("GenerateItems 完了");
    }

    private GameObject CreateItemUI(ItemData item, int index)
    {
        Debug.Log($"CreateItemUI 開始: Item {index}");

        // ベースとなるパネル
        GameObject itemObj = new GameObject("Item_" + index, typeof(RectTransform));
        RectTransform itemRect = itemObj.GetComponent<RectTransform>();

        Image bgImage = itemObj.AddComponent<Image>();
        bgImage.color = new Color(0.95f, 0.95f, 0.95f, 1f);

        Debug.Log($"アイコン取得中...");
        Sprite icon = item.GetIcon();

        // アイコン画像
        GameObject iconObj = new GameObject("Icon", typeof(RectTransform));
        iconObj.transform.SetParent(itemObj.transform, false);
        Image iconImage = iconObj.AddComponent<Image>();

        if (icon != null)
        {
            iconImage.sprite = icon;
            iconImage.preserveAspect = true;
        }
        else
        {
            Debug.LogWarning("アイコンがnullです");
            iconImage.color = Color.gray; // アイコンがない場合はグレー表示
        }

        RectTransform iconRect = iconObj.GetComponent<RectTransform>();
        iconRect.anchorMin = new Vector2(0, 0.5f);
        iconRect.anchorMax = new Vector2(0, 0.5f);
        iconRect.pivot = new Vector2(0, 0.5f);
        iconRect.anchoredPosition = new Vector2(10, 0);
        iconRect.sizeDelta = new Vector2(80, 80);

        Debug.Log($"テキスト情報取得中...");

        string itemName = "";
        string description = "";
        int price = 0;
        int itemLv = 0;

        try
        {
            itemName = item.GetItemName();
            Debug.Log($"itemName取得成功: {itemName}");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"GetItemName()でエラー: {e.Message}");
            itemName = "エラー";
        }

        try
        {
            description = item.GetDescription();
            Debug.Log($"description取得成功: {description}");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"GetDescription()でエラー: {e.Message}");
            description = "エラー";
        }

        try
        {
            price = item.GetPrice();
            Debug.Log($"price取得成功: {price}");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"GetPrice()でエラー: {e.Message}");
        }

        try
        {
            itemLv = item.GetItemLv();
            Debug.Log($"itemLv取得成功: {itemLv}");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"GetItemLv()でエラー: {e.Message}");
        }

        // テキスト情報エリア
        GameObject textArea = new GameObject("TextArea", typeof(RectTransform));
        textArea.transform.SetParent(itemObj.transform, false);
        RectTransform textRect = textArea.GetComponent<RectTransform>();
        textRect.anchorMin = new Vector2(0, 0);
        textRect.anchorMax = new Vector2(1, 1);
        textRect.offsetMin = new Vector2(100, 10);
        textRect.offsetMax = new Vector2(-10, -10);

        // アイテム名（上部）
        GameObject nameObj = new GameObject("Name", typeof(RectTransform));
        nameObj.transform.SetParent(textArea.transform, false);
        Text nameText = nameObj.AddComponent<Text>();
        nameText.text = itemName;

        Font font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        if (font == null)
        {
            font = Font.CreateDynamicFontFromOSFont("Arial", 14);
        }
        nameText.font = font;
        nameText.fontSize = 16;
        nameText.fontStyle = FontStyle.Bold;
        nameText.color = Color.black;
        nameText.alignment = TextAnchor.UpperLeft;

        RectTransform nameRect = nameObj.GetComponent<RectTransform>();
        nameRect.anchorMin = new Vector2(0, 1);
        nameRect.anchorMax = new Vector2(1, 1);
        nameRect.pivot = new Vector2(0, 1);
        nameRect.anchoredPosition = new Vector2(0, 0);
        nameRect.sizeDelta = new Vector2(0, 25);

        // 説明（中央）
        GameObject descObj = new GameObject("Description", typeof(RectTransform));
        descObj.transform.SetParent(textArea.transform, false);
        Text descText = descObj.AddComponent<Text>();
        descText.text = description;
        descText.font = font;
        descText.fontSize = 12;
        descText.color = new Color(0.3f, 0.3f, 0.3f);
        descText.alignment = TextAnchor.UpperLeft;

        RectTransform descRect = descObj.GetComponent<RectTransform>();
        descRect.anchorMin = new Vector2(0, 0);
        descRect.anchorMax = new Vector2(1, 1);
        descRect.offsetMin = new Vector2(0, 25);
        descRect.offsetMax = new Vector2(0, -25);

        // 価格とレベル（下部）
        GameObject priceObj = new GameObject("PriceLevel", typeof(RectTransform));
        priceObj.transform.SetParent(textArea.transform, false);
        Text priceText = priceObj.AddComponent<Text>();
        string priceLevel = $"価格: {price}G  Lv: {itemLv}";
        priceText.text = priceLevel;
        priceText.font = font;
        priceText.fontSize = 14;
        priceText.color = new Color(0.2f, 0.5f, 0.8f);
        priceText.alignment = TextAnchor.LowerLeft;

        RectTransform priceRect = priceObj.GetComponent<RectTransform>();
        priceRect.anchorMin = new Vector2(0, 0);
        priceRect.anchorMax = new Vector2(1, 0);
        priceRect.pivot = new Vector2(0, 0);
        priceRect.anchoredPosition = new Vector2(0, 0);
        priceRect.sizeDelta = new Vector2(0, 25);

        Debug.Log($"CreateItemUI 完了: Item {index}");
        return itemObj;
    }
}