using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Button = UnityEngine.UIElements.Button;

public class SelectionPanel : Panel
{
    [SerializeField] private GameObject unitsPanel;
    [SerializeField] private GameObject buildingsPanel;
    private Button closeButton;
    private Button unitsButton;
    private Button buildingsButton;

    private void OnEnable()
    {
        closeButton = root.Q<Button>("closeButton");
        unitsButton = root.Q<Button>("unitsButton");
        buildingsButton = root.Q<Button>("buildingsButton");

        closeButton.clicked += Close;
        unitsButton.clicked += OpenUnitsPanel;
        buildingsButton.clicked += OpenBuildingsPanel;
        ButtonHighlighment();
    }

    private void OpenUnitsPanel()
    {
        Instantiate(unitsPanel, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.Euler(panelRotation, 0, 0));
        Destroy(gameObject);
    }

    private void OpenBuildingsPanel()
    {
        Instantiate(buildingsPanel, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.Euler(panelRotation, 0, 0));
        Destroy(gameObject);
    }
}





