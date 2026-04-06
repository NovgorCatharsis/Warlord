using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using Button = UnityEngine.UIElements.Button;

public class Panel : MonoBehaviour
{
    [SerializeField] internal GameMasterSO gameMasterSO;

    internal GameObject tile;
    internal TileController tileController;
    internal GameObject selectionPanel;
    internal UIDocument UIDocument;
    internal VisualElement root;
    internal Texture2D texture;
    internal Texture2D highlightTexture;

    internal bool highlighted = false;
    internal float deltaZ;
    internal float panelRotation;
    internal int slot;

    internal float distance = 5f;
    internal Vector3 direction = new Vector3(0, -1, 0);
    internal RaycastHit hit;

    private void Awake()
    {
        UIDocument = GetComponent<UIDocument>();
        root = UIDocument.rootVisualElement;
        selectionPanel = Resources.Load<GameObject>("Prefabs/Panels/Selection Panel");

        panelRotation = Camera.main.transform.rotation.eulerAngles.x;
    }

    internal void ButtonHighlighment() 
    {
        foreach (Button button in root.Query<Button>().ToList())
        {
            button.RegisterCallback<PointerEnterEvent>(e =>
            {
                if (!highlighted)
                {
                    texture = button.resolvedStyle.backgroundImage.texture;
                    highlightTexture = Resources.Load<Texture2D>("Materials/UI/" + texture.name + "_Highlighted");
                    button.style.backgroundImage = new StyleBackground(highlightTexture);
                    highlighted = true;
                }
            });
            button.RegisterCallback<PointerLeaveEvent>(e =>
            {
                if (highlighted)
                {
                    texture = button.resolvedStyle.backgroundImage.texture;
                    if (texture.name.Contains("_Highlighted")) texture = Resources.Load<Texture2D>("Materials/UI/" + texture.name.Substring(0, texture.name.LastIndexOf("_Highlighted")));
                    button.style.backgroundImage = new StyleBackground(texture);
                    highlighted = false;
                }
            });
        }
    }

    internal bool TileCheck(int mode)
    {
        tile = RaycastDown();
        if (tile == null || !tile.CompareTag("Tile"))
        {
            Debug.Log("Panel is not above a tile.");
            return false;
        }
        tileController = tile.GetComponent<TileController>();

        if (mode == 1)
        {
            if (tileController.unitsOnTile.Count() >= 3)
            {
                Debug.Log("Tile is at maximum capacity, you can't hire units here anymore.");
                return false;
            }

            for (int i = 0; i < tileController.unitsSlots.Length; i++)
            {
                if (tileController.unitsSlots[i] == 0)
                {
                    tileController.unitsSlots[i] = 1; // Occupy the slot on the new tile

                    slot = i;
                    deltaZ = 0.5f - (i * 0.5f);
                    break;
                }
            }
        }
        else if (mode == 2) 
        {
            if (tileController.building != null)
            {
                Debug.Log("Tile already has a building on it, you can't build here.");
                return false;
            }
        }
        return true;
    }
    

    internal void Return()
    {
        Instantiate(selectionPanel, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.Euler(panelRotation, 0, 0));
        Destroy(gameObject);
    }

    internal void Close()
    {
        tile = RaycastDown();
        if (tile != null && tile.CompareTag("Tile"))
        {
            tile.GetComponent<TileController>().panelOpened = false;
        }
        Destroy(gameObject);
    }

    internal GameObject RaycastDown()
    {
        if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.7f), direction, out hit, distance, LayerMask.GetMask("Tiles")))
        {
            return hit.transform.gameObject; // returns the object that was hit by the raycast
        }
        else return null; // if nothing was hit, return null
    }
}





