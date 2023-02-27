using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{

    public GameObject cameraToShake;
    public float magnitudeInput;
    public float durationShake;
    public AnimationCurve strength;
    public bool isShaking;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.Space ))
        //{
        //    print("shook screen");
        //    StartCoroutine(Shake(durationShake, magnitudeInput));
        //}
    }

    public IEnumerator Shake(float duration, float magnitude)
    {
        
        Vector3 originalPos = transform.localPosition;
        float elapsedTime = 0;
        while(elapsedTime < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            float value = strength.Evaluate(elapsedTime / duration);
            transform.localPosition = new Vector3( value * x, value * y, originalPos.z);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = originalPos;
        yield return null;
    }
}
