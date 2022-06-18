using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "3D Puzzling Platformer/Flat Sprite Sheet")]
public class FlatSpriteSheet : ScriptableObject
{
    public enum Mapping
    {
        topLeft,
        topMid,
        topRight,
        midLeft,
        midMid,
        midRight,
        bottomLeft,
        bottomMid,
        bottomRight,
        horizontalLeft,
        horizontalMid,
        horizontalRight,
        verticalLeft,
        verticalMid,
        verticalRight,
        isolated
    }

    [SerializeField]
    private Sprite topLeft;
    [SerializeField]
    private Sprite topMid;
    [SerializeField]
    private Sprite topRight;
    [SerializeField]
    private Sprite midLeft;
    [SerializeField]
    private Sprite midMid;
    [SerializeField]
    private Sprite midRight;
    [SerializeField]
    private Sprite bottomLeft;
    [SerializeField]
    private Sprite bottomMid;
    [SerializeField]
    private Sprite bottomRight;
    [SerializeField]
    private Sprite horizontalLeft;
    [SerializeField]
    private Sprite horizontalMid;
    [SerializeField]
    private Sprite horizontalRight;
    [SerializeField]
    private Sprite verticalLeft;
    [SerializeField]
    private Sprite verticalMid;
    [SerializeField]
    private Sprite verticalRight;
    [SerializeField]
    private Sprite isolated;

    private Dictionary<FlatSpriteSheet.Mapping, Sprite> kvp = null;

    public Sprite GetSprite(FlatSpriteSheet.Mapping mapping)
    {
        if(kvp == null)
        {
            kvp = new Dictionary<Mapping, Sprite>
            {
                {FlatSpriteSheet.Mapping.topLeft, topLeft},
                {FlatSpriteSheet.Mapping.topMid, topMid},
                {FlatSpriteSheet.Mapping.topRight, topRight},
                {FlatSpriteSheet.Mapping.midLeft, midLeft},
                {FlatSpriteSheet.Mapping.midMid, midMid},
                {FlatSpriteSheet.Mapping.midRight, midRight},
                {FlatSpriteSheet.Mapping.bottomLeft, bottomLeft},
                {FlatSpriteSheet.Mapping.bottomMid, bottomMid},
                {FlatSpriteSheet.Mapping.bottomRight, bottomRight},
                {FlatSpriteSheet.Mapping.horizontalLeft, horizontalLeft},
                {FlatSpriteSheet.Mapping.horizontalMid, horizontalMid},
                {FlatSpriteSheet.Mapping.horizontalRight, horizontalRight},
                {FlatSpriteSheet.Mapping.verticalLeft, verticalLeft},
                {FlatSpriteSheet.Mapping.verticalMid, verticalMid},
                {FlatSpriteSheet.Mapping.verticalRight, verticalRight},
                {FlatSpriteSheet.Mapping.isolated, isolated},
            };
        }
        return kvp[mapping];
    }
}