using UnityEngine;
using UnityEngine.UI;

public class ScrollViewImageGenerator : MonoBehaviour
{
    [SerializeField] private ScrollRect _scrollRect;
    [SerializeField] private Sprite[] imageArray;       // 表示する画像の配列
    [SerializeField] private Vector2 imageSize = new Vector2(150, 150);
    [SerializeField] private float spacing = 20f;       // 間隔

    private void Start()
    {
        SetupContent();
        GenerateImage();
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

        sizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
        sizeFitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
    }

    private void GenerateImage() 
    {
        Transform content = _scrollRect.content;

        for (int i = 0; i < imageArray.Length; i++)
        {
            // Image用オブジェクトを生成
            GameObject imageObj = new GameObject("Image_" + i);
            imageObj.transform.SetParent(content, false);

            // Image コンポーネント設定
            Image img = imageObj.AddComponent<Image>();
            img.sprite = imageArray[i];
            img.preserveAspect = true;

            // RectTransform 設定
            RectTransform rectTransform = imageObj.GetComponent<RectTransform>();
            rectTransform.sizeDelta = imageSize;

            // LayoutElement 設定
            LayoutElement layoutElement = imageObj.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = imageSize.y;
            layoutElement.preferredWidth = imageSize.x;
        }

        // 一番上から表示開始
        _scrollRect.verticalNormalizedPosition = 1f;
    }
}
