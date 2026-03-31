using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using Button = UnityEngine.UIElements.Button;

public class Test : MonoBehaviour
{
    [SerializeField] private GameMasterSO gameMasterSO;
    private UIDocument UIDocument;
    private Button buildingsButton;


    void Awake()
    {
        UIDocument = GetComponent<UIDocument>();
        VisualElement root = UIDocument.rootVisualElement;

        buildingsButton = root.Q<Button>("buildingsButton");
        buildingsButton.clicked += gameMasterSO.TurnSkip;
    }
}




