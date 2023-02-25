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
    public int convoIndex = 0;
    private State state = State.COMPLETED;

    [Header("Choice System")]
    public int currentPathNumber = 0;  // to keep track of what path number I took
    public int previousPathNumber = 0;
    public ChoiceEvent currentChoiceEvent;
    public GameObject currentChoiceChosen;
    public GameObject choiceDisplay;


    [Header("People and Textbox")]
    public GameObject dialogueItems;
    public GameObject character1;
    public GameObject character2;
    public bool convoStarted;

    [Header("Interrogation")]
    public QuestioningSystem questioningSystem;
    public bool questioningCurrently;

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

        if(currentPathNumber >= potentialPaths.Count)
        {
            print("path scene number exceeds number of paths in game");
            return;
        }
        currentConvo = potentialPaths[currentPathNumber];

        StartCoroutine(typeText(currentConvo.conversations[convoIndex].convoText));
    }

    public IEnumerator typeText(string text)
    {
        namePanel.text = currentConvo.conversations[convoIndex].character.name;
        namePanel.color = currentConvo.conversations[convoIndex].character.nameColor;
        textPanel.text = "";

        state = State.TALKING;

        int label = currentConvo.conversations[convoIndex].character.characterLabel;
        Sprite emotionParam = currentConvo.conversations[convoIndex].currentCharacterEmotion;
        showEmotion(emotionParam, label);

        // choice system takes place
        currentChoiceEvent = currentConvo.conversations[convoIndex].choiceEvent;
        

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
        if (currentChoiceEvent != null)
        {
            print("Currently making a choice");
            StartCoroutine(MakeChoice());
        }

        yield return null;
    }


    public void NextLine() // controls the value of convoIndex, which is the marker for which speaker is speaking
    {
        // check flag
        if(questioningCurrently == true)
        {
            print("Currently questioning");
            return;
        }

        if(currentChoiceEvent != null)
        {
            print("You haven't made a choice yet bitch");
            return;
        }
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

        currentChoiceChosen = null;
        
        float number = 75f;
        if (currentChoiceEvent.numberOfChoices == 1)
        {
            number = 0;
        }

        for (int i = 0; i < currentChoiceEvent.numberOfChoices; ++i)
        {
            GameObject prefabC = Instantiate(currentChoiceEvent.choiceResource, choiceDisplay.transform);
            prefabC.transform.GetComponentInChildren<TextMeshProUGUI>().text = currentChoiceEvent.possibleChoices[i].choiceText;
            prefabC.transform.localPosition = new Vector3(0, number);
            prefabC.GetComponent<ChoiceInfo>().pathNumberChoice = currentChoiceEvent.possibleChoices[i].choicePath; // change later
            number -= 150;
        }

        while (currentChoiceChosen == null)
        {
            yield return null;
            if (currentChoiceChosen != null)
            {
                Transform cDisplay = GameObject.Find("Choice Event Display").transform;
                foreach (Transform choice in cDisplay)
                {
                    choice.gameObject.SetActive(false);
                }
                // then move onto next conversation or change the currentConvo;
                yield return new WaitForSeconds(.5f); // temporary, maybe just add another line of dialogue
                convoIndex++;
                textPanel.text = string.Empty;
                currentConvo = potentialPaths[currentPathNumber];
                // if path number != prev path number do current = 0, else dont do anything
                if(currentPathNumber != previousPathNumber)
                {
                    convoIndex = 0;
                }
                currentChoiceEvent = null;
                StartCoroutine(typeText(currentConvo.conversations[convoIndex].convoText));
                
                break;
            }
        }
        
        print("you finished choosing");
        yield return null;
    }
}
