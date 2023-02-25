using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ConversationManager : MonoBehaviour
{
    public float typingSpeed;
    public TextMeshProUGUI textPanel;
    public TextMeshProUGUI namePanel;

    [Header("Conversation Scene Info")]
    public List<MarriageConversation> potentialPaths;
    public MarriageConversation currentConvo;
    public int TouhouConversationIndex;
    private int convoIndex = 0;
    private State state = State.COMPLETED;
    public int pathSceneNumber = 0;  // to keep track of what path number I took


    [Header("People and Textbox")]
    public GameObject dialogueItems;
    public GameObject character1;
    public GameObject character2;
    public bool convoStarted;


    //[Header("Controls Characters")]
    // public CharacterController characterController;
    private enum State
    {
        TALKING, COMPLETED
    }

    // Start is called before the first frame update
    void Start()
    {
        startConvo();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Z) && convoStarted == true)
        {
            if (state == State.COMPLETED) // if the line is completed
            {
                NextLine();
            }

            else // in the midst of typing
            {
                StopAllCoroutines();
                textPanel.text = currentConvo.conversations[convoIndex].convoText;
                state = State.COMPLETED;

            }
        }
    }


    public void startConvo()
    {
        convoStarted = true;
        convoIndex = 0;

        // enable ui 
        dialogueItems.SetActive(true); // polish later on by adding text box animations for beginning/end
        character1.SetActive(true);
        // character2.SetActive(true);

        if(pathSceneNumber >= potentialPaths.Count)
        {
            print("path scene number exceeds number of paths in game");
            return;
        }
        currentConvo = potentialPaths[pathSceneNumber];

        StartCoroutine(typeText(currentConvo.conversations[convoIndex].convoText));
    }

    private IEnumerator typeText(string text)
    {
        namePanel.text = currentConvo.conversations[convoIndex].character.name;
        namePanel.color = currentConvo.conversations[convoIndex].character.nameColor;
        textPanel.text = "";

        state = State.TALKING;

        int label = currentConvo.conversations[convoIndex].character.characterLabel;
        Sprite emotionParam = currentConvo.conversations[convoIndex].currentCharacterEmotion;
        showEmotion(emotionParam, label);

        int charIndex = 0;

        while (state != State.COMPLETED)
        {
            // add audio clip of text here
            textPanel.text += text[charIndex];
            yield return new WaitForSeconds(typingSpeed);

            if (++charIndex == text.Length)
            {
                // print("finished");
                state = State.COMPLETED;
                break;
            }
        }

        yield return null;
    }


    public void NextLine() // controls the value of convoIndex, which is the marker for which speaker is speaking
    {

        // depends on line 79
        if (convoIndex < currentConvo.conversations.Count - 1)
        {
            convoIndex++;
            textPanel.text = string.Empty;
            StartCoroutine(typeText(currentConvo.conversations[convoIndex].convoText));
        }

        else
        {
            print("end of dialogue for now");
            // temporary disappearance of dialogue box and characters
            if (dialogueItems.activeInHierarchy)
            {
                dialogueItems.SetActive(false);
                character1.SetActive(false);
                //character2.SetActive(false);
            }

            convoStarted = false;
        }
    }


    // in future use in separate script
    public void showEmotion(Sprite spriteEmotion, int speakerNumber) // 1 or 2
    {
        if (spriteEmotion == null)
        {
            print("no emotion animation will be played");
            return;
        }

        if (speakerNumber == 1)
        {
            character1.GetComponent<Image>().sprite = spriteEmotion;
        }
        //else if (speakerNumber == 2)
        //{
        //    character2.GetComponent<Image>().sprite = spriteEmotion;
        //}

        else
        {
            print("no emotion sprite will be displayed for now");
        }
    }


    public IEnumerator MakeChoice()
    {


        yield return null;
    }
}
