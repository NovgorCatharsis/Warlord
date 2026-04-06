using UnityEngine;

public class BuildingController : MonoBehaviour
{
    [SerializeField] private GameMasterSO gameMasterSO;

    [Header("Income")]
    [SerializeField] private int coinsIncome;
    [SerializeField] private int recruitsIncome;
    [SerializeField] private int moraleIncome;
    [SerializeField] private int corpsesIncome;

    private void OnEnable()
    {
        gameMasterSO.ThirdPhaseInitiated += Income;
    }

    private void OnDisable()
    {
        gameMasterSO.ThirdPhaseInitiated -= Income;
    }

    private void Income()
    {
        gameMasterSO.coins += coinsIncome;
        gameMasterSO.recruits += recruitsIncome;
        gameMasterSO.morale += moraleIncome;
        gameMasterSO.corpses += corpsesIncome;
    }

    public virtual void Destruction()
    {
        Destroy(gameObject);
    }
}
