using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonCreator : MonoBehaviour
{
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private Button buttonPrefab;
    [SerializeField] private ShopInventory inventory;
    void Start()
    {


        foreach (var item in inventory.items) 
        {
            // �{�^���𐶐�
            Button newButton = Instantiate(buttonPrefab);

            //Canvas �̎q�ɂ���
            newButton.transform.SetParent(scrollRect.content, false);

            Transform nameTextTransform = newButton.transform.Find("NameText");
            Transform moneyTextTransform = newButton.transform.Find("MoneyText");

            TextMeshProUGUI nameText = nameTextTransform.GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI moneyText = moneyTextTransform.GetComponent<TextMeshProUGUI>();

            nameText.text = item.GetItemName();
            moneyText.text = item.GetPrice().ToString() + "G";

            var capturedItem = item;
            newButton.onClick.AddListener(() => capturedItem.ExecuteAction());
        }
    }
}