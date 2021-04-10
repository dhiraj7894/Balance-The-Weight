using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leftRope : MonoBehaviour
{
    public static leftRope leftR;

    public float _speed = 1;
    public float _weight;
    public float _RightWeight;
    public float _crocoToCharecter;

    public bool isMovingUp = false;
    public bool isPlayerAttachedToCroco = false;

    public GameObject charecter;

    public Transform leftCroco;
    public Transform weightCollector;


    public List<Transform> weights = new List<Transform>();
    public List<Animator> animation = new List<Animator>();

    public SpriteRenderer leftRender;

    private Vector3 initPos;
    void Start()
    {
        leftR = this;
        initPos = transform.position;
    }


    void Update()
    {
        _RightWeight = rightRope.rightR._weight;
        if (!GameManager.gManager.IsBalanced)
        {
            weightChecker();
            ropeMovement();
            weightAdder();
            crocoChecker();
            GameOver();
        }
        weightBalanced();

    }
    void weightBalanced()
    {
        if (GameManager.gManager.IsBalanced)
        {
            animation[0].SetBool("Hang", false);
            transform.position = Vector3.MoveTowards(transform.position, initPos, 2 * Time.deltaTime);
            leftRender.sprite = GameManager.gManager.Smile;
        }
    }
    void GameOver()
    {
        if (isPlayerAttachedToCroco)
        {
            _speed = 0;
            transform.Translate(Vector3.down * 3 * Time.deltaTime);
            GameManager.gManager.wheelSpeed = 600;
        }

        if (rightRope.rightR.isPlayerAttachedToCroco)
        {
            _speed = 0;
            if (transform.position.y <= 3.7f)
            {
                transform.Translate(Vector3.up * 5 * Time.deltaTime);
            }
            if(transform.position.y >= 3.7f)
            {
                transform.position = new Vector3(1,3.7f,transform.position.z);
                charecter.GetComponent<Rigidbody>().useGravity = true;
                charecter.GetComponent<Rigidbody>().isKinematic = false;
                leftRender.sprite = GameManager.gManager.scared;
            }
        }
    }
    void crocoChecker()
    {
        _crocoToCharecter = Vector3.Distance(transform.position, leftCroco.position);
        if (_crocoToCharecter <= 4 && !isPlayerAttachedToCroco)
        {
            animation[0].SetBool("Hang", true);
        }
        if (_crocoToCharecter >= 4 && !isPlayerAttachedToCroco)
        {
            animation[0].SetBool("Hang", false);
        }
        if (_crocoToCharecter <= 2.8f && !isPlayerAttachedToCroco)
        {
            charecter.transform.parent = leftCroco.transform;
            charecter.transform.localPosition = new Vector3(3.4f, -0.02f, -1.57f);
            charecter.transform.localRotation = Quaternion.Euler(-75.31f, 103.69f, 88.614f);
            for(int i = 0; i <= weights.Count-1; i++)
            {
                weights[i].GetComponent<Animator>().Play("scaleDown");
            }

            isPlayerAttachedToCroco = true;
        }
    }
    public void weightAdder()
    {
        foreach(Transform obj in weightCollector)
        {
            if (!weights.Contains(obj))
            {
                weights.Add(obj);
                _weight += obj.gameObject.GetComponent<_weightMovement>().weight;
            }
        }
        
    }

    void weightChecker()
    {
        if(_RightWeight > _weight)
        {
            isMovingUp = false;
            
        }

        else if(_RightWeight < _weight)
        {
            isMovingUp = true;
            
        }

    }

    void ropeMovement()
    {
        if (isMovingUp)
        {
            transform.Translate(Vector3.down * _speed * Time.deltaTime);
            leftRender.sprite = GameManager.gManager.scared;
        }
        else if (!isMovingUp)
        {
            transform.Translate(Vector3.down * -_speed * Time.deltaTime);
            leftRender.sprite = GameManager.gManager.Smile;
        }
    }
}
