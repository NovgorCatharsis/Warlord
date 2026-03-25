using System;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "GameMasterSO", menuName = "Scriptable Objects/GameMasterSO")]
public class GameMasterSO : ScriptableObject
{
    public int coins = 0;
    public int recruits = 0;
    public int corpses = 0;
    public int morale = 0;

    public GameObject selectedUnit;
    public int tilesNumber = 0;
    public int calculatedFights = 0;

    public event Action ResourcesUpdated;
    public event Action FirstPhaseInitiated; // Fight
    public event Action SecondPhaseInitiated; // Enemy movement
    public event Action ThirdPhaseInitiated; // Income

    private void OnEnable() 
    {
        tilesNumber = GameObject.FindGameObjectsWithTag("Tile").Count();
        ResourcesUpdated?.Invoke();
        Debug.Log("TOTAL OF " + tilesNumber + " TILES");
    }


    public void TurnSkip()
    {
        Debug.Log("=================== TURN SKIPPED ===================");
        Debug.Log("=================== FIGHTS NOW CALCULATING ===================");
        FirstPhaseInitiated?.Invoke();
    }
    public void FightsCheck()
    {
        calculatedFights++;
        if (calculatedFights == tilesNumber)
        {
            Debug.Log("=================== ENEMIES NOW MOVING ===================");
            calculatedFights = 0;
            SecondPhaseInitiated?.Invoke();
            GatherIncome();
        }
    }
    public void GatherIncome()
    {
        Debug.Log("=================== INCOME NOW BEING GATHERED ===================");
        ThirdPhaseInitiated?.Invoke();
        ResourcesUpdated?.Invoke();
    }
}
