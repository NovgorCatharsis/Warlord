using UnityEngine;
using UnityEngine.UIElements;
using Button = UnityEngine.UIElements.Button;

public class SelectionWindow : MonoBehaviour
{
    private UIDocument UIDocument;

    private Button test1;
    private Button test2;


    void Awake()
    {
        UIDocument = GetComponent<UIDocument>();
        VisualElement root = UIDocument.rootVisualElement;

        test1 = root.Q<Button>("test1");
        test2 = root.Q<Button>("test2");
        test1.clicked += () => Debug.Log("Test 1 clicked");
        test2.clicked += () => Debug.Log("Test 2 clicked");
    }
}




