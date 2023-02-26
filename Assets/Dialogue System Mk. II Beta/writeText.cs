using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class writeText : MonoBehaviour
{

    public newConvoManager convoManager;
    public GameManager gameManagerRef;

    //public float typingSpeed;
    public TextMeshProUGUI textPanel;
    public TextMeshProUGUI namePanel;
    public CanvasGroup notepadCanvas;

    private State state = State.COMPLETED;

    public TextBox currentDialogue;

    public bool convoStarted;

    public GameObject leftCharSlot;
    public GameObject rightCharSlot;
    public GameObject centerCharSlot;

    public Button QuestioningButton;
    public AudioClip questionSFX;

    [Header("Reactionary Variables")]
    public ScreenShake shaker;
    public FlashScreen flasher;
    public bool isAnimatingText;
    public EmotionalTextMaker textEmotioner;

    private bool hasChoices;

    public GameObject[] choiceButtonArr;

    private int numOfButtons;

    [Header("Audio Sound Effects")]
    public AudioSource textSFXPlayer;
    public AudioSource genSFXPlayer;
    public AudioSource jukeboxPlayer;

    [Header("Notepad properties")]
    public bool notePadOpened;
    public TextMeshProUGUI page1;
    public TextMeshProUGUI page2;

    [Header("Character Animator Properties")]
    public Animator[] animatorChars;

    [Header("Character Emotion Properties")]
    public Image[] characterBodies;



    private enum State
    {
        TALKING, COMPLETED
    }

    // Start is called before the first frame update
    void Start()
    {
        hasChoices = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (!hasChoices)
        {
            if (currentDialogue.convo.autoAdvance && convoStarted)
            {
                if (state == State.COMPLETED) // if the line is completed
                {
                    convoManager.ConvoCompleteCalc(currentDialogue, false, -1);
                }
            }
            else if (Input.GetKeyDown(KeyCode.Z) && convoStarted && !notePadOpened)
            {

                if (state == State.COMPLETED) // if the line is completed
                {
                    convoManager.ConvoCompleteCalc(currentDialogue, false, -1);
                }

                else // in the midst of typing
                {
                    StopAllCoroutines();
                    textPanel.text = currentDialogue.convo.convoText;
                    state = State.COMPLETED;

                }
            }
        }
        else
        {
            if (convoStarted && state == State.COMPLETED)
            {
                showChoices();
            }
        }

    }

    public void showChoices()
    {

        numOfButtons = currentDialogue.convo.buttonText.Length;

        if (numOfButtons == 0)
        {
            return;
        }

        //Debug.Log(numOfButtons);


        //Debug.Log(choiceButtonArr[0]);

        for (int i = 0; i < numOfButtons; i++)
        {

            Debug.Log(choiceButtonArr[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().name);

            choiceButtonArr[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = currentDialogue.convo.buttonText[i];
            choiceButtonArr[i].SetActive(true);
        }
    }

    public void choice1Button()
    {
        for (int i = 0; i < numOfButtons; i++)
        {
            choiceButtonArr[i].SetActive(false);
        }
        convoManager.ConvoCompleteCalc(currentDialogue, false, 1);

    }
    public void choice2Button()
    {
        for (int i = 0; i < numOfButtons; i++)
        {
            choiceButtonArr[i].SetActive(false);
        }
        convoManager.ConvoCompleteCalc(currentDialogue, false, 2);
    }
    public void choice3Button()
    {
        for (int i = 0; i < numOfButtons; i++)
        {
            choiceButtonArr[i].SetActive(false);
        }
        convoManager.ConvoCompleteCalc(currentDialogue, false, 3);
    }

    public void holdUpButton()
    {

        genSFXPlayer.clip = questionSFX;
        genSFXPlayer.Play();

        if (convoStarted == true)
        {
            if (state == State.COMPLETED)
            {
                convoManager.ConvoCompleteCalc(currentDialogue, true, -1);
            }
            else // in the midst of typing
            {
                StopAllCoroutines();
                textPanel.text = currentDialogue.convo.convoText;
                state = State.COMPLETED;
                convoManager.ConvoCompleteCalc(currentDialogue, true, -1);
            }
        }
    }

    public void startConvo(TextBox currDialogue)
    {
        currentDialogue = currDialogue;
        convoStarted = true;

        if (currentDialogue.convo.IncreaseHusbandHeart || currentDialogue.convo.IncreaseWifeHeart)
        {
            gameManagerRef.modifyHeartPos(currDialogue.convo.IncreaseHusbandHeart, currDialogue.convo.IncreaseWifeHeart, currDialogue.convo.HusbandHeartIncreaseInterval, currDialogue.convo.WifeHeartIncreaseInterval);
        }

        leftCharSlot.SetActive(currentDialogue.convo.LeftCharOn);
        rightCharSlot.SetActive(currentDialogue.convo.RightCharOn);
        centerCharSlot.SetActive(currentDialogue.convo.CenterCharOn);

        if (currentDialogue.convo.nameOfFlag != null && currentDialogue.convo.nameOfFlag != "")
        {
            gameManagerRef.raiseFlag(currentDialogue.convo.nameOfFlag);
        }

        if (currentDialogue.convo.ShouldLeadToChoice)
        {
            hasChoices = true;
        }
        else
        {
            hasChoices = false;
        }



        QuestioningButton.interactable = currentDialogue.convo.holdButtonState;


        // enable ui 
        //dialogueItems.SetActive(true); // polish later on by adding text box animations for beginning/end
        //character1.SetActive(true);
        // character2.SetActive(true);

        StartCoroutine(typeText(currentDialogue.convo.convoText));
    }

    public IEnumerator typeText(string text)
    {

        try
        {
            namePanel.text = currentDialogue.convo.character.speakerName;
        }
        catch
        {
            namePanel.text = "???";
        }

        try
        {
            namePanel.color = currentDialogue.convo.character.nameColor;
        }
        catch
        {
            namePanel.color = Color.white;
        }


        textPanel.text = "";

        state = State.TALKING;

        try
        {
            int label = currentDialogue.convo.character.characterLabel;
        }
        catch
        {
            int label = 0;
        }


        try
        {
            for (int i = 0; i < characterBodies.Length; ++i)
            {
                if (currentDialogue.convo.currentCharacterEmotions[i] == null) { continue; }
                characterBodies[i].sprite = currentDialogue.convo.currentCharacterEmotions[i];
            }

        }
        catch
        {
            print("not enough emotions for all characters");
        }


        //showEmotion(emotionParam, label);
        // apply blanket text changes
        try
        {
            if (currentDialogue.convo.textColor_.a <= 0) { textPanel.color = Color.black; }
            else { textPanel.color = currentDialogue.convo.textColor_; }

            if (currentDialogue.convo.inputFontSize <= 0)
            {
                textPanel.fontSize = 36f;
            }
            else
            {
                textPanel.fontSize = currentDialogue.convo.inputFontSize;
            }

            if (currentDialogue.convo.isBold)
            {
                textPanel.fontStyle = FontStyles.Bold;
            }
            else
            {
                textPanel.fontStyle = FontStyles.Normal;
            }

            if (currentDialogue.convo.isWavy) { textEmotioner.wavyText = true; }
            if (currentDialogue.convo.isWiggling) { textEmotioner.wiggleText = true; }
        }
        catch
        {
            textPanel.color = Color.black;
            textPanel.fontSize = 36f;
        }

        int charIndex = 0;

        while (state != State.COMPLETED)
        {

            if (currentDialogue.convo.ShouldPlayShake)
            {
                if (charIndex == currentDialogue.convo.ShakeAtWhatCharIdx)
                {
                    // Make the screen shake!
                    Debug.Log("Screen shook");
                    // screenshake script reference
                    StartCoroutine(shaker.Shake(currentDialogue.convo.durationShake_, currentDialogue.convo.intensity_));
                }
            }
            if (currentDialogue.convo.ShouldPlayFlash)
            {
                if (charIndex == currentDialogue.convo.FlashAtWhatCharIdx)
                {
                    // Make the screen flash!   
                    Debug.Log("Screen flashed");
                    flasher.flashAnimator.SetTrigger("Flash");
                }
            }
            if (currentDialogue.convo.ShouldPlaySound)
            {
                if (charIndex == currentDialogue.convo.SoundAtWhatCharIdx)
                {
                    // Play a sound!   
                    Debug.Log("Sound played");
                    genSFXPlayer.PlayOneShot(currentDialogue.convo.soundToPlay);
                }
            }
            if (currentDialogue.convo.ShouldPlayAnimation)
            {
                if (charIndex == currentDialogue.convo.AnimationAtWhatCharIdx)
                {
                    // Make the object animate!   
                    Debug.Log("Thing Animated");
                    //currentDialogue.convo.animToPlay.Play();
                    //currentDialogue.convo.whichObjToAnim.SetTrigger("hop");
                    for (int i = 0; i < animatorChars.Length; ++i)
                    {
                        AnimationClip[] clips = animatorChars[i].runtimeAnimatorController.animationClips;
                        foreach (AnimationClip clip in clips)
                        {
                            //print(clip.name);
                            if (clip.name == currentDialogue.convo.animationName[i])
                            {
                                print("played correct animation");
                                animatorChars[i].Play(clip.name);
                            }
                        }
                    }
                }
            }


            // add audio clip of text here
            try
            {

                if (currentDialogue.convo.ShouldPlayTextSound && text[charIndex] != ' ')
                {
                    // print("play text sound");
                    textSFXPlayer.clip = currentDialogue.convo.textSound;
                    textSFXPlayer.Play();
                }

            }
            catch
            {
                print("Either missing audio source or no audio clip is provided");
            }

            try
            {
                if (charIndex == currentDialogue.convo.musicAtWhatCharIdx)
                {
                    if (currentDialogue.convo.ShouldUpdateBGM)
                    {
                        jukeboxPlayer.Stop();
                        if (currentDialogue.convo.musicToPlay != null) {
                            jukeboxPlayer.clip = currentDialogue.convo.musicToPlay;
                            jukeboxPlayer.Play();
                            jukeboxPlayer.loop = true;
                        }
                        
                    }
                }
            }
            catch { 
                
            }


            textPanel.text += text[charIndex];
            yield return new WaitForSeconds(currentDialogue.convo.typingSpeed);

            if (++charIndex >= text.Length)
            {
                // print("finished");
                state = State.COMPLETED;
                break;
            }
        }

        yield return null;
    }



    public void NextLine(TextBox newDialogue) // controls the value of convoIndex, which is the marker for which speaker is speaking
    {


        if (newDialogue != null)
        {
            currentDialogue = newDialogue;
            textPanel.text = string.Empty;

            //added for text effects (animation)
            textEmotioner.wiggleText = false;
            textEmotioner.wavyText = false;

            if (currentDialogue.convo.IncreaseHusbandHeart || currentDialogue.convo.IncreaseWifeHeart)
            {
                gameManagerRef.modifyHeartPos(currentDialogue.convo.IncreaseHusbandHeart, currentDialogue.convo.IncreaseWifeHeart, currentDialogue.convo.HusbandHeartIncreaseInterval, currentDialogue.convo.WifeHeartIncreaseInterval);
            }

            leftCharSlot.SetActive(currentDialogue.convo.LeftCharOn);
            rightCharSlot.SetActive(currentDialogue.convo.RightCharOn);
            centerCharSlot.SetActive(currentDialogue.convo.CenterCharOn);

            if (currentDialogue.convo.nameOfFlag != null && currentDialogue.convo.nameOfFlag != "")
            {
                gameManagerRef.raiseFlag(currentDialogue.convo.nameOfFlag);
            }

            QuestioningButton.interactable = currentDialogue.convo.holdButtonState;

            if (currentDialogue.convo.ShouldLeadToChoice)
            {
                hasChoices = true;
            }
            else
            {
                hasChoices = false;
            }

            StartCoroutine(typeText(currentDialogue.convo.convoText));
        }

        else
        {
            print("Jump to Final Ending");
            textPanel.text = string.Empty;
            // temporary disappearance of dialogue box and characters
            /*
            if (dialogueItems.activeInHierarchy)
            {
                dialogueItems.SetActive(false);
                character1.SetActive(false);
                //character2.SetActive(false);
            }
            */

            convoStarted = false;

            gameManagerRef.gameEnd();
        }
    }

    public void OpenNotePad()
    {
        /*
        // count flags first to determine page 1 or page 2
        int number = gameManagerRef.flagManager.Count;
        // do later
        gameManagerRef.flagManager.Keys.ToString();*/
        notepadCanvas.alpha = 1;
        notepadCanvas.blocksRaycasts = true;

        notePadOpened = true;
    }
    public void CloseNotePad()
    {

        notepadCanvas.alpha = 0;
        notepadCanvas.blocksRaycasts = false;

        notePadOpened = false;
    }
}
