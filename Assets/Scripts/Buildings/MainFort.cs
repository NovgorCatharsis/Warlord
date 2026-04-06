using UnityEngine;

public class MainFort : Fort
{
    internal GameObject winScreen;

    private void Awake()
    {
        winScreen = Resources.Load<GameObject>("Prefabs/UI/Win UIDocument");
    }
    public override void Destruction()
    {
        Instantiate(winScreen);
        Destroy(gameObject);
    }
}
