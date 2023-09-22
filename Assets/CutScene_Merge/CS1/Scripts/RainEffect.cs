using UnityEngine;
using DG.Tweening;

public class RainEffect : MonoBehaviour
{
    public float flickerRate = 0.1f;

    private SpriteRenderer spriteRenderer;
    private bool isVisible = true;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        InvokeRepeating(nameof(Flicker), 2.0f, flickerRate);
    }

    private void Flicker()
    {
        isVisible = !isVisible;
        spriteRenderer.enabled = isVisible;
    }
}
