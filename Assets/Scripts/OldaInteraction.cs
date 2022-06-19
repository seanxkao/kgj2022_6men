using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OldaInteraction : Interactable
{
    [SerializeField]
    private DialogSystem _dialogSystem;

    protected override void Interaction(Player player)
    {
        StartCoroutine(Play());
    }

    IEnumerator Play()
    {
        yield return _dialogSystem.Play();
        SceneManager.LoadSceneAsync("Level2", LoadSceneMode.Single);
    }
}
