using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Amrzs : Interactable
{
    [SerializeField]
    private Vector3Int playerWarpPosition;
    [SerializeField]
    private Vector3Int selfWarpPosition;

    protected override void Interaction(Player player)
    {
        switch(interactCount)
        {
            case 0:
                transform.position = selfWarpPosition;
                player.transform.position = playerWarpPosition;
                break;
            default:
                Destroy(gameObject);
                break;
        }
    }
}
