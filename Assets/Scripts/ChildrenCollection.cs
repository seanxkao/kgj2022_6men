using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChildrenCollection : MonoBehaviour
{
    public static bool amrzs;
    public static bool haiz;
    public static bool lutra;
    public static bool obear;
    public static bool olda;

    public static HashSet<string> childrens = new HashSet<string>();

    public Image olda_image;
    public Image obear_image;
    public Image lutra_image;
    public Image amrzs_image;
    public Image haiz_image;

    public Sprite olda_sprite;
    public Sprite obear_sprite;
    public Sprite lutra_sprite;
    public Sprite amrzs_sprite;
    public Sprite haiz_sprite;

    public Sprite olda_sprite_dark;
    public Sprite obear_sprite_dark;
    public Sprite lutra_sprite_dark;
    public Sprite amrzs_sprite_dark;
    public Sprite haiz_sprite_dark;

    public void SetSprites()
    {
        olda_image.sprite = childrens.Contains("olda")
            ? olda_sprite
            : olda_sprite_dark;

        obear_image.sprite = childrens.Contains("obear")
            ? obear_sprite
            : obear_sprite_dark;

        lutra_image.sprite = childrens.Contains("lutra")
            ? lutra_sprite
            : lutra_sprite_dark;

        amrzs_image.sprite = childrens.Contains("amrzs")
            ? amrzs_sprite
            : amrzs_sprite_dark;

        haiz_image.sprite = childrens.Contains("haiz")
            ? haiz_sprite
            : haiz_sprite_dark;

        return;
        olda_image.sprite = olda ? olda_sprite : olda_sprite_dark;
        obear_image.sprite = obear ? obear_sprite : obear_sprite_dark;
        lutra_image.sprite = lutra ? lutra_sprite : lutra_sprite_dark;
        amrzs_image.sprite = amrzs ? amrzs_sprite : amrzs_sprite_dark;
        haiz_image.sprite = haiz ? haiz_sprite : haiz_sprite_dark;
    }
}
