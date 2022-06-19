using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : Interactable
{
    [SerializeField]
    private string nextSceneString;

    protected override void Interaction(Player player)
    {
        SceneManager.LoadSceneAsync(nextSceneString, LoadSceneMode.Single);
    }
}
