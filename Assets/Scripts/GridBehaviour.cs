using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridBehaviour : MonoBehaviour
{
    public Collider c2d { get; protected set; }
    public Image image { get; protected set; }

    private void Awake() 
    {
        c2d = GetComponent<Collider>();
        image = GetComponentInChildren<Image>();    
    }

    private void Start() 
    {
        var game = Game.instance;
        if(game != null)
        {
            SetImageAxis(game.projectionAxis);
            game.AxisChange.AddListener(SetImageAxis);
        }
    }

    private void SetImageAxis(ProjectionAxis axis)
    {
        switch(axis)
        {
            case ProjectionAxis.X:
                image.rectTransform.rotation = Quaternion.Euler(0,-90,0);
                break;
            case ProjectionAxis.Y:
                image.rectTransform.rotation = Quaternion.Euler(90,180,0);
                break;
            case ProjectionAxis.Z:
                image.rectTransform.rotation = Quaternion.Euler(0,180,0);
                break;
        }
    }
}