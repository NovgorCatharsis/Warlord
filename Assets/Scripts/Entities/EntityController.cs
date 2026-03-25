using UnityEngine;

public class EntityController : MonoBehaviour
{
    [SerializeField] internal GameMasterSO gameMasterSO;
    [SerializeField] public int strength;

    internal GameObject currentTile;
    internal TileController tileController;
    internal float deltaZ;


    private float distance = 5f;
    private Vector3 direction = new Vector3(0, -1, 0);
    private RaycastHit hit;


    private void Start()
    {
        currentTile = RaycastDown();
        if (currentTile == null || !currentTile.CompareTag("Tile"))
        {
            Death();
            return;
        }
        else
        {
            tileController = currentTile.GetComponent<TileController>();
            UpdateTile();
        }
    }

    public virtual void UpdateTile(){ }
    //public virtual void Move(GameObject newTile) { }

    internal void Death()
    {
        Destroy(gameObject);
    }


    internal GameObject RaycastDown()
    {
        if (Physics.Raycast(transform.position, direction, out hit, distance))
        {
            return hit.transform.gameObject; // returns the object that was hit by the raycast
        }
        else return null; // if nothing was hit, return null
    }
}
