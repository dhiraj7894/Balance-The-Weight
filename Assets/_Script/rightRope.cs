using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rightRope : MonoBehaviour
{
    public static rightRope rightR;

    public float _speed = 1;
    public float _weight;
    public float _LeftWeight;

    public bool isMovingUp = false;

    public Transform weightCollector;
    public List<Transform> weights = new List<Transform>();

    void Start()
    {
        
        rightR = this;
    }


    void Update()
    {
        _LeftWeight = leftRope.leftR._weight;
        ropeMovement();
        weightAdder();
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
        }
        else if (!isMovingUp)
        {
            transform.Translate(Vector3.down * -_speed * Time.deltaTime);
        }
    }
}
