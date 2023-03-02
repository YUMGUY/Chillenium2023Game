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
    private float animRuntimeLength;

    private Animator heartAnim;

    // Start is called before the first frame update
    void Start()
    {
        heartAnim = GetComponent<Animator>();
        animRuntimeLength = heartAnim.runtimeAnimatorController.animationClips[0].length;

        //Debug.Log(animRuntimeLength);
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

        if (animRuntimeLength - howLongBeforePlayingSFX < 0)
        {
            yield return new WaitForSeconds(0);
        }
        else {
            yield return new WaitForSeconds(animRuntimeLength - howLongBeforePlayingSFX);
        }
        
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
