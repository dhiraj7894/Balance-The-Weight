using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rightRope : MonoBehaviour
{
    //Wheel Rotator is added here 
    public static rightRope rightR;

    public float _speed = 1;
    public float _weight;
    public float _LeftWeight;
    public float _crocoToCharecter;

    public bool isMovingUp = false;
    public bool isPlayerAttachedToCroco = false;

    public GameObject charecter;

    public Transform rightCroco;
    public Transform weightCollector;

    public List<Transform> weights = new List<Transform>();
    public List<Animator> animation = new List<Animator>();

    public SpriteRenderer rightRender;

    private Vector3 initPos;
    void Start()
    {
        rightR = this;
        initPos = transform.position;
    }


    void Update()
    {
        _LeftWeight = leftRope.leftR._weight;

        if (!GameManager.gManager.IsBalanced)
        {
            ropeMovement();
            weightAdder();
            weightChecker();
            crocoChecker();
            GameOver();
        }
        weightBalanced();
    }

    void GameOver()
    {
        if (isPlayerAttachedToCroco)
        {
            _speed = 0;
            transform.Translate(Vector3.down * 3 * Time.deltaTime);
            
        }  
        if (leftRope.leftR.isPlayerAttachedToCroco)
        {
            _speed = 0;
            if (transform.position.y <= 3.7f)
            {
                transform.Translate(Vector3.up * 5 * Time.deltaTime);
                GameManager.gManager.wheelSpeed = 600;
            }
            if (transform.position.y >= 3.7f)
            {
                transform.position = new Vector3(-1, 3.7f, transform.position.z);
                charecter.GetComponent<Rigidbody>().useGravity = true;
                charecter.GetComponent<Rigidbody>().isKinematic = false;
                rightRender.sprite = GameManager.gManager.scared;
            }
        }
    }

    void weightBalanced()
    {
        
        if (GameManager.gManager.IsBalanced)
        {
            if (transform.position.y == initPos.y)
            {
                GameManager.gManager.isGameFinished = true;
            }
            animation[0].SetBool("Hang", false);
            animation[1].SetBool("Hang", false);
            transform.position = Vector3.MoveTowards(transform.position, initPos, 2 * Time.deltaTime);
            GameManager.gManager.wheelSpeed = 50;
            rightRender.sprite = GameManager.gManager.Smile;
            if (isMovingUp)
            {
                GameManager.gManager.wheelRotator(1);
            }
            else if (!isMovingUp)
            {
                GameManager.gManager.wheelRotator(-1);
            }
        }
    }

   void crocoChecker()
    {
        _crocoToCharecter = Vector3.Distance(transform.position, rightCroco.position);
        if (_crocoToCharecter <= 4 && !isPlayerAttachedToCroco)
        {
            animation[0].SetBool("Hang", true);
            animation[1].SetBool("Hang", true);
        }
        if (_crocoToCharecter >= 4 && !isPlayerAttachedToCroco)
        {
            animation[0].SetBool("Hang", false);
            animation[1].SetBool("Hang", false);
        }
        if(_crocoToCharecter <= 2.8f && !isPlayerAttachedToCroco)
        {
            charecter.transform.parent = rightCroco.transform;
            charecter.transform.localPosition = new Vector3(4.22f, -0.54f, -1.36f);
            charecter.transform.localRotation =  Quaternion.Euler(71.077f, -168.16f, 38.245f);
            for (int i = 0; i <= weights.Count-1; i++)
            {
                weights[i].GetComponent<Animator>().Play("scaleDown");
            }
            isPlayerAttachedToCroco = true;
        }
    }

    void weightChecker()
    {
        if (_LeftWeight > _weight)
        {
            isMovingUp = false;
            
        }

        else if (_LeftWeight < _weight)
        {
            isMovingUp = true;
            
        }

    }

    public void weightAdder()
    {
        foreach (Transform obj in weightCollector)
        {
            if (!weights.Contains(obj))
            {
                weights.Add(obj);
                _weight += obj.gameObject.GetComponent<_weightMovement>().weight;
            }
        }

    }

    void ropeMovement()
    {
        if (isMovingUp)
        {
            transform.Translate(Vector3.down * _speed * Time.deltaTime);
            rightRender.sprite = GameManager.gManager.scared;
            GameManager.gManager.wheelRotator(-1);
        }
        else if (!isMovingUp)
        {
            transform.Translate(Vector3.down * -_speed * Time.deltaTime);
            rightRender.sprite = GameManager.gManager.Smile;
            GameManager.gManager.wheelRotator(1);
        }
    }
}
