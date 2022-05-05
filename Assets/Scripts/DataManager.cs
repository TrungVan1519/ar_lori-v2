using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    [SerializeField] private SelectObjectToPlace selectObjectToPlace;
    [SerializeField] private GameObject buttonContainer;
    [SerializeField] private List<ObjectToPlace> objectToPlaces;

    private GameObject objectToPlace;
    private int objectToPlaceId;

    private static DataManager instance;
    public static DataManager Instance
    {
        get
        {
            if (instance == null) instance = FindObjectOfType<DataManager>();
            return instance;
        }
    }

    public void SetObjectToPlace(int id)
    {
        objectToPlace = objectToPlaces[id].prefab;
    }

    public GameObject GetObjectToPlace()
    {
        return objectToPlace;
    }

    private void Start()
    {
        LoadObjectToPlaces();
        CreateButtons();
    }

    private void LoadObjectToPlaces()
    {
        foreach (var objectToPlace in Resources.LoadAll("ObjectToPlaces", typeof(ObjectToPlace)))
        {
            objectToPlaces.Add(objectToPlace as ObjectToPlace);
        }
    }

    private void CreateButtons()
    {
        foreach(var objectToPlace in objectToPlaces)
        {
            SelectObjectToPlace button = Instantiate(selectObjectToPlace, buttonContainer.transform);
            button.ObjectToPlaceId = objectToPlaceId;
            button.ButtonTexture = objectToPlace.texture;
            objectToPlaceId++;
        }
    }
}
