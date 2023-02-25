using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartBar : MonoBehaviour
{

    public GameManager gameManagerRef;

    public Transform HusbandStart;
    public Transform HusbandEnd;
    public Transform WifeStart;
    public Transform WifeEnd;

    public GameObject LeftHeart;
    public GameObject RightHeart;

    private float HusBarDistance;
    private float WifeBarDistance;
    private float initialLeftPos;
    private float initialRightPos;

    [SerializeField]
    [Range(0.0f, 1.0f)]
    private float HusBarPercent = 0.0f;

    [SerializeField]
    [Range(0.0f, 1.0f)]
    private float WifeBarPercent = 0.0f;

    

    // Start is called before the first frame update
    void Start()
    {
        HusBarDistance = Mathf.Abs(HusbandEnd.position.x - HusbandStart.position.x);
        WifeBarDistance = Mathf.Abs(WifeStart.position.x - WifeEnd.position.x);

        initialLeftPos = LeftHeart.transform.position.x;
        initialRightPos = RightHeart.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        LeftHeart.transform.position = new Vector3(initialLeftPos + HusBarPercent * HusBarDistance, LeftHeart.transform.position.y, LeftHeart.transform.position.z);
        RightHeart.transform.position = new Vector3(initialRightPos - WifeBarPercent * WifeBarDistance, RightHeart.transform.position.y, RightHeart.transform.position.z);

        if (HusBarPercent == 1.0f && WifeBarPercent == 1.0f) {
            gameManagerRef.winCondition();
        }
    }

    public void incrementHeart(bool HusIncrease, bool WifeIncrease, float increaseFactor)
    {
        Debug.Log("Someone's heart level increased!");
        if (HusIncrease) {
            if (HusBarPercent + increaseFactor > 1)
            {
                HusBarPercent = 1.0f;
            }
            else {
                HusBarPercent += increaseFactor;
            }
            
        }
        if (WifeIncrease) {
            if (WifeBarPercent + increaseFactor > 1)
            {
                WifeBarPercent = 1.0f;
            }
            else {
                WifeBarPercent += increaseFactor;
            }
            
        }
    }

    public void decrementHeart(bool HusDecrease, bool WifeDecrease, float decreaseFactor)
    {
        Debug.Log("Someone's heart level decreased...");
        if (HusDecrease)
        {
            if (HusBarPercent + decreaseFactor < 0)
            {
                HusBarPercent = 0.0f;
            }
            else {
                HusBarPercent += decreaseFactor;
            }
            
        }
        if (WifeDecrease)
        {
            if (WifeBarPercent + decreaseFactor < 0)
            {
                WifeBarPercent = 0.0f;
            }
            else {
                WifeBarPercent += decreaseFactor;
            }
            
        }
    }
}
