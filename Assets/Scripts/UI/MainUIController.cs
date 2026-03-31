using UnityEngine;
using UnityEngine.UIElements;
using Button = UnityEngine.UIElements.Button;

public class MainUIController : MonoBehaviour
{
    [SerializeField] private GameMasterSO gameMasterSO;
    private UIDocument UIDocument;
    private Button turnSkipButton;
    private Label coinsLabel;
    private Label recruitsLabel;
    private Label moraleLabel;
    private Label corpsesLabel;

    private VisualElement root;
    private VisualElement container;
    private VisualElement header;
    private VisualElement footer;

    void Awake()
    {
        UIDocument = GetComponent<UIDocument>();
        root = UIDocument.rootVisualElement;

        container = root.Q<VisualElement>("Container");
        header = root.Q<VisualElement>("Header");
        footer = root.Q<VisualElement>("Footer");

        turnSkipButton = root.Q<Button>("turnSkipButton");

        coinsLabel = root.Q<Label>("coinsLabel");
        recruitsLabel = root.Q<Label>("recruitsLabel");
        moraleLabel = root.Q<Label>("moraleLabel");
        corpsesLabel = root.Q<Label>("corpsesLabel");

        turnSkipButton.clicked += gameMasterSO.TurnSkip;
        gameMasterSO.ResourcesUpdated += UpdateResources;

        container.pickingMode = PickingMode.Ignore;
        header.pickingMode = PickingMode.Ignore;
        footer.pickingMode = PickingMode.Ignore;
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




