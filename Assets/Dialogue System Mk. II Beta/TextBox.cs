using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogue", menuName = "Dialogue_Data/New Dialogue Field")]
public class TextBox : ScriptableObject
{
    [Header("scene's convo")]
    public Conversation convo;

    [System.Serializable]
    public struct Conversation
    {
        [TextArea(2, 10)]
        public string convoText;
        public Sprite currentCharacterEmotion;
        public SpeakerInfo character;

        [Header("List all possible conversation branches from this text")]
        public TextBox[] possibleNextTexts;

        [Header("Heart Meter Effects")]
        public bool IncreaseHusbandHeart;
        public bool IncreaseWifeHeart;
        public float HusbandHeartIncreaseInterval;
        public float WifeHeartIncreaseInterval;

        [Header("Enable or Disable Characters")]
        public bool LeftCharOn;
        public bool RightCharOn;
        public bool CenterCharOn;

        [Header("Screen Shake")]
        public bool ShouldPlayShake;
        public int ShakeAtWhatCharIdx;

        [Header("Screen Flash")]
        public bool ShouldPlayFlash;
        public int FlashAtWhatCharIdx;

        [Header("Sound Effect")]
        public bool ShouldPlaySound;
        public int SoundAtWhatCharIdx;
        public AudioClip soundToPlay;

        [Header("Dialogue Anim")]
        public bool ShouldPlayAnimation;
        public int AnimationAtWhatCharIdx;
        public Animation animToPlay;
        public Animator whichObjToAnim;


        [SerializeField]
        public enum Action
        {
            IDLE, SHOW, DISAPPEAR
        }
    }


}
