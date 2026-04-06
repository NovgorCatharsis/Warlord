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
        gameMasterSO.SecondPhaseInitiated -= Heal;
        gameMasterSO.SecondPhaseInitiated -= MoveRestoration;
        if (tileController)
        {
            tileController.unitsOnTile.Remove(gameObject);
            tileController.unitsSlots[currentSlot] = 0;
        }
        gameMasterSO.corpses += 3; // Each unit adds 3 corpses to the pool when it dies
        gameMasterSO.morale -= 5;
        gameMasterSO.UpdateUI();
    }

    public void Move(GameObject newTile)
    {
        if (tileController.connectedTiles.Contains(newTile))
        {
            if (newTile.GetComponent<TileController>().unitsOnTile.Count() >= 3)
            {
                Debug.Log("Tile is at maximum capacity, cannot move there.");
                return;
            }

            tileController.unitsOnTile.Remove(gameObject); // Remove unit from current tile
            tileController.unitsSlots[currentSlot] = 0; // Free up the slot on the old tile

            currentTile = newTile;
            tileController = currentTile.GetComponent<TileController>();

            for (int i = 0; i < tileController.unitsSlots.Length; i++)
            {
                if (tileController.unitsSlots[i] == 0)
                {
                    tileController.unitsSlots[i] = 1; // Occupy the slot on the new tile

                    currentSlot = i;
                    tileController.unitsOnTile.Add(gameObject); // Add unit to new tile
                    deltaZ = 0.5f - (i * 0.5f);
                    break;
                }
            }

            gameObject.transform.position = new Vector3(newTile.transform.position.x-0.5f, newTile.transform.position.y+0.62f, newTile.transform.position.z+deltaZ);
            moved = true;
        }
    }
}
