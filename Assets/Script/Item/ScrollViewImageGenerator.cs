using UnityEngine;
using UnityEngine.UI;

public class ScrollViewImageGenerator : MonoBehaviour
{
    [SerializeField] private ScrollRect _scrollRect;
    [SerializeField] private Sprite[] imageArray;       // �\������摜�̔z��
    [SerializeField] private Vector2 imageSize = new Vector2(150, 150);
    [SerializeField] private float spacing = 20f;       // �Ԋu

    private void Start()
    {
        SetupContent();
        GenerateImage();
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

        sizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
        sizeFitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
    }

    private void GenerateImage() 
    {
        Transform content = _scrollRect.content;

        for (int i = 0; i < imageArray.Length; i++)
        {
            // Image�p�I�u�W�F�N�g�𐶐�
            GameObject imageObj = new GameObject("Image_" + i);
            imageObj.transform.SetParent(content, false);

            // Image �R���|�[�l���g�ݒ�
            Image img = imageObj.AddComponent<Image>();
            img.sprite = imageArray[i];
            img.preserveAspect = true;

            // RectTransform �ݒ�
            RectTransform rectTransform = imageObj.GetComponent<RectTransform>();
            rectTransform.sizeDelta = imageSize;

            // LayoutElement �ݒ�
            LayoutElement layoutElement = imageObj.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = imageSize.y;
            layoutElement.preferredWidth = imageSize.x;
        }

        // ��ԏォ��\���J�n
        _scrollRect.verticalNormalizedPosition = 1f;
    }
}
