using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Amrzs : Interactable
{
    [SerializeField]
    private DialogSystem _dialog1;
    [SerializeField]
    private DialogSystem _dialog2;

    [SerializeField]
    private Vector3Int playerWarpPosition;
    [SerializeField]
    private Vector3Int selfWarpPosition;

    protected override IEnumerator InteractionCoroutine(Player player)
    {
        return base.InteractionCoroutine(player);
    }

    protected override IEnumerator _PlayDialog(Player player)
    {
        switch(interactCount)
        {
            case 0:
                return _Play1(player);
            default:
                return _Play2(player);
        }
    }

    IEnumerator _Play1(Player player)
    {
        yield return _dialog1.Play();

        transform.position = selfWarpPosition;
        player.transform.position = playerWarpPosition;
    }

    IEnumerator _Play2(Player player)
    {
        yield return _dialog2.Play();
    }
}
