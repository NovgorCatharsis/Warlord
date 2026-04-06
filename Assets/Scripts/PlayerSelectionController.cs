using UnityEngine;
public class PlayerSelectionController : MonoBehaviour
{
    [SerializeField] private GameMasterSO gameMasterSO;
    [SerializeField] private GameObject selectionPanel;
    private PlayerInputController playerInputController;

    private GameObject hitObject;
    private GameObject selectedUnit;
    private GameObject[] connectedTiles;

    private RaycastHit hit;
    private Ray ray;
    

    private void Awake()
    {
        playerInputController = gameObject.GetComponent<PlayerInputController>();
        playerInputController.LeftClickPressed += SelectObject;
        playerInputController.RightClickPressed += MoveUnit;
    }


    private void SelectObject()
    {
        DeselectUnit();

        hitObject = Raycast();
        if (hitObject == null)
        {
            return;
        }

        if (hitObject.CompareTag("Unit"))
        {
            gameMasterSO.selectedUnit = hitObject;
            hitObject.transform.position = new Vector3(hitObject.transform.position.x, hitObject.transform.position.y + 0.25f, hitObject.transform.position.z);
            
            connectedTiles = hitObject.GetComponent<UnitController>().tileController.connectedTiles;
            if (hitObject.GetComponent<UnitController>().moved) return;
            if (hitObject.GetComponent<UnitController>().tileController.enemiesOnTile.Count > 0) return;
            foreach (GameObject tile in connectedTiles)
            {
                tile.transform.Find("Highlight").gameObject.SetActive(true); // Highlight connected tiles
            }
        }
        else if (hitObject.CompareTag("Tile"))
        {
            if (hitObject.GetComponent<TileController>().underControl && !hitObject.GetComponent<TileController>().panelOpened)
            {
                hitObject.GetComponent<TileController>().panelOpened = true;
                Instantiate(selectionPanel, new Vector3(hitObject.transform.position.x, hitObject.transform.position.y + 1.4f, hitObject.transform.position.z - 0.7f), Quaternion.Euler(45, 0, 0));
            }
        }
    }

    private void DeselectUnit() 
    {
        if (gameMasterSO.selectedUnit != null) // Deselect previously selected unit
        {
            gameMasterSO.selectedUnit.transform.position = new Vector3(gameMasterSO.selectedUnit.transform.position.x, gameMasterSO.selectedUnit.transform.position.y - 0.25f, gameMasterSO.selectedUnit.transform.position.z);
            if (connectedTiles.Length > 0)
            {
                foreach (GameObject tile in connectedTiles)
                {
                    tile.transform.Find("Highlight").gameObject.SetActive(false); // Highlight connected tiles
                }
            }
        }
        gameMasterSO.selectedUnit = null;
    }

    private void MoveUnit()
    {
        selectedUnit = gameMasterSO.selectedUnit;
        if (selectedUnit == null) return; // No unit selected, do nothing
        if (selectedUnit.GetComponent<UnitController>().moved) return; // If already moved this turn, do nothing
        if (selectedUnit.GetComponent<UnitController>().tileController.enemiesOnTile.Count > 0) return; // Enemies block movement

        hitObject = Raycast();
        if (hitObject == null) return;

        if (!hitObject.CompareTag("Tile")) return; // If the right-clicked object is not a tile, do nothing

        DeselectUnit();
        selectedUnit.GetComponent<UnitController>().Move(hitObject); //Move unit to tile
    }


    private GameObject Raycast() 
    {
        ray = Camera.main.ScreenPointToRay(playerInputController.mousePosition); //From mouse position

        if (Physics.Raycast(ray, out hit))
        {
            return hit.transform.gameObject; // returns the object that was hit by the raycast
        }
        else return null; // if nothing was hit, return null
    }
}


