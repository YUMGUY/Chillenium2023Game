using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newConvoManager : MonoBehaviour
{

    public writeText writeTextRef;
    public GameManager gameManagerRef;

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

    public void ConvoCompleteCalc(TextBox currDialogue, bool usedButton, int choiceButtonPressed)
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

            // This part chooses the next text box to display based on the Possible Next Texts array
            // Please keep the following standard in mind when assigning elements to that array:
            //Element 0 is default, keep this for the linear dialogue path
            //Element 1 is for you don't meet the requirements for the text box in Element 2
            //Element 2 is for the tangent text box that you could go to if you press the hold up button
            //Element 3 and beyond is mapped to choices 1 2 and 3 in that order

            int NextConvoIndex = 0; // Default case

            if (usedButton) {

                string flagRequirement = currDialogue.convo.possibleNextTexts[2].convo.NextTaskConditions[2];
                bool flagValue = (bool)gameManagerRef.flagManager[flagRequirement];
                if (flagValue)
                {
                    NextConvoIndex = 2;
                }
                else {
                    NextConvoIndex = 1;
                }
            }
            if (choiceButtonPressed > -1) {
                NextConvoIndex = 2 + choiceButtonPressed;
            }


            newDialogue = currDialogue.convo.possibleNextTexts[NextConvoIndex];

        }


        writeTextRef.NextLine(newDialogue);

    }
}
