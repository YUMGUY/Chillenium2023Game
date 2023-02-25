using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceInfo : MonoBehaviour
{
    // Start is called before the first frame update
    public ConversationManager ReferencedManager;
    public int pathNumberChoice;
    private void Awake()
    {
        ReferencedManager = GameObject.Find("Dialogue Manager Canvas").GetComponent<ConversationManager>();
    }
    void Start()
    {
        
    }
    
    public void DeterminePath()
    {
        ReferencedManager.currentChoiceChosen = this.gameObject;
        ReferencedManager.currentPathNumber = pathNumberChoice;
        print("path chosen: " + pathNumberChoice);
    }



}
