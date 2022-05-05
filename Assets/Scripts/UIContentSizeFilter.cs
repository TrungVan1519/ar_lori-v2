using UnityEngine;
using UnityEngine.UI;

public class UIContentSizeFilter : MonoBehaviour
{
    private HorizontalLayoutGroup horizontalLayoutGroup;
    private RectTransform rect;

    private void Start()
    {
        horizontalLayoutGroup = GetComponent<HorizontalLayoutGroup>();
        rect = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (transform.childCount > 0)
        { 
            int spaceCount = transform.childCount - 1;
            float childWidth = transform.GetChild(0).GetComponent<RectTransform>().rect.width;
            float width = childWidth * spaceCount
                + horizontalLayoutGroup.padding.left
                + horizontalLayoutGroup.padding.right
                + horizontalLayoutGroup.spacing * spaceCount;

            rect.sizeDelta = new Vector2(width, rect.rect.height);
        }
    }
}
