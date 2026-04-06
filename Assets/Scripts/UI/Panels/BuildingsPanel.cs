using System;
using UnityEngine;
using UnityEngine.UIElements;
using Button = UnityEngine.UIElements.Button;

public class BuildingsPanel : Panel
{
    [SerializeField] private GameObject house;
    [SerializeField] private GameObject mine;
    private GameObject chosenBuilding;
    private GameObject builtBuilding;
    private Button closeButton;
    private Button backButton;

    private Button houseButton;
    private Button mineButton;

    private void OnEnable()
    {
        closeButton = root.Q<Button>("closeButton");
        backButton = root.Q<Button>("backButton");
        houseButton = root.Q<Button>("houseButton");
        mineButton = root.Q<Button>("mineButton");

        backButton.clicked += Return;
        closeButton.clicked += Close;
        houseButton.clicked += () => Build("house");
        mineButton.clicked += () => Build("mine");
        ButtonHighlighment();
    }

    private void Build(string id)
    {
        switch (id)
        {
            case "house":
                chosenBuilding = house;
                break;
            case "mine":
                chosenBuilding = mine;
                break;
            default:
                throw new ArgumentException("Invalid building ID: " + id);
        }

        if (TileCheck(2) && chosenBuilding.GetComponent<CostController>().Buy())
        {
            builtBuilding = Instantiate(chosenBuilding, new Vector3(tile.transform.position.x, tile.transform.position.y, tile.transform.position.z), Quaternion.Euler(0,180,0));
            tileController.building = builtBuilding;
        }
    }
}





