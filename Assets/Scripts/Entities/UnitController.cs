using System.Linq;
using UnityEngine;

public class UnitController : EntityController
{
    public override void UpdateTile()
    {
        tileController.unitsOnTile.Add(gameObject);
    }
    private void OnDisable()
    {
        if (tileController) tileController.unitsOnTile.Remove(gameObject);
    }

    public void Move(GameObject newTile)
    {
        if (tileController.connectedTiles.Contains(newTile))
        {
            switch (newTile.GetComponent<TileController>().unitsOnTile.Count()) // How many units on new tile
            {  
                case 0:
                    deltaZ = 0.5f;
                    break;
                case 1:
                    deltaZ = 0.0f;
                    break;
                case 2:
                    deltaZ = -0.5f;
                    break;
                default:
                    Debug.Log("Tile is at maximum capacity, cannot move there.");
                    return;
            }

            gameObject.transform.position = new Vector3(newTile.transform.position.x-0.5f, newTile.transform.position.y+0.62f, newTile.transform.position.z+deltaZ);
            tileController.unitsOnTile.Remove(gameObject); // Remove unit from current tile

            currentTile = newTile;
            tileController = currentTile.GetComponent<TileController>();
            tileController.unitsOnTile.Add(gameObject); // Add unit to new tile
        }
    }
}
