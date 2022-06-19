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

    protected override void Interaction(Player player)
    {
        Debug.Log("Amrzs");

        switch(interactCount)
        {
            case 0:
                StartCoroutine(_Play1(player));
                break;
            default:
                StartCoroutine(_Play2(player));
                break;
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

        Destroy(gameObject);
    }
}
