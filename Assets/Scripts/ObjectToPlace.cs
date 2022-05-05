using UnityEngine;

[CreateAssetMenu(fileName = "ObjectToPlace", menuName = "Object To Place")]
public class ObjectToPlace : ScriptableObject
{
    public double price;
    public GameObject prefab;
    public Sprite texture; // item's image
}
