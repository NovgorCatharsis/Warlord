using Unity.VisualScripting;
using UnityEngine;

public class Fort : MonoBehaviour
{
    [SerializeField] internal GameMasterSO gameMasterSO;
    [SerializeField] private GameObject enemy;
    [SerializeField] private int spawnChance = 4; // Higher = lower

    private GameObject tile;
    private TileController tileController;
    private int randomIndex;


    private float distance = 5f;
    private Vector3 direction = new Vector3(0, -1, 0);
    private RaycastHit hit;

    private void OnEnable()
    {
        tile = RaycastDown();
        if (tile == null || !tile.CompareTag("Tile"))
        {
            Debug.Log("Fort is not above a tile.");
            Destruction();
        }
        tileController = tile.GetComponent<TileController>();

        gameMasterSO.SecondPhaseInitiated += SpawnEnemy;
    }

    private void OnDisable()
    {
        gameMasterSO.SecondPhaseInitiated -= SpawnEnemy;
    }

    public void SpawnEnemy()
    {
        if (tileController.unitsOnTile.Count != 0) return; // No spawn enemy if there are units on tile

        randomIndex = UnityEngine.Random.Range(0, spawnChance);
        if (randomIndex != 0) return; // Chance to spawn enemy; 1/4

        Instantiate(enemy, new Vector3(tile.transform.position.x + 0.5f, tile.transform.position.y + 0.62f, tile.transform.position.z + 0.5f), Quaternion.Euler(0, 270, 0));
    }

    public virtual void Destruction()
    {
        Destroy(gameObject);
    }

    private GameObject RaycastDown()
    {
        if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y, transform.position.z), direction, out hit, distance, LayerMask.GetMask("Tiles")))
        {
            return hit.transform.gameObject; // returns the object that was hit by the raycast
        }
        else return null; // if nothing was hit, return null
    }
}
