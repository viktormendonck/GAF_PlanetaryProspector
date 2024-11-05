using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class PreviewColorHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public bool CanPlace = false;
    [SerializeField] private Color notAvailable = new Color(0.8f,0.05f,0.05f,0.5f);
    [SerializeField] private Color available = new Color(1,1,1,0.5f);
    private BoxCollider2D box;
    private SpriteRenderer spriteRenderer;
    private bool hasCollisions;
    public bool hasEnoughMoney = true;

    private void Start()
    {
        box = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = available;
        hasCollisions =  GetComponentInParent<BuildButton>().hasCollisions;
        if (!hasCollisions)
        {
            CanPlace = true;
        }
    }


    private void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(box.bounds.center, box.bounds.size, 0, 1 << LayerMask.NameToLayer("Building"));
        if (hasCollisions && hasEnoughMoney)
        {
            if (colliders.Length > 0)
            {
                CanPlace = false;
                spriteRenderer.color = notAvailable;
            }
            else
            {
                CanPlace = true;
                spriteRenderer.color = available;
            }
        } else if (!hasEnoughMoney)
        {
            CanPlace = false;
            spriteRenderer.color = notAvailable;
        }
        else
        {
            CanPlace = true;
            spriteRenderer.color = available;
        }
    }
}
