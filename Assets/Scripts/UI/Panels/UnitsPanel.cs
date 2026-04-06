using System;
using UnityEngine;
using UnityEngine.UIElements;
using Button = UnityEngine.UIElements.Button;

public class UnitsPanel : Panel
{
    [SerializeField] private GameObject soldierUnit;
    [SerializeField] private GameObject reanimatedSoldierUnit;
    private GameObject chosenUnit;
    private GameObject spawnedUnit;
    private Button closeButton;
    private Button backButton;

    private Button soldierButton;
    private Button reanimatedSoldierButton;

    private void OnEnable()
    {
        closeButton = root.Q<Button>("closeButton");
        backButton = root.Q<Button>("backButton");
        soldierButton = root.Q<Button>("soldierButton");
        reanimatedSoldierButton = root.Q<Button>("reanimatedSoldierButton");

        backButton.clicked += Return;
        closeButton.clicked += Close;
        soldierButton.clicked += () => HireUnit("soldier");
        reanimatedSoldierButton.clicked += () => HireUnit("reanimatedSoldier");
        ButtonHighlighment();
    }

    private void HireUnit(string id)
    {
        switch (id)
        {             
            case "soldier":
                chosenUnit = soldierUnit;
                break;
            case "reanimatedSoldier":
                chosenUnit = reanimatedSoldierUnit;
                break;
            default:
                throw new ArgumentException("Invalid unit ID: " + id);
        }

        if (TileCheck(1) && chosenUnit.GetComponent<CostController>().Buy())
        {
            spawnedUnit = Instantiate(chosenUnit, new Vector3(tile.transform.position.x - 0.5f, tile.transform.position.y + 0.62f, tile.transform.position.z + deltaZ), Quaternion.Euler(0, 270, 0));
            spawnedUnit.GetComponent<UnitController>().currentSlot = slot;
        }
    }
}





