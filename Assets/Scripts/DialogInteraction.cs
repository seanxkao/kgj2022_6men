using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogInteraction : Interactable
{
    [SerializeField]
    private bool _shouldGoToNextScene;
    [SerializeField]
    private string _nextSceneName;
    [SerializeField]
    private DialogSystem _dialogSystem;
    [SerializeField]
    private bool destroyAfterDialogComplete = false;

    protected override void Interaction(Player player)
    {
        StartCoroutine(Play());
    }

    IEnumerator Play()
    {
        yield return _dialogSystem.Play();
        if(_shouldGoToNextScene)
        {
            SceneManager.LoadSceneAsync(_nextSceneName, LoadSceneMode.Single);
        }
        if(destroyAfterDialogComplete)
            Destroy(gameObject);
    }
}
