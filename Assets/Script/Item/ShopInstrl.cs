using UnityEngine;
using UnityEngine.UI;

public class ButtonCreator : MonoBehaviour
{
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private Button buttonPrefab;

    void Start()
    {
        // ƒ{ƒ^ƒ“‚ğ¶¬
        Button newButton = Instantiate(buttonPrefab);
        //Canvas ‚Ìq‚É‚·‚é
        newButton.transform.SetParent(scrollRect.content, false);
    }
}