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
    [SerializeField]
    Button _galleryButton;

    void Awake()
    {
        _startButton.onClick.AddListener(_StartGame);
        _exitButton.onClick.AddListener(_ExitGame);
        _galleryButton.onClick.AddListener(_LoadGallery);
    }

    void _StartGame()
    {
        SceneManager.LoadScene("Prologue");
    }

    void _ExitGame()
    {
        Application.Quit();
    }

    void _LoadGallery()
    {
        SceneManager.LoadScene("Gallery");
    }
}
