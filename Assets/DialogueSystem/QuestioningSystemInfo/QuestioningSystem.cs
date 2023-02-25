using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestioningSystem : MonoBehaviour
{
    // Start is called before the first frame update
    public ConversationManager refConvo;
    public MarriageConversation questioningConversation;
    private State state = State.COMPLETED;

    public float typingSpeed;
    public TextMeshProUGUI textPanel;
    public TextMeshProUGUI namePanel;

    [Header("Dialogue Indexes")]
    public int questionDialogueIndex;
    public int storedConvoIndex;
    private enum State
    {
        TALKING, COMPLETED
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && refConvo.questioningCurrently == true)
        {
            if (state == State.COMPLETED) // if the line is completed
            {
                NextQuestionLine();
            }

            else // in the midst of typing
            {
                StopAllCoroutines();
                textPanel.text = questioningConversation.conversations[questionDialogueIndex].convoText;
                state = State.COMPLETED;

            }
        }
    }


    public void QuestionCharacter()
    {
        // add flag raised later on

        // replace text with either default failure text or successive text
        // set ci to 0
        storedConvoIndex = refConvo.convoIndex;
        refConvo.convoIndex = 0;
        refConvo.questioningCurrently = true;
        StartCoroutine(TypeQuestioning(questioningConversation.conversations[questionDialogueIndex].convoText));
        // then if flag not raised, default failure text coroutine, go back to next line, so stored ci is called, stored ci is placed here
        // if flag raised, move on in coroutine

    }


    public IEnumerator TypeQuestioning(string text)
    {


        namePanel.text = questioningConversation.conversations[questionDialogueIndex].character.name;
        namePanel.color = questioningConversation.conversations[questionDialogueIndex].character.nameColor;
        textPanel.text = "";

        state = State.TALKING;

        //int label = currentConvo.conversations[convoIndex].character.characterLabel;
        //Sprite emotionParam = currentConvo.conversations[convoIndex].currentCharacterEmotion;
        //showEmotion(emotionParam, label);

        // choice system takes place
       

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


    public void NextQuestionLine() // controls the value of convoIndex, which is the marker for which speaker is speaking
    {
        // check flag

  
        // depends on line 79
        if (questionDialogueIndex < questioningConversation.conversations.Count - 1)
        {
            questionDialogueIndex++;
            textPanel.text = string.Empty;
            StartCoroutine(TypeQuestioning(questioningConversation.conversations[questionDialogueIndex].convoText));
        }

        else
        {
            print("end of questioning dialogue for now");
            print(questionDialogueIndex);
            refConvo.convoIndex = questioningConversation.conversations[questionDialogueIndex].pickUpPoint;
            print(refConvo.convoIndex);
            refConvo.questioningCurrently = false;
            StartCoroutine(refConvo.typeText(refConvo.currentConvo.conversations[refConvo.convoIndex].convoText));
        }
    }
}
