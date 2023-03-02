using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class newConvoManager : MonoBehaviour
{

    public writeText writeTextRef;
    public GameManager gameManagerRef;

    public int currTier;
    public int maxConvoTier = 4;

    private List<TextBox[]> originArr;
    public TextBox[] originArrForTierOne;
    public TextBox[] originArrForTierTwo;
    public TextBox[] originArrForTierThree;

    public TextBox prologueOrigin;
    public TextBox finaleOrigin;
    public TextBox tier3Origin;

    private List<TextBox> listOfRoutesToGetThrough; //Per tier

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

        listOfRoutesToGetThrough = new List<TextBox>();

        originArr = new List<TextBox[]>();

        originArr.Add(originArrForTierOne);
        originArr.Add(originArrForTierTwo);
        originArr.Add(originArrForTierThree);

        currTier = 0;
        writeTextRef.startConvo(prologueOrigin);
        //Comment above line and uncomment below to start at specific tier
        //writeTextRef.startConvo(originArr[currTier][0]);


        //writeTextRef.startConvo(shuffleConvos(currTier));

    }

    // Update is called once per frame
    void Update()
    {
        //currDialogue = writeTextRef.currentDialogue;
    }

    public List<TextBox> shuffleConvos(int Tier) {
        int lengthOfOriginArr = originArr[Tier-1].Length;
        //int lengthOfOriginArr = 0; // ----------------------------This is debug, replace with line above for final product

        int howManyRoutesToPull = 0;

        if (Tier == 1)
        {
            howManyRoutesToPull = 3;
        }
        else if (Tier == 2)
        {
            howManyRoutesToPull = 2;
        }
        else if (Tier == 3) { 
            howManyRoutesToPull = 1;
        }

        List<TextBox> ListOfChosenRoutes = new List<TextBox>();

        int chosenText;

        List<TextBox> ListOfPossibleRoutes = new List<TextBox>();

        Debug.Log(Tier - 1);
        for (int i = 0; i < originArr[Tier - 1].Length; i++) { 

            ListOfPossibleRoutes.Add(originArr[Tier - 1][i]);
            Debug.Log("Current Tier: " + Tier + " and idx: " + i);
        }
        int ListOfPossibleRoutesSize = originArr[Tier - 1].Length;

        for (int i = 0; i < howManyRoutesToPull; i++) {

            chosenText = Random.Range(0, ListOfPossibleRoutesSize);
            ListOfChosenRoutes.Add(ListOfPossibleRoutes[chosenText]);

            ListOfPossibleRoutes.Remove(ListOfPossibleRoutes[chosenText]);
            ListOfPossibleRoutesSize--;
        }

        foreach (TextBox origin in ListOfChosenRoutes) {
            Debug.Log(origin.convo.convoText);
        }

        //int chosenText = Random.Range(0, lengthOfOriginArr);
        //Debug.Log("The chosen origin was Tier " + (Tier) + " with the text: " + originArr[Tier-1][chosenText].convo.convoText);
        return ListOfChosenRoutes;
    }

    public void ConvoCompleteCalc(TextBox currDialogue, bool usedButton, int choiceButtonPressed)
    {
        //Here is where we perform any end-of-conversation calculations/figuring out where to go from here.
        int numOfConvosFromCurrent = currDialogue.convo.possibleNextTexts.Length;

        if (numOfConvosFromCurrent == 0) {


            if (listOfRoutesToGetThrough.Count != 0)
            {
                Debug.Log("1 listOfRoutes Count " + listOfRoutesToGetThrough.Count);

                listOfRoutesToGetThrough.RemoveAt(0);

                if (listOfRoutesToGetThrough.Count == 0)
                {
                    if (currTier < (maxConvoTier)) {
                        currTier++;
                        Debug.Log("Called shuffle with Tier " + currTier);
                        listOfRoutesToGetThrough = shuffleConvos(currTier);
                        Debug.Log("2 listOfRoutes Count " + listOfRoutesToGetThrough.Count);
                        writeTextRef.startConvo(listOfRoutesToGetThrough[0]);
                        return;
                    }
                    
                }
                else {
                    writeTextRef.startConvo(listOfRoutesToGetThrough[0]);
                }


                Debug.Log("Here make sure you go to finale tier");
                

                return;
            }
            else if (currTier < (maxConvoTier))
            {
                currTier++;
                Debug.Log("Called shuffle with Tier " + currTier);
                listOfRoutesToGetThrough = shuffleConvos(currTier);
                Debug.Log("3 listOfRoutes Count " + listOfRoutesToGetThrough.Count);
                writeTextRef.startConvo(listOfRoutesToGetThrough[0]);
                return;
            }


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

    public void StartBadSequence() {

        SceneManager.LoadScene("BadEnding");
        //currTier = 4;

        //if (endingIndex < 0)
        //{
        //    //Got best ending, hearts met
        //    writeTextRef.startConvo(finaleOrigin);
        //}
        //else if (endingIndex == 1)
        //{
        //    //Got husband ending
        //    writeTextRef.startConvo(finaleOrigin);
        //}
        //else if (endingIndex == 2)
        //{
        //    //Got wife ending
        //    writeTextRef.startConvo(finaleOrigin);
        //}
        //else {
        //    //Got generic divorce ending
        //    writeTextRef.startConvo(finaleOrigin);
        //}

    }
}
