using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "3D Puzzling Platformer/Grid")]
public class GridData : ScriptableObject
{
    [field: SerializeField]
    public string gridName { get; private set; }
    [field: SerializeField]
    public FlatSpriteSheet flatSpriteSheet{ get; private set; }
    [field: SerializeField]
    public IsoSpriteSheet isoSpriteSheet { get; private set; }
    [field: SerializeField]
    public GridBehaviour gridBehaviour { get; private set; }
}
