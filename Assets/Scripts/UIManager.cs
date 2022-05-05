using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Transform selectionPoint;

    private GraphicRaycaster graphicRaycaster;
    private PointerEventData pointerEventData;

    private static UIManager instance;
    public static UIManager Instance
    {
        get
        {
            if (instance == null) instance = FindObjectOfType<UIManager>();
            return instance;
        }
    }

    void Start()
    {
        graphicRaycaster = GetComponent<GraphicRaycaster>();
        pointerEventData = new PointerEventData(GetComponent<EventSystem>());
        pointerEventData.position = selectionPoint.position;
    }

    public bool OnEnter(GameObject button)
    {
        List<RaycastResult> raycastResults = new List<RaycastResult>();
        graphicRaycaster.Raycast(pointerEventData, raycastResults);

        foreach(var raycastResult in raycastResults)
        {
            if (raycastResult.gameObject == button) return true;
        }
        return false;
    }
}
