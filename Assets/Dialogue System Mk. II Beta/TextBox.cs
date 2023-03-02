using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogue", menuName = "Dialogue_Data/New Dialogue Field")]
public class TextBox : ScriptableObject
{
    [Header("Conversation Attributes")]
    public Conversation convo;

    [System.Serializable]
    public struct Conversation
    {

        [TextArea(2, 10)]
        public string convoText;

        [Header("-Speaker-")]
        public SpeakerInfo character;
        

        [Header("-Text effects-")]
        public bool autoAdvance;
        public bool ShouldPlayTextSound;
        //public AudioClip textSound;         // This should be combined with "Character"
        public float typingSpeed;


        [Header("-Custom text effects-")]
        public Color textColor_;
        public int inputFontSize;
        public bool isBold;
        public bool isWavy;
        public bool isWiggling;

        [Header("-Sound effect-")]
        public bool ShouldPlaySound;
        public AudioClip soundToPlay;
        public int SoundAtWhatCharIdx;

        [Header("-Screen shake-")]
        public bool ShouldPlayShake;
        public float intensity_;
        public int ShakeAtWhatCharIdx;
        public float durationShake_;

        [Header("-Screen flash-")]
        public bool ShouldPlayFlash;
        public int FlashAtWhatCharIdx;

        [Header("-Elaborate button-")]
        public bool holdButtonState;

        [Header("-Choices-")]
        public bool ShouldLeadToChoice;
        public string[] buttonText;

        [Header("-Heart meter effects-")]
        public bool IncreaseHusbandHeart;
        public bool IncreaseWifeHeart;
        public float HusbandHeartIncreaseInterval;
        public float WifeHeartIncreaseInterval;

        [Header("-Raise a flag at this dialogue?-")]
        public string nameOfFlag;

        [Header("-List all possible conversation branches from this text-")]
        public TextBox[] possibleNextTexts;

        [Header("-For each above possible convo, list a condition for them to be taken. Index 0 is the default-")]
        public string[] NextTaskConditions;

        [Header("-Character emotions-")]
        public Sprite[] currentCharacterEmotions;

        [Header("-Enable or Disable Characters-")]
        public bool LeftCharOn;
        public bool RightCharOn;
        public bool CenterCharOn;

        [Header("-Dialogue Anim-")]
        public bool ShouldPlayAnimation;
        public int AnimationAtWhatCharIdx;
        public string[] animationName;

        [Header("-BGM Effect-")]
        public bool ShouldUpdateBGM;
        public int musicAtWhatCharIdx;
        public AudioClip musicToPlay;


        [SerializeField]
        public enum Action
        {
            IDLE, SHOW, DISAPPEAR
        }
    }


}
