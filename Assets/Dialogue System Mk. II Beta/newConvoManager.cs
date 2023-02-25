using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newConvoManager : MonoBehaviour
{

    public writeText writeTextRef;

    private int currTier;
    public int maxConvoTier = 3;

    private List<TextBox[]> originArr;
    public TextBox[] originArrForTierOne;
    public TextBox[] originArrForTierTwo;
    public TextBox[] originArrForTierThree;

    //private TextBox currDialogue;

    // Start is called before the first frame update
    void Start()
    {
        /*
        Debug.Log(originArrForTierOne.Length);
        Debug.Log(originArrForTierOne[0].convo.convoText);
        if (originArrForTierOne == null) {
            Debug.Log("uh oh");
        }*/
        originArr = new List<TextBox[]>();

        originArr.Add(originArrForTierOne);
        originArr.Add(originArrForTierTwo);
        originArr.Add(originArrForTierThree);

        currTier = 0;
        writeTextRef.startConvo(shuffleConvos(currTier));
    }

    // Update is called once per frame
    void Update()
    {
        //currDialogue = writeTextRef.currentDialogue;
    }

    public TextBox shuffleConvos(int Tier) {
        //int lengthOfOriginArr = originArr[Tier].Length;
        int lengthOfOriginArr = 0; // ----------------------------This is debug, replace with line above for final product
        int chosenText = Random.Range(0, lengthOfOriginArr);
        Debug.Log("The chosen origin was Tier " + (Tier+1) + " with the text: " + originArr[Tier][chosenText].convo.convoText);
        return originArr[Tier][chosenText];
    }

    public void ConvoCompleteCalc(TextBox currDialogue)
    {
        //Here is where we perform any end-of-conversation calculations/figuring out where to go from here.
        int numOfConvosFromCurrent = currDialogue.convo.possibleNextTexts.Length;

        if (currTier < (maxConvoTier-1) && numOfConvosFromCurrent == 0) {
            currTier++;
            Debug.Log("Called shuffle with Tier " + currTier);
            writeTextRef.startConvo(shuffleConvos(currTier));
            return;
        }


        //currentDialogue.convo.PossibleNextTexts


        TextBox newDialogue = null;

        if (numOfConvosFromCurrent != 0)
        {
            int tempDecidingNumber = 0;

            newDialogue = currDialogue.convo.possibleNextTexts[tempDecidingNumber];

        }
        writeTextRef.NextLine(newDialogue);

    }
}
