using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadLevel : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(ReloadScene());
        }
    }

    IEnumerator ReloadScene()
    {
        const float t = 0.5f;
        const float dt = 0.02f;
        for (int i = 0; i < t/dt; i++)
        {
            Camera.main.orthographicSize *= 2;
            yield return new WaitForSecondsRealtime(dt);
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
