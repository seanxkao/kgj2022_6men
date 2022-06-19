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

    private Vector2 seed;

    private void Awake() 
    {
        seed = Random.insideUnitCircle * 100;
    }

    protected override void Interaction(Player player)
    {
        switch(interactCount)
        {
            case 0:
                foreach(Transform child in transform)
                    GameObject.Destroy(child.gameObject);
                var components = GetComponents(typeof(Component));
                foreach(var component in components)
                {
                    if(component != this && !(component is Transform))
                        Destroy(component);
                }
                player.StartCoroutine(ScreenShake());
                Destroy(gameObject);
                break;
            default:
                break;
        }
    }

    IEnumerator ScreenShake()
    {
        var cam = Camera.main;
        var game = Game.instance;
        var axis = game.projectionAxis;
        Vector3 position = cam.transform.position;
        Vector3 rotation = cam.transform.rotation.eulerAngles;
        float timer = 0;
        while(timer < screenShakeDuration)
        {
            if(axis != game.projectionAxis)
            {
                position = cam.transform.position;
                rotation = cam.transform.rotation.eulerAngles;
                axis = game.projectionAxis;
            }
            float x = (Mathf.PerlinNoise(seed.x + timer * screenShakeFreq, seed.y) - 0.5f) * screenShakeAmp;
            float y = (Mathf.PerlinNoise(seed.x, seed.y + timer * screenShakeFreq) - 0.5f) * screenShakeAmp;
            float z = (Mathf.PerlinNoise(seed.x - timer * screenShakeFreq, seed.y - timer * screenShakeFreq) - 0.5f) * screenShakeAmp;
            if(shake)
                cam.transform.position = position + new Vector3(x,y,z);
            if(rotate)
                cam.transform.rotation = Quaternion.Euler(rotation + new Vector3(x,y,z) * 20f);

            timer += Time.deltaTime;
            yield return null;
        }

        if(axis != game.projectionAxis)
        {
            position = cam.transform.position;
            rotation = cam.transform.rotation.eulerAngles;
            axis = game.projectionAxis;
        }
        cam.transform.position = position;
        cam.transform.rotation = Quaternion.Euler(rotation);

    }
}
