using Mono.Cecil.Cil;
using UnityEngine;
using UnityEngine.UIElements;
public class WorldUIController : MonoBehaviour
{
    [SerializeField] private GameObject playerObject;
    private PlayerInputController playerInputController;
    private UIDocument UIDocument;

    private RaycastHit hit;
    private Ray ray;

    private Vector2 pixelUV;
    private Vector2 mousePosition;
    private Vector2 invalidPosition = new Vector2(float.NaN, float.NaN);


    private void Awake()
    {
        UIDocument = GetComponent<UIDocument>();
        playerInputController = playerObject.GetComponent<PlayerInputController>();
    }

    private void Update()
    {
        mousePosition = playerInputController.mousePosition;
        //Debug.Log("Mouse position on enable: " + mousePosition);
        //if (mousePosition == Vector2.zero)
        //{
        //    Debug.LogWarning("Mouse position is zero during OnEnable. This may indicate that the PlayerInputController has not yet updated the mouse position.");
        //    return;
        //}
        UIDocument.panelSettings.SetScreenToPanelSpaceFunction((Vector2 screenPosition) =>
        {
            ray = Camera.main.ScreenPointToRay(mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * 1000f, Color.magenta);
            if (!Physics.Raycast(ray, out hit, 100f, LayerMask.GetMask("UI")))
            {
                Debug.Log("No hit detected");
                return invalidPosition;
            }

            pixelUV = hit.textureCoord;

            pixelUV.y = 1 - pixelUV.y;
            pixelUV.x *= UIDocument.panelSettings.targetTexture.width;
            pixelUV.y *= UIDocument.panelSettings.targetTexture.height;
            Debug.Log("PixelUV " + pixelUV);
            return pixelUV;
        });
    }
}


