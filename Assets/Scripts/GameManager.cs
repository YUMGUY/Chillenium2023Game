using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public HeartBar heartBarRef;

    public Hashtable flagManager;
    public string[] flagsList;

    // Start is called before the first frame update
    void Start()
    {
        flagManager = new Hashtable();

        initializeHash(flagManager);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void initializeHash(Hashtable Hash) {
        Hash.Clear();
        for (int i = 0; i < flagsList.Length; i++) {
            Debug.Log(flagsList[i]);
            Hash.Add(flagsList[i], false); //Creates hash with all flag names and initializes them all to 0.
        }
    }

    public void raiseFlag(string nameOfFlag) {
        flagManager[nameOfFlag] = true;
    }

    public void modifyHeartPos(bool changeHus, bool changeWife, float husFactor, float wifeFactor) {

        Debug.Log("This dialogue affects heart level");

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

    public void winCondition() {
        Debug.Log("You Win!");
    }
}
