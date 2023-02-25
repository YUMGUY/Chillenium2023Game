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

        public float typingSpeed;

        [Header("List all possible conversation branches from this text")]
        public TextBox[] possibleNextTexts;

        [Header("For each above possible convo, list a condition for them to be taken. Index 0 is the default")]
        public string[] NextTaskConditions;

        [Header("Does the text move to the next box automatically?")]
        public bool autoAdvance;

        [Header("Is the Hold Up Button Enabled?")]
        public bool holdButtonState;

        [Header("Heart Meter Effects")]
        public bool IncreaseHusbandHeart;
        public bool IncreaseWifeHeart;
        public float HusbandHeartIncreaseInterval;
        public float WifeHeartIncreaseInterval;

        [Header("Raise a flag at this dialogue?")]
        public string nameOfFlag;

        [Header("Enable or Disable Characters")]
        public bool LeftCharOn;
        public bool RightCharOn;
        public bool CenterCharOn;

        [Header("Screen Shake")]
        public bool ShouldPlayShake;
        public int ShakeAtWhatCharIdx;
        public float durationShake_;
        public float intensity_;

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

        [Header("Custom Text Effects")]
        public Color textColor_;
        public float inputFontSize;
        public bool isBold;
        public bool isWavy;
        public bool isWiggling;
        [SerializeField]
        public enum Action
        {
            IDLE, SHOW, DISAPPEAR
        }
    }


}
