using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBrush : MonoBehaviour {

    public Vector2 brushSize = Vector2.zero;
    public int tileID = 0;
    public SpriteRenderer spriteRenderer;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireCube(transform.position, brushSize);
    }

    public void UpdateBrush(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }
}
