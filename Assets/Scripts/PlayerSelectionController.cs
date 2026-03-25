using UnityEngine;
public class PlayerSelectionController : MonoBehaviour
{
    
    [SerializeField] private GameMasterSO gameMasterSO;
    private PlayerInputController playerInputController;

    private GameObject hitObject;
    private GameObject selectedUnit;

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
        gameMasterSO.selectedUnit = null; // Annul previous selection

        hitObject = Raycast();
        if (hitObject == null) return;

        if (hitObject.CompareTag("Unit"))
        {
            gameMasterSO.selectedUnit = hitObject;
            //Debug.Log("This unit STRENGTH is: " + hitObject.GetComponent<UnitController>().strength);
        }
        else if (hitObject.CompareTag("Enemy"))
        {
            //Debug.Log("This enemy STRENGTH is: " + hitObject.GetComponent<EnemyController>().strength);
        }
    }

    private void MoveUnit()
    {
        selectedUnit = gameMasterSO.selectedUnit;
        if (selectedUnit == null) return; // No unit selected, do nothing

        hitObject = Raycast();
        if (hitObject == null) return;

        if (!hitObject.CompareTag("Tile")) return; // If the right-clicked object is not a tile, do nothing

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


