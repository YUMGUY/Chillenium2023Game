using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Choice", menuName = "Dialogue_Data/New Choice Event")]
public class ChoiceEvent : ScriptableObject
{
    public int numberOfChoices;
    public GameObject choiceResource;
    public List<Choice> possibleChoices;
    

    [System.Serializable]
    public struct Choice
    {
        public string choiceText;
        public int choicePath;
    }
}
