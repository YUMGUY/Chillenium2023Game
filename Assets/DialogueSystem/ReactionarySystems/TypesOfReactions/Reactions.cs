using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Reaction", menuName = "Dialogue_Data/New Reaction")]
public class Reactions : ScriptableObject
{
    public string type_;
    public float magnitude_;
    public float duration_;
    public bool atBeginning;
    public bool atEnd;
    public bool atMiddle;
}
