using UnityEngine;

public class CostController : MonoBehaviour
{
    [SerializeField] private GameMasterSO gameMasterSO;

    [Header("Cost")]
    [SerializeField] private int coinsCost;
    [SerializeField] private int recruitsCost;
    [SerializeField] private int moraleCost;
    [SerializeField] private int corpsesCost;

    
    public bool Buy()
    {
        if (
        gameMasterSO.coins >= coinsCost &&
        gameMasterSO.recruits >= recruitsCost &&
        gameMasterSO.corpses >= corpsesCost)
        {
            gameMasterSO.coins -= coinsCost;
            gameMasterSO.recruits -= recruitsCost;
            gameMasterSO.morale -= moraleCost;
            gameMasterSO.corpses -= corpsesCost;
            Debug.Log("Object is bought!");
            gameMasterSO.UpdateUI();
            return true;
        }
        else 
        {
            Debug.Log("Not enough resources");
            return false;
        }
    }
}
