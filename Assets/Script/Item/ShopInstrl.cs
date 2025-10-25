using UnityEngine;
using UnityEngine.UI;

public class ButtonCreator : MonoBehaviour
{
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private Button buttonPrefab;

    void Start()
    {
        // �{�^���𐶐�
        Button newButton = Instantiate(buttonPrefab);
        //Canvas �̎q�ɂ���
        newButton.transform.SetParent(scrollRect.content, false);
    }
}