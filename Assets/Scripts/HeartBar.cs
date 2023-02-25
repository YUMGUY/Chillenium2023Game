using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartBar : MonoBehaviour
{

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
    }
}
