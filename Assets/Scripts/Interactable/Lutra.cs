using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lutra : Interactable
{
    [SerializeField]
    private float screenShakeDuration = 3;
    [SerializeField]
    private float screenShakeFreq = 2;
    [SerializeField]
    private float screenShakeAmp = 1;
    [SerializeField]
    private bool shake = true;
    [SerializeField]
    private bool rotate = true;

    private Vector2 xShakeSeed;
    private Vector2 yShakeSeed;
    private Vector2 zShakeSeed;
    private Vector2 xRotateSeed;
    private Vector2 yRotateSeed;
    private Vector2 zRotateSeed;

    private void Awake() 
    {
        var seed = Random.insideUnitCircle;
        xShakeSeed = seed;
        yShakeSeed = seed;
        zShakeSeed = seed;
        xRotateSeed = seed;
        yRotateSeed = seed;
        zRotateSeed = seed;
    }

    protected override void Interaction(Player player)
    {
        switch(interactCount)
        {
            case 0:

                Destroy(gameObject);
                break;
            default:
                Destroy(gameObject);
                break;
        }
    }

    IEnumerator ScreenShake()
    {
        yield return null;
        /*
        var cam = Camera.main;
        Vector3 position = cam.transform.position;
        Vector3 rotation = cam.transform.rotation.eulerAngles;
        float timer = 0;
        while(timer < screenShakeDuration)
        {
            if(shake)
            {
                
            }
            if(rotate)
            {

            }

            timer += Time.deltaTime;
            yield return null;
        }*/
    }
}
