using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Button = UnityEngine.UIElements.Button;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameMasterSO gameMasterSO;
    private UIDocument UIDocument;
    private Button repeatButton;
    private Button exitButton;

    private VisualElement root;


    private void OnEnable()
    {
        UIDocument = GetComponent<UIDocument>();
        root = UIDocument.rootVisualElement;

        repeatButton = root.Q<Button>("repeatButton");
        exitButton = root.Q<Button>("exitButton");

        repeatButton.clicked += StartOver;
        exitButton.clicked += Exit;
    }

    private void StartOver()
    {
        gameMasterSO.Annul();
        gameMasterSO.UpdateUI();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Exit()
    {
        gameMasterSO.Annul();
        Application.Quit();
    }
}




