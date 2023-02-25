using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FlashScreen : MonoBehaviour
{

    public RawImage whiteScreen;
    public Animator flashAnimator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            print("flashed screen");
            flashAnimator.SetTrigger("Flash");
        }
    }
}
