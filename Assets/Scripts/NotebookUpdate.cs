using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NotebookUpdate : MonoBehaviour
{

    public GameManager gameManagerRef;
    private int amountOfSlotsUsed = 0;
    private int currPage = 1;
    private int slotsPerPage = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateNotebook(string flagName) {

        string notebookDesc = "";
        bool shouldAddToNotebook = true;

        switch (flagName) {
            case ("default"):
                shouldAddToNotebook = false;
                break;

            case ("testflag"):
                notebookDesc = "This is a test flag";
                break;
            case ("motherStoodUpForHim"):
                notebookDesc = "Mama Blu thinks her son just needs to talk it out with her.";
                break ;
            case ("husbandPlanningItaly"):
                notebookDesc = "He is planning for their trip to Italy.";
                break;
            case ("husbandPayingOffWedding"):
                notebookDesc = "He is still working to pay off their wedding expenses.";
                break;
            case ("wifeSecretBank"):
                notebookDesc = "She has a secret bank account and is saving for their Italy trip.";
                break;
            case ("wifeOolanderFeelings"):
                notebookDesc = "Oolander seems to think she's secretly into him.";
                break;
            case ("wifeFashionOrigins"):
                notebookDesc = "She became a designer because of Italian fashion";
                break;
            case ("wifeWantsItaly"):
                notebookDesc = "She wants to go on a trip to Italy";
                break;

            


            default:
                shouldAddToNotebook = false;
                break;
         
        }

        if (shouldAddToNotebook) {

            getNextAvailableSlot().GetComponent<TextMeshProUGUI>().text = notebookDesc;

            amountOfSlotsUsed++;
            if (amountOfSlotsUsed >= slotsPerPage) {
                currPage++;
                amountOfSlotsUsed = 0;
            }
        }

    }

    private GameObject getNextAvailableSlot() {
        GameObject slot;


        string pageNum = "Page " + currPage;
        Debug.Log(pageNum);
        GameObject pageRef = transform.GetChild(0).Find(pageNum).gameObject;
        string slotNum = "Slot " + (amountOfSlotsUsed + 1);
        Debug.Log(slotNum);
        slot = pageRef.transform.Find(slotNum).gameObject;

        return slot;
    }
}
