using UnityEngine;
using UnityEngine.UI;

public class ItemDataScrollViewGenerator : MonoBehaviour
{
    [SerializeField] private ScrollRect _scrollRect;
    [SerializeField] private ShopInventory shopInventory;  // ShopInventory���g�p
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

        // �c���у��C�A�E�g
        VerticalLayoutGroup layoutGroup = content.GetComponent<VerticalLayoutGroup>();
        if (layoutGroup == null)
        {
            layoutGroup = content.gameObject.AddComponent<VerticalLayoutGroup>();
        }

        //�C���[�W�̊Ԋu
        layoutGroup.spacing = spacing;
        //�㒆���Ɋ񂹂�
        layoutGroup.childAlignment = TextAnchor.UpperCenter;
        //�摜�������ɍ��킹�ĐL�т邩�ǂ���
        layoutGroup.childControlWidth = true;
        //�����������I�ɐݒ肷�邩�ǂ���
        layoutGroup.childControlHeight = false;
        //���������I�ɍL���邩
        layoutGroup.childForceExpandWidth = false;
        //�c�ɍL���邩
        layoutGroup.childForceExpandHeight = false;
        //���E�㉺�ɗ]�����ǂ̂��炢��邩(left, right, top, bottom)
        layoutGroup.padding = new RectOffset(10, 10, 10, 10);

        // ContentSizeFitter �ō�������������
        ContentSizeFitter sizeFitter = content.GetComponent<ContentSizeFitter>();
        if (sizeFitter == null)
        {
            sizeFitter = content.gameObject.AddComponent<ContentSizeFitter>();
        }
        //�c�̎�������
        sizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
        //�������Ȃ�
        sizeFitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
    }

    private void GenerateItems()
    {

        if (shopInventory == null)
        {
            Debug.LogError("shopInventory �� Inspector �ɐݒ肳��Ă��܂���I");
            return;
        }

        if (shopInventory.items == null)
        {
            Debug.LogError("shopInventory.items ������������Ă��܂���I");
            return;
        }

        Debug.Log($"�A�C�e����: {shopInventory.items.Count}");

        Transform content = _scrollRect.content;

        for (int i = 0; i < shopInventory.items.Count; i++)
        {
            ItemData item = shopInventory.items[i];

            if (item == null)
            {
                Debug.LogWarning($"Item {i} �� null �ł�");
                continue;
            }

            // �A�C�e���\���p�̃I�u�W�F�N�g�𐶐�
            GameObject itemObj = CreateItemUI(item, i);
            itemObj.transform.SetParent(content, false);

            // RectTransform �ݒ�
            RectTransform rectTransform = itemObj.GetComponent<RectTransform>();
            rectTransform.sizeDelta = itemSize;

            // LayoutElement �ݒ�
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

        // ���C�A�E�g�������I�ɍX�V
        Canvas.ForceUpdateCanvases();

        // ��ԏォ��\���J�n
        if (_scrollRect != null)
        {
            _scrollRect.verticalNormalizedPosition = 1f;
            Debug.Log("�X�N���[���ʒu��ݒ肵�܂���");
        }

        Debug.Log("GenerateItems ����");
    }

    private GameObject CreateItemUI(ItemData item, int index)
    {
        Debug.Log($"CreateItemUI �J�n: Item {index}");

        // �x�[�X�ƂȂ�p�l��
        GameObject itemObj = new GameObject("Item_" + index, typeof(RectTransform));
        RectTransform itemRect = itemObj.GetComponent<RectTransform>();

        Image bgImage = itemObj.AddComponent<Image>();
        bgImage.color = new Color(0.95f, 0.95f, 0.95f, 1f);

        Debug.Log($"�A�C�R���擾��...");
        Sprite icon = item.GetIcon();

        // �A�C�R���摜
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
            Debug.LogWarning("�A�C�R����null�ł�");
            iconImage.color = Color.gray; // �A�C�R�����Ȃ��ꍇ�̓O���[�\��
        }

        RectTransform iconRect = iconObj.GetComponent<RectTransform>();
        iconRect.anchorMin = new Vector2(0, 0.5f);
        iconRect.anchorMax = new Vector2(0, 0.5f);
        iconRect.pivot = new Vector2(0, 0.5f);
        iconRect.anchoredPosition = new Vector2(10, 0);
        iconRect.sizeDelta = new Vector2(80, 80);

        Debug.Log($"�e�L�X�g���擾��...");

        string itemName = "";
        string description = "";
        int price = 0;
        int itemLv = 0;

        try
        {
            itemName = item.GetItemName();
            Debug.Log($"itemName�擾����: {itemName}");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"GetItemName()�ŃG���[: {e.Message}");
            itemName = "�G���[";
        }

        try
        {
            description = item.GetDescription();
            Debug.Log($"description�擾����: {description}");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"GetDescription()�ŃG���[: {e.Message}");
            description = "�G���[";
        }

        try
        {
            price = item.GetPrice();
            Debug.Log($"price�擾����: {price}");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"GetPrice()�ŃG���[: {e.Message}");
        }

        try
        {
            itemLv = item.GetItemLv();
            Debug.Log($"itemLv�擾����: {itemLv}");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"GetItemLv()�ŃG���[: {e.Message}");
        }

        // �e�L�X�g���G���A
        GameObject textArea = new GameObject("TextArea", typeof(RectTransform));
        textArea.transform.SetParent(itemObj.transform, false);
        RectTransform textRect = textArea.GetComponent<RectTransform>();
        textRect.anchorMin = new Vector2(0, 0);
        textRect.anchorMax = new Vector2(1, 1);
        textRect.offsetMin = new Vector2(100, 10);
        textRect.offsetMax = new Vector2(-10, -10);

        // �A�C�e�����i�㕔�j
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

        // �����i�����j
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

        // ���i�ƃ��x���i�����j
        GameObject priceObj = new GameObject("PriceLevel", typeof(RectTransform));
        priceObj.transform.SetParent(textArea.transform, false);
        Text priceText = priceObj.AddComponent<Text>();
        string priceLevel = $"���i: {price}G  Lv: {itemLv}";
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

        Debug.Log($"CreateItemUI ����: Item {index}");
        return itemObj;
    }
}