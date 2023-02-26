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
            case ("testflag"):
                notebookDesc = "This is a test flag";
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
