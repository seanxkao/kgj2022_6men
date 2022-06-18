using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBehaviour : MonoBehaviour
{
    public GridData data { get; private set; }
    public Collider2D c2d { get; protected set; }
    public SpriteRenderer sr { get; protected set; }
    public virtual void Construct(GridData data)
    {
        this.data = data; 
        this.c2d = gameObject.AddComponent(typeof(BoxCollider2D)) as BoxCollider2D;
        this.sr = gameObject.AddComponent(typeof(SpriteRenderer)) as SpriteRenderer;
        sr.sprite = data.flatSpriteSheet.GetSprite(FlatSpriteSheet.Mapping.midMid);
    }

    public virtual void UpdateGrid(SectionAxis axis)
    {
        sr.sprite = data.flatSpriteSheet.GetSprite(FlatSpriteSheet.Mapping.midMid);
    }
}