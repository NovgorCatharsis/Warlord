using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TileController : MonoBehaviour
{
    [SerializeField] private GameMasterSO gameMasterSO;
    [SerializeField] public GameObject[] connectedTiles;

    private EntityController entityController;
    private EnemyController enemyController;
    private PlayerInputController playerInputController;

    public List<GameObject> unitsOnTile;
    public List<GameObject> enemiesOnTile;

    public bool fightHappened;
    private int unitsStrengthSum;
    private int enemiesStrengthSum;
    private int strengthDif;
    

    private void Awake()
    {
        playerInputController = gameObject.GetComponent<PlayerInputController>();
        gameMasterSO.FirstPhaseInitiated += Fight;
    }
    //private void Start()
    //{
    //    gameMasterSO.FirstPhaseInitiated += Fight;
    //}


    public void Fight()
    {
        Debug.Log("UNITS ON TILE " + gameObject.name + ": " + unitsOnTile.Count); 
        Debug.Log("ENEMIES ON TILE " + gameObject.name + ": " + enemiesOnTile.Count);

        fightHappened = false;
        if (unitsOnTile.Count == 0 || enemiesOnTile.Count == 0) 
        {
            gameMasterSO.FightsCheck();
            return;
        }
        fightHappened = true;

        unitsStrengthSum = unitsOnTile.Sum(u => u.GetComponent<UnitController>().strength);
        enemiesStrengthSum = enemiesOnTile.Sum(e => e.GetComponent<EnemyController>().strength);
        strengthDif = Mathf.Abs(unitsStrengthSum - enemiesStrengthSum);

        Debug.Log("TILE " + gameObject.name + " HAS A FIGHT");
        Debug.Log("UNITS STRENGTH  " + unitsStrengthSum);
        Debug.Log("ENEMIES STRENGTH  " + enemiesStrengthSum);
        Debug.Log("STRENGTH DIF  " + strengthDif);

        if (unitsStrengthSum > enemiesStrengthSum) { FightResult(unitsOnTile, enemiesOnTile); Debug.Log("UNITS WON"); } // (winners, losers) If units win
        else if (unitsStrengthSum < enemiesStrengthSum) { FightResult(enemiesOnTile, unitsOnTile); Debug.Log("ENEMIES WON"); } // (winners, losers) If enemies win
        else { FightResult(enemiesOnTile, unitsOnTile, true); Debug.Log("DRAW"); } // (isDraw) If draw

        gameMasterSO.FightsCheck();
        unitsStrengthSum = 0; // Annul values
        enemiesStrengthSum = 0;
        strengthDif = 0;
    }

    public void FightResult (List<GameObject> winners, List<GameObject> losers, bool isDraw = false)
    {
        List<GameObject> entities = (List<GameObject>)winners.Concat(losers).ToList();

        foreach (GameObject entity in entities)
        {
            entityController = entity.GetComponent<EntityController>();

            // If draw
            if (isDraw) 
            {
                entityController.Death();
                continue;
            }

            // For losers
            if (losers.Contains(entity))
            {
                entityController.Death();
                continue;
            }

            // For winners
            if (strengthDif <= 0) continue; // If all damage was already taken
            if (entityController.strength >= strengthDif)
            {
                entityController.strength -= strengthDif;
                strengthDif = 0;
            }
            else
            {
                strengthDif -= entityController.strength;
                entityController.strength = 0;
            }
            if (entityController.strength <= 0) entityController.Death(); // In case of lethal damage
        }
    }
}
