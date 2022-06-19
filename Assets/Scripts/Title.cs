using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Title : MonoBehaviour
{
    [SerializeField]
    Button _startButton;
    [SerializeField]
    Button _exitButton;

    void Awake()
    {
        _startButton.onClick.AddListener(_StartGame);
        _exitButton.onClick.AddListener(_ExitGame);
    }

    void _StartGame()
    {
        SceneManager.LoadScene("Prologue");
    }

    void _ExitGame()
    {
        Application.Quit();
    }
}
