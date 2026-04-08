using UnityEngine;

public class MainCastle : BuildingController
{
    internal GameObject loseScreen;

    private void Awake()
    {
        loseScreen = Resources.Load<GameObject>("Prefabs/UI/Lose UIDocument");
    }
    
    private void Start()
    {
        gameMasterSO.Annul();
        gameMasterSO.UpdateUI();
    }

    public override void Destruction()
    {
        Instantiate(loseScreen);
        Destroy(gameObject);
    }
}
