using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Speaker", menuName = "Dialogue_Data/New Speaker")]
public class SpeakerInfo : ScriptableObject
{
    public string speakerName;
    public Color nameColor;
    public int characterLabel;
    //potential text color associated with character
}
