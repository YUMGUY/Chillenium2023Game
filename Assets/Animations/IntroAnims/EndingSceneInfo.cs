using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EndingSceneInfo : MonoBehaviour
{
    public GameObject screen;
    public GameObject blackScreen;

    public AudioSource endingAudio;
    public AudioClip endingMusic;
    public AudioClip heartSFX;

    public float howLongBeforePlayingSFX;
    public float sfxAudioLength;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(startEndAudio());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator startEndAudio() {
        yield return new WaitForSeconds(howLongBeforePlayingSFX);
        endingAudio.clip = heartSFX;
        endingAudio.loop = false;
        endingAudio.Play();
        yield return new WaitForSeconds(sfxAudioLength);
        endingAudio.clip = endingMusic;
        endingAudio.loop = true;
        endingAudio.Play();

        yield return null;

    }

    public void EnableGoodImage()
    {
        screen.SetActive(true);
        blackScreen.SetActive(false);
    }
}
