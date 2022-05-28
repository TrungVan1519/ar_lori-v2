using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class DoubleTapToPlaceObject : MonoBehaviour
{
    [SerializeField] private GameObject placementIndicator;

    private ARRaycastManager arRaycastManager;
    private List<ARRaycastHit> hits;
    private Pose placementPose;
    private GameObject oldPlacedObj;

    // TODO handle double tap
    private int tapCount;
    private double newTime;
    private double delayedTime = 0.1; // wait for second tap in delayedTime seconds

    void Start()
    {
        arRaycastManager = FindObjectOfType<ARRaycastManager>();
    }

    void Update()
    {
        UpdatePlacementPose();
        UpdatePlacementIndicator();
        PlaceObject();
    }

    private void PlaceObject()
    {
        //// Single tap to place objects
        // SingleTapToPlace();

        // Double tap to place objects
        DoubleTapToPlace();
    }

    private void SingleTapToPlace()
    {
        if (hits.Count > 0 && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            // TODO delete old objects to place a new one
            if (oldPlacedObj != null) Destroy(oldPlacedObj);
            oldPlacedObj = Instantiate(DataManager.Instance.GetObjectToPlace(), placementPose.position, placementPose.rotation);
        }
    }

    private void DoubleTapToPlace()
    {
        if (hits.Count > 0 && IsDoubleTap())
        {
            // TODO delete old objects to place a new one
            if (oldPlacedObj != null) Destroy(oldPlacedObj);
            oldPlacedObj = Instantiate(DataManager.Instance.GetObjectToPlace(), placementPose.position, placementPose.rotation);
        }
    }

    private bool IsDoubleTap()
    {
        if (Input.touchCount == 1)
        {
            // Get the second tap
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Ended) tapCount += 1;
            if (tapCount == 1) newTime = Time.time + delayedTime;
            else if (tapCount == 2 && Time.time <= newTime && !IsTouchOverUI(touch))
            {
                tapCount = 0;
                return true;
            }
        }

        if (Time.time > newTime) tapCount = 0;
        return false;
    }

    private bool IsTouchOverUI(Touch touch)
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = new Vector2(touch.position.x, touch.position.y);
        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, raycastResults);
        return raycastResults.Count > 0;
    }

    private void UpdatePlacementIndicator()
    {
        if (hits.Count > 0)
        {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }
        else
        {
            if (placementIndicator != null) placementIndicator.SetActive(false);
        }
    }

    private void UpdatePlacementPose()
    {
        hits = new List<ARRaycastHit>();
        arRaycastManager.Raycast(Camera.main.ViewportToScreenPoint(new Vector3(0.5f, 0.5f)), hits, TrackableType.Planes);

        if (hits.Count > 0)
        {
            placementPose = hits[0].pose;
            placementPose.rotation = Quaternion.LookRotation(new Vector3(Camera.current.transform.forward.x, 0, Camera.current.transform.forward.z).normalized);
        }
    }
}
