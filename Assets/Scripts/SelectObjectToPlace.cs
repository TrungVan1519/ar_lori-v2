using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SelectObjectToPlace : MonoBehaviour
{
    [SerializeField] private RawImage rawImage;

    private Sprite buttonTexture;
    public Sprite ButtonTexture
    {
        get { return buttonTexture; }
        set
        {
            buttonTexture = value;
            rawImage.texture = buttonTexture.texture;
        }
    }

    private int objectToPlaceId;
    public int ObjectToPlaceId
    {
        get { return objectToPlaceId; }
        set { objectToPlaceId = value; }
    }

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(SelectObject);
    }

    void Update()
    {
        transform.DOScale(UIManager.Instance.OnEnter(gameObject) ? Vector3.one * 2 : Vector3.one, 0.3f);
    }

    private void SelectObject()
    {
        DataManager.Instance.SetObjectToPlace(objectToPlaceId);
    }
}
