using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Haiz : Interactable
{
    protected override void Interaction(Player player)
    {
        Destroy(gameObject);
    }
}