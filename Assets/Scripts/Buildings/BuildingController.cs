using UnityEngine;

public class BuildingController : MonoBehaviour
{
    [SerializeField] private GameMasterSO gameMasterSO;

    [Header("Income")]
    [SerializeField] private int coinsIncome;
    [SerializeField] private int recruitsIncome;
    [SerializeField] private int moraleIncome;
    [SerializeField] private int corpsesIncome;

    [Header("Cost")]
    [SerializeField] private int coinsCost;
    [SerializeField] private int recruitsCost;
    [SerializeField] private int moraleCost;
    [SerializeField] private int corpsesCost;

    private void Awake()
    {
        gameMasterSO.ThirdPhaseInitiated += Income;
    }
    
    private void Build()
    {
        if (
        gameMasterSO.coins >= coinsCost &&
        gameMasterSO.recruits >= recruitsCost &&
        gameMasterSO.morale >= moraleCost &&
        gameMasterSO.corpses >= corpsesCost)
        {
            gameMasterSO.coins -= coinsCost;
            gameMasterSO.recruits -= recruitsCost;
            gameMasterSO.morale -= moraleCost;
            gameMasterSO.corpses -= corpsesCost;
            Debug.Log("Building is built!");
        }
        else 
        {
            Debug.Log("Not enough resources");
        }
    }
    private void Income()
    {
        gameMasterSO.coins += coinsIncome;
        gameMasterSO.recruits += recruitsIncome;
        gameMasterSO.morale += moraleIncome;
        gameMasterSO.corpses += corpsesIncome;
    }
}
