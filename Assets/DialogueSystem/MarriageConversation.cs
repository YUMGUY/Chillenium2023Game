using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMarriageConvo", menuName = "Dialogue_Data/New Marriage Convo")]
public class MarriageConversation : ScriptableObject
{
    [Header("scene's convo")]
    public List<Conversation> conversations;


    [System.Serializable]
    public struct Conversation
    {
        [TextArea(2, 10)]
        public string convoText;
        public Sprite currentCharacterEmotion;
        public SpeakerInfo character;
        public ChoiceEvent choiceEvent;

        [Header("Questioning System Var")]
        public int pickUpPoint;
        public Action currentAction;

        [SerializeField]
        public enum Action
        {
            IDLE, SHOW, DISAPPEAR
        }
    }
}
