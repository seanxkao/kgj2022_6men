using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoEntrySign : MonoBehaviour
{
    [SerializeField]
    private Image sign;
    [SerializeField]
    private float clearTime = 0.3f;
    private float clearTimer;

    private void Start() 
    {
        clearTimer = 0;    
    }

    public void Show() { clearTimer = clearTime; }

    // Update is called once per frame
    void Update()
    {
        sign.color = new Color(1,1,1, clearTimer / clearTime);
        clearTimer = Mathf.Clamp(clearTimer - Time.deltaTime, 0, clearTime);
    }
}
