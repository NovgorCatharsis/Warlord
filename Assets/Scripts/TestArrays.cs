using UnityEngine;

public class TestArrays : MonoBehaviour
{
    public GameObject[] unitsOnTile;
    void Start()
    {
        unitsOnTile = new GameObject[3];
        Debug.Log("ARRAY "+unitsOnTile);
        Debug.Log("ARRAY LENGTH "+unitsOnTile.Length);

        for (int i = 0; i < unitsOnTile.Length; i++)
        {
            if (Equals(unitsOnTile[i], null)) unitsOnTile[i] = new GameObject("Unit " + i);
            Debug.Log("ARRAY ELEMENT "+unitsOnTile[i]);
        }
    }

}
