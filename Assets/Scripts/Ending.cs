using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour
{
    [SerializeField]
    DialogSystem dialogSystem;


    [SerializeField] GameObject childrenCollectionGO;
    [SerializeField] ChildrenCollection childrenCollection;

    private void Awake()
    {
        StartCoroutine(_Play());
    }

    IEnumerator _Play()
    {
        childrenCollectionGO.SetActive(true);
        childrenCollection.SetSprites();
        yield return dialogSystem.Play();


        yield return new WaitUntil(() => Input.anyKeyDown);

        SceneManager.LoadScene("Title");
    }
}
