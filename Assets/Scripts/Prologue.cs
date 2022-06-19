using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Prologue : MonoBehaviour
{
    [SerializeField]
    DialogSystem dialogSystem;

    private void Awake()
    {
        StartCoroutine(_Play());
    }

    IEnumerator _Play()
    {
        yield return dialogSystem.Play();
        SceneManager.LoadScene("Level1");
    }
}
