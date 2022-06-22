using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Gallery : MonoBehaviour
{
    public Image cgImage;

    public Image amrzs_img;
    public Image haiz_img;
    public Image lutra_img;
    public Image obear_img;
    public Image olda_img;
    public Image padko_img;


    private void Start()
    {
        if (!ChildrenCollection.childrens.Contains("amrzs")) 
        {
            amrzs_img.color = Color.black;
            amrzs_img.GetComponent<Button>().enabled = false;
        }
        if (!ChildrenCollection.childrens.Contains("haiz")) 
        {
            haiz_img.color = Color.black;
            haiz_img.GetComponent<Button>().enabled = false;
        }
        if (!ChildrenCollection.childrens.Contains("lutra")) 
        {
            lutra_img.color = Color.black;
            lutra_img.GetComponent<Button>().enabled = false;
        }
        if (!ChildrenCollection.childrens.Contains("obear")) 
        {
            obear_img.color = Color.black;
            obear_img.GetComponent<Button>().enabled = false;
        }
        if (!ChildrenCollection.childrens.Contains("olda")) 
        {
            olda_img.color = Color.black;
            olda_img.GetComponent<Button>().enabled = false;
        }
        if (!ChildrenCollection.childrens.Contains("padko")) 
        {
            padko_img.color = Color.black;
            padko_img.GetComponent<Button>().enabled = false;
        }
    }

    void Update()
    {
        if (cgImage.gameObject.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                CloseImage();
            }
        }
    }

    public void ShowImage(Sprite sprite)
    {
        cgImage.sprite = sprite;
        cgImage.gameObject.SetActive(true);
    }

    public void CloseImage()
    {
        cgImage.gameObject.SetActive(false);
    }

    public void BackToTitle()
    {
        SceneManager.LoadScene("Title");
    }
}
