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
        coins = 0;
        recruits = 0;
        corpses = 0;
        morale = 0;
        selectedUnit = null;
        tilesNumber = GameObject.FindGameObjectsWithTag("Tile").Count();
        UpdateUI();  
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
            Debug.Log("=================== ENEMIES NOW SPAWNING ===================");
            Debug.Log("=================== ENTITIES NOW HEALING ===================");
            GatherIncome();
        }
    }
    public void GatherIncome()
    {
        Debug.Log("=================== INCOME NOW BEING GATHERED ===================");
        ThirdPhaseInitiated?.Invoke();
        UpdateUI();
    }

    public void UpdateUI()
    {
        ResourcesUpdated?.Invoke();
    }
}
