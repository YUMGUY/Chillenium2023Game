using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public HeartBar heartBarRef;

    public NotebookUpdate notebookRef;

    public newConvoManager conversationManager;

    public Hashtable flagManager;
    public string[] flagsList;

    private float husbandScore;
    private float wifeScore;

    // Start is called before the first frame update
    void Start()
    {
        flagManager = new Hashtable();

        initializeHash(flagManager);

    }

    // Update is called once per frame
    void Update()
    {
        husbandScore = heartBarRef.HusBarPercent;
        wifeScore = heartBarRef.WifeBarPercent;
    }

    public void initializeHash(Hashtable Hash) {
        Hash.Clear();
        for (int i = 0; i < flagsList.Length; i++) {
            Debug.Log(flagsList[i]);
            Hash.Add(flagsList[i], false); //Creates hash with all flag names and initializes them all to 0.
        }
    }

    public void raiseFlag(string nameOfFlag) {
        notebookRef.updateNotebook(nameOfFlag);

        flagManager[nameOfFlag] = true; //Potential bug to fix, if getting Null Ref here, it's because you're raising a flag at an origin

        Debug.Log(nameOfFlag);
        Debug.Log(flagManager[nameOfFlag]);
        
        
    }

    public void modifyHeartPos(bool changeHus, bool changeWife, float husFactor, float wifeFactor) {

        //Debug.Log("This dialogue affects heart level");

        if (husFactor > 0)
        {
            heartBarRef.incrementHeart(true, false, husFactor);
        }
        else if (husFactor < 0) {
            heartBarRef.decrementHeart(true, false, husFactor);
        }

        if (wifeFactor > 0)
        {
            heartBarRef.incrementHeart(false, true, wifeFactor);
        }
        else if (husFactor < 0)
        {
            heartBarRef.decrementHeart(false, true, wifeFactor);
        }
    }

    public void gameEnd() {
        Debug.Log("The game is over");

        int ending = 0;

        if (husbandScore > 0.7 && wifeScore < 0.35) {
            ending = 1;
        }
        else if (husbandScore < 0.35 && wifeScore > 0.7) {
            ending = 2;
        }
        else {
            ending = 0;
        }

        conversationManager.startEndSequence(ending);

        Debug.Log("You're ending is: " + ending);
    }

    public void winCondition() {
        Debug.Log("You Win!");

        //conversationManager.startEndSequence(-1);

    }
}
