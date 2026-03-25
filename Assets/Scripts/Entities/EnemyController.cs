using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : EntityController
{
    private List<GameObject> tilesAhead = new List<GameObject>();
    private bool foundTile;

    public override void UpdateTile()
    {
        tileController.enemiesOnTile.Add(gameObject);
    }
    private void OnEnable()
    {
        gameMasterSO.SecondPhaseInitiated += Move;
    }

    private void OnDisable()
    {
        tileController.enemiesOnTile.Remove(gameObject);
        gameMasterSO.SecondPhaseInitiated -= Move;
    }

    public void Move()
    {
        if (tileController.fightHappened) { Debug.Log("ENEMY " + gameObject.name + " FOUGHT. NO MOVEMENT.");  return; } // Enemies, who participated in fights, do not move

        // Find tile with the lowest strength sum ahead and move there
        foreach (GameObject tile in tileController.connectedTiles)
        {
            if (tile.transform.position.x < currentTile.transform.position.x)
            {
                tilesAhead.Add(tile); // Array of tiles ahead of the enemy
            }
        }
        if (tilesAhead.Count == 0) return;

        tilesAhead = tilesAhead.OrderBy(t => t.GetComponent<TileController>().unitsOnTile.Sum(u => u.GetComponent<UnitController>().strength)).ToList(); // Sort tiles by enemy strength
        foreach (GameObject tile in tilesAhead)
        {
            if (tile.GetComponent<TileController>().enemiesOnTile.Count() > 2) //if too many enemies on tile already
            {
                continue;
            }
            else // End of loop if suitable tile is found
            {
                foundTile = true;
                break;
            }
        }

        if (!foundTile) return;
        tileController.enemiesOnTile.Remove(gameObject);

        currentTile = tilesAhead[0];
        tilesAhead.Clear();
        tileController = currentTile.GetComponent<TileController>();
        
        switch (tileController.enemiesOnTile.Count()) // How many units on new tile
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
        }
        gameObject.transform.position = new Vector3(currentTile.transform.position.x + 0.5f, currentTile.transform.position.y + 0.62f, currentTile.transform.position.z);
        tileController.enemiesOnTile.Add(gameObject);
    }
}
