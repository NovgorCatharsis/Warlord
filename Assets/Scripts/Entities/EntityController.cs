using UnityEngine;
using UnityEngine.UIElements;

public class EntityController : MonoBehaviour
{
    [SerializeField] internal GameMasterSO gameMasterSO;
    [SerializeField] internal float maxStrength;
    public float strength;

    internal GameObject currentTile;
    internal TileController tileController;
    internal GameObject HealthUI;
    internal int currentSlot;
    internal float deltaZ;
    public bool moved;

    internal UIDocument UIDocument;
    internal VisualElement root;
    internal Label healthLabel;

    private float distance = 5f;
    private Vector3 direction = new Vector3(0, -1, 0);
    private RaycastHit hit;


    private void Start()
    {
        currentTile = RaycastDown();
        if (currentTile == null || !currentTile.CompareTag("Tile"))
        {
            Debug.Log("Entity is not on a tile! Destroying entity.");
            Debug.Log(currentTile);

            Death();
            return;
        }
        else
        {
            tileController = currentTile.GetComponent<TileController>();
            UpdateTile();
        }

        strength = maxStrength;
        gameMasterSO.SecondPhaseInitiated += Heal;
        gameMasterSO.SecondPhaseInitiated += MoveRestoration;

        HealthUI = transform.Find("Health").gameObject;
        UIDocument = HealthUI.GetComponent<UIDocument>();
        root = UIDocument.rootVisualElement;
        healthLabel = root.Q<Label>("healthLabel");

        Heal();
    }

    internal void Heal()
    {
        if (gameObject.name.Contains("Enemy"))
        {
            if (strength < maxStrength && !tileController.fightHappened) strength += 0.5f;

            healthLabel.style.color = new StyleColor(Color.lightSalmon);
        }
        if (gameObject.CompareTag("Unit"))
        {
            if (!gameObject.name.Contains("Reanimated"))
            {
                if (gameMasterSO.morale < 0)
                {
                    strength -= 0.5f;
                    if (strength <= 0)
                    {
                        Death();
                        return;
                    }
                }
                else if (strength < maxStrength && !tileController.fightHappened)
                {
                    strength += 0.5f;
                }
            }
            if (strength / maxStrength < 0.5f) healthLabel.style.color = new StyleColor(Color.red);
            else if (strength / maxStrength < 1.0f) healthLabel.style.color = new StyleColor(Color.yellow);
            else healthLabel.style.color = new StyleColor(Color.green);
        }
        healthLabel.text = strength.ToString() + "/" + maxStrength.ToString();
    }



    internal void MoveRestoration()
    {
        moved = false;
    }

    public virtual void UpdateTile(){ }
    //public virtual void Move(GameObject newTile) { }

    internal void Death()
    {
        Destroy(gameObject);
        // Is followed by OnDisable() in children
    }


    internal GameObject RaycastDown()
    {
        if (Physics.Raycast(transform.position, direction, out hit, distance, LayerMask.GetMask("Tiles")))
        {
            return hit.transform.gameObject; // returns the object that was hit by the raycast
        }
        else return null; // if nothing was hit, return null
    }
}
