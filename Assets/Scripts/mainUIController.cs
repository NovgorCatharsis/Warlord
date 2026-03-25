using UnityEngine;
using UnityEngine.UIElements;
using Button = UnityEngine.UIElements.Button;

public class mainUIController : MonoBehaviour
{
    [SerializeField] private GameMasterSO gameMasterSO;
    private UIDocument UIDocument;
    private Button turnSkipButton;
    private Label coinsLabel;
    private Label recruitsLabel;
    private Label moraleLabel;
    private Label corpsesLabel;

    void Awake()
    {
        UIDocument = GetComponent<UIDocument>();
        VisualElement root = UIDocument.rootVisualElement;

        turnSkipButton = root.Q<Button>("turnSkipButton");
        turnSkipButton.clicked += gameMasterSO.TurnSkip;

        gameMasterSO.ResourcesUpdated += UpdateResources;
        coinsLabel = root.Q<Label>("coinsLabel");
        recruitsLabel = root.Q<Label>("recruitsLabel");
        moraleLabel = root.Q<Label>("moraleLabel");
        corpsesLabel = root.Q<Label>("corpsesLabel");
    }


    public void UpdateLabel(string id, int value)
    {
        switch (id)
        {
            case "coins":
                coinsLabel.text = $"Coins: {value}";
                break;
            case "recruits":
                recruitsLabel.text = $"Recruits: {value}";
                break;
            case "morale":
                moraleLabel.text = $"Morale: {value}";
                break;
            case "corpses":
                corpsesLabel.text = $"Corpses: {value}";
                break;
        }
    }

    public void UpdateResources()
    {
        UpdateLabel("coins", gameMasterSO.coins);
        UpdateLabel("recruits", gameMasterSO.recruits);
        UpdateLabel("morale", gameMasterSO.morale);
        UpdateLabel("corpses", gameMasterSO.corpses);
    }
}




