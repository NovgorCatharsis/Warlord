using UnityEngine;

public class BannerTexture : MonoBehaviour
{
    [SerializeField] private Material mat;


    private void Awake()
    {
        var renderer = GetComponent<SkinnedMeshRenderer>();
        renderer.material = mat;
    }
}
