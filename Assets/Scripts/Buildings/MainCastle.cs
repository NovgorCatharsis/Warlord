using UnityEngine;

public class MainCastle : BuildingController
{
    internal GameObject loseScreen;

    private void Awake()
    {
        loseScreen = Resources.Load<GameObject>("Prefabs/UI/Lose UIDocument");
    }
    public override void Destruction()
    {
        Instantiate(loseScreen);
        Destroy(gameObject);
    }
}
