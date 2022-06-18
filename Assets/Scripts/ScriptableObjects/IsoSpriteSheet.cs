using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "3D Puzzling Platformer/Isometric Sprite Sheet")]
public class IsoSpriteSheet : ScriptableObject
{
    [SerializeField]
    private Sprite baseSprite;

    public Sprite GetSprite()
    {
        return baseSprite;
    }
}