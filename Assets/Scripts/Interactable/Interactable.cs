using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [field: SerializeField]
    public string id { get; protected set; }
    [SerializeField]
    private LayerMask detectLayer;
    [SerializeField]
    private bool canInteract = false;
    [SerializeField]
    private DialogSystem _dialogSystem;
    [SerializeField]
    public int fuckCount;

    public int interactCount { get; protected set; } = 0;

    private void StartInteract(Player player) 
    {
        StartCoroutine(InteractionCoroutine(player));
    }

    private IEnumerator InteractionCoroutine(Player player)
    {
        if (_dialogSystem != null)
        {
            yield return _dialogSystem.Play();
        }

        Interaction(player);
        Debug.Log("Interact");
        if(interactCount >= fuckCount)
        {
            ChildrenCollection.childrens.Add(id);
        }

        interactCount++; 
    }

    protected virtual void Interaction(Player player) {}

    private void OnTriggerEnter(Collider other) 
    {
        if((1 << other.gameObject.layer | detectLayer) == detectLayer)
        {
            canInteract = true;
            Player player = null;
            if(other.gameObject.TryGetComponent(out player))
                player.Interact.AddListener(StartInteract);
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if((1 << other.gameObject.layer | detectLayer) == detectLayer)
        {
            canInteract = false;
            Player player = null;
            if(other.gameObject.TryGetComponent(out player))
                player.Interact.RemoveListener(StartInteract);
        }
    }
}
