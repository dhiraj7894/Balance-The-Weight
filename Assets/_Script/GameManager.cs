using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gManager;

    public leftRope LRope;
    public rightRope RRope;

    public GameObject Wheel;
    public GameObject Confetti;
    public Sprite Smile, scared;

    public bool IsBalanced = false;
    public bool isGameFinished = false;
    public float wheelSpeed;
    private float differenceBetweenWight;

    void Start()
    {
        gManager = this;
        Application.targetFrameRate = 60;       
    }

    void Update()
    {
        if(!LRope.isPlayerAttachedToCroco || !RRope.isPlayerAttachedToCroco )
            weightBalanceChecker();
        crocoExit();
        if (isGameFinished)
        {
            Confetti.SetActive(true);
        }
    }

    float angle;
    public void wheelRotator(float sign)
    {
        if (!isGameFinished)
        {
            angle += wheelSpeed * sign * Time.deltaTime;
            Wheel.transform.localRotation = Quaternion.Euler(0, 0, angle);
        }
    }
    public void crocoExit()
    {
        if(LRope.isPlayerAttachedToCroco || RRope.isPlayerAttachedToCroco)
        {
           LRope.leftCroco.GetComponent<Animator>().SetTrigger("out");
           RRope.rightCroco.GetComponent<Animator>().SetTrigger("out");
        }
    }

    public void weightBalanceChecker()
    {
        differenceBetweenWight = Mathf.Abs(LRope._weight - RRope._weight);

        if(differenceBetweenWight > 0 && differenceBetweenWight <= 2)
        {
            LRope._speed = 0.1f;
            RRope._speed = 0.1f;
            wheelSpeed = 20;
        }
        if (differenceBetweenWight >= 3 && differenceBetweenWight <= 6)
        {
            LRope._speed = 0.3f;
            RRope._speed = 0.3f;
            wheelSpeed = 30;
        }
        if (differenceBetweenWight >= 7 && differenceBetweenWight <= 13)
        {
            LRope._speed = 0.5f;
            RRope._speed = 0.5f;
            wheelSpeed = 50;
        }
        if (differenceBetweenWight >= 14)
        {
            LRope._speed = 1f;
            RRope._speed = 1f;
        }
        if (differenceBetweenWight == 0)
        {
            IsBalanced = true;
            wheelSpeed = 0;
        }
    }
}
