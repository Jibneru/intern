using UnityEngine;

public class InnerLineController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Material material;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        material = spriteRenderer.material;
    }
}
