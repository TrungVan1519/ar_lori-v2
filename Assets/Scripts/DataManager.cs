using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        objectToPlaces.Clear();

        switch (SceneManager.GetActiveScene().name)
        {
            case "FurnitureScene":
                foreach (var objectToPlace in Resources.LoadAll("ObjectToPlaces", typeof(ObjectToPlace)))
                {
                    if (objectToPlace.name.ToLower().Contains("Bed".ToLower())
                        || objectToPlace.name.ToLower().Contains("Table".ToLower())
                        || objectToPlace.name.ToLower().Contains("Chair".ToLower())
                        || objectToPlace.name.ToLower().Contains("Bookshelf".ToLower()))
                    {
                        objectToPlaces.Add(objectToPlace as ObjectToPlace);
                    }
                }
                break;
            case "DecoratorScene":
                foreach (var objectToPlace in Resources.LoadAll("ObjectToPlaces", typeof(ObjectToPlace)))
                {
                    if (!objectToPlace.name.ToLower().Contains("Bed".ToLower())
                        && !objectToPlace.name.ToLower().Contains("Table".ToLower())
                        && !objectToPlace.name.ToLower().Contains("Chair".ToLower())
                        && !objectToPlace.name.ToLower().Contains("Bookshelf".ToLower()))
                    {
                        objectToPlaces.Add(objectToPlace as ObjectToPlace);
                    }
                }
                break;
            default:
                foreach (var objectToPlace in Resources.LoadAll("ObjectToPlaces", typeof(ObjectToPlace)))
                {
                    objectToPlaces.Add(objectToPlace as ObjectToPlace);
                }
                break;
        }
    }

    private void CreateButtons()
    {
        foreach (var objectToPlace in objectToPlaces)
        {
            SelectObjectToPlace button = Instantiate(selectObjectToPlace, buttonContainer.transform);
            button.ObjectToPlaceId = objectToPlaceId;
            button.ButtonTexture = objectToPlace.texture;
            objectToPlaceId++;
        }
    }
}
