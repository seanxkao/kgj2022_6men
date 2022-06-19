using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Interactable : MonoBehaviour
{
    [field: SerializeField]
    public string id { get; protected set; }
    [SerializeField]
    private LayerMask detectLayer;
    [SerializeField]
    private bool canInteract = false;
    [SerializeField]
    private DialogSystem dialogSystem;

    [SerializeField]
    private bool _shouldGoToNextScene;
    [SerializeField]
    private string _nextSceneName;
    [SerializeField]
    private bool destroyAfterDialogComplete = false;

    [SerializeField]
    public int fuckCount;

    public int interactCount { get; protected set; } = 0;

    private void StartInteract(Player player) 
    {
        StartCoroutine(InteractionCoroutine(player));
    }

    protected virtual IEnumerator _PlayDialog(Player player)
    {
        if (dialogSystem != null)
        {
            yield return dialogSystem.Play();
        }
    }

    protected virtual IEnumerator InteractionCoroutine(Player player)
    {
        yield return _PlayDialog(player);

        Interaction(player);
        if(interactCount >= fuckCount)
        {
            ChildrenCollection.childrens.Add(id);
        }
        interactCount++;

        if (_shouldGoToNextScene)
        {
            SceneManager.LoadSceneAsync(_nextSceneName, LoadSceneMode.Single);
        }

        if (destroyAfterDialogComplete)
        {
            Destroy(gameObject);
        }
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
