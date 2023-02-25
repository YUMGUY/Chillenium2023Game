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

    private State state = State.COMPLETED;

    public TextBox currentDialogue;

    public bool convoStarted;

    public GameObject leftCharSlot;
    public GameObject rightCharSlot;
    public GameObject centerCharSlot;

    public Button QuestioningButton;

    [Header("Reactionary Variables")]
    public ScreenShake shaker;
    public FlashScreen flasher;


    private enum State
    {
        TALKING, COMPLETED
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (currentDialogue.convo.autoAdvance && convoStarted) {
            if (state == State.COMPLETED) // if the line is completed
            {
                convoManager.ConvoCompleteCalc(currentDialogue, false);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Z) && convoStarted)
        {

            if (state == State.COMPLETED) // if the line is completed
            {
                convoManager.ConvoCompleteCalc(currentDialogue, false);
            }

            else // in the midst of typing
            {
                StopAllCoroutines();
                textPanel.text = currentDialogue.convo.convoText;
                state = State.COMPLETED;

            }
        }
    }

    public void holdUpButton() {
        if (convoStarted == true) {
            if (state == State.COMPLETED) {
                convoManager.ConvoCompleteCalc(currentDialogue, true);
            }
            else // in the midst of typing
            {
                StopAllCoroutines();
                textPanel.text = currentDialogue.convo.convoText;
                state = State.COMPLETED;
                convoManager.ConvoCompleteCalc(currentDialogue, true);
            }
        }
    }

    public void startConvo(TextBox currDialogue)
    {
        currentDialogue = currDialogue;
        convoStarted = true;

        if (currentDialogue.convo.IncreaseHusbandHeart || currentDialogue.convo.IncreaseWifeHeart) {
            gameManagerRef.modifyHeartPos(currDialogue.convo.IncreaseHusbandHeart, currDialogue.convo.IncreaseWifeHeart, currDialogue.convo.HusbandHeartIncreaseInterval, currDialogue.convo.WifeHeartIncreaseInterval);
        }

        leftCharSlot.SetActive(currentDialogue.convo.LeftCharOn);
        rightCharSlot.SetActive(currentDialogue.convo.RightCharOn);
        centerCharSlot.SetActive(currentDialogue.convo.CenterCharOn);

        if (currentDialogue.convo.nameOfFlag != null && currentDialogue.convo.nameOfFlag != "")
        {
            gameManagerRef.raiseFlag(currentDialogue.convo.nameOfFlag);
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
            namePanel.text = currentDialogue.convo.character.name;
        }
        catch {
            namePanel.text = "???";
        }

        try
        {
            namePanel.color = currentDialogue.convo.character.nameColor;
        }
        catch { 
            namePanel.color = Color.white;
        }


        textPanel.text = "";
        
        state = State.TALKING;

        try
        {
            int label = currentDialogue.convo.character.characterLabel;
        }
        catch {
            int label = 0;
        }


        try
        {
            Sprite emotionParam = currentDialogue.convo.currentCharacterEmotion;
        }
        catch { 

        }

        
        //showEmotion(emotionParam, label);
        // apply blanket text changes
        

        int charIndex = 0;

        while (state != State.COMPLETED)
        {

            if (currentDialogue.convo.ShouldPlayShake) {
                if (charIndex == currentDialogue.convo.ShakeAtWhatCharIdx) {
                    // Make the screen shake!
                    Debug.Log("Screen shook");
                    // screenshake script reference
                    StartCoroutine(shaker.Shake(currentDialogue.convo.durationShake_, currentDialogue.convo.intensity_));
                }
            }
            if (currentDialogue.convo.ShouldPlayFlash) {
                if (charIndex == currentDialogue.convo.FlashAtWhatCharIdx)
                {
                    // Make the screen flash!   
                    Debug.Log("Screen flashed");
                    flasher.flashAnimator.SetTrigger("Flash");
                }
            }
            if (currentDialogue.convo.ShouldPlaySound) {
                if (charIndex == currentDialogue.convo.SoundAtWhatCharIdx)
                {
                    // Play a sound!   
                    Debug.Log("Sound played");
                }
            }
            if (currentDialogue.convo.ShouldPlayAnimation) {
                if (charIndex == currentDialogue.convo.AnimationAtWhatCharIdx)
                {
                    // Make the object animate!   
                    Debug.Log("Thing Animated");
                    //currentDialogue.convo.animToPlay.Play();
                }
            }


            // add audio clip of text here
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

            if (currentDialogue.convo.IncreaseHusbandHeart || currentDialogue.convo.IncreaseWifeHeart)
            {
                gameManagerRef.modifyHeartPos(currentDialogue.convo.IncreaseHusbandHeart, currentDialogue.convo.IncreaseWifeHeart, currentDialogue.convo.HusbandHeartIncreaseInterval, currentDialogue.convo.WifeHeartIncreaseInterval);
            }

            leftCharSlot.SetActive(currentDialogue.convo.LeftCharOn);
            rightCharSlot.SetActive(currentDialogue.convo.RightCharOn);
            centerCharSlot.SetActive(currentDialogue.convo.CenterCharOn);

            if (currentDialogue.convo.nameOfFlag != null && currentDialogue.convo.nameOfFlag != "") {
                gameManagerRef.raiseFlag(currentDialogue.convo.nameOfFlag);
            }

            QuestioningButton.interactable = currentDialogue.convo.holdButtonState;

            StartCoroutine(typeText(currentDialogue.convo.convoText));
        }

        else
        {
            print("end of dialogue for now");
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
        }
    }
}
