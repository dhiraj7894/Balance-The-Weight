using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _weightMovement : MonoBehaviour
{
    //Charecters
    [SerializeField] private GameObject leftRopeCharecter;
    [SerializeField] private GameObject rightRopeCharecter;
    [SerializeField] private Vector3 offsetPosition;
    
    [SerializeField] private bool isMouseMoving = false;
    [SerializeField] public float weight;




    private Vector3 lRCDistance; //Distance from left Charecter.
    private Vector3 rRCDistance; //Distance from right Charecter.
    private Vector3 initialPosition;
    void Start()
    {
        initialPosition = this.transform.localPosition;
    }


    void Update()
    {
        if (isMouseMoving)
            movement();

    }

    void movement()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        this.transform.localPosition = new Vector3(mousePosition.x - offsetPosition.x, mousePosition.y - offsetPosition.y, this.transform.localPosition.z);
    }
    private void OnMouseDown()
    {
        isMouseMoving = true;
    }
    private void OnMouseUp()
    {
        isMouseMoving = false;
        DistanceChecker();
    }

    void DistanceChecker()
    {
        lRCDistance.x = Mathf.Abs(this.transform.localPosition.x - leftRopeCharecter.transform.position.x);
        lRCDistance.y = Mathf.Abs(this.transform.localPosition.y - leftRopeCharecter.transform.position.y);

        rRCDistance.x = Mathf.Abs(this.transform.localPosition.x - rightRopeCharecter.transform.position.x);
        rRCDistance.y = Mathf.Abs(this.transform.localPosition.y - rightRopeCharecter.transform.position.y);

        if (lRCDistance.magnitude < 0.9f)
        {
            /*leftRope.leftR.weightAdder();*/

            if(weight == 3)
            {
                this.transform.parent = leftRopeCharecter.transform.GetChild(2).transform;
                this.transform.localPosition = new Vector3(-0.3f, 0, 0.45f);
                this.transform.rotation = Quaternion.Euler(-50, -90, 90);
            }
            if(weight == 4)
            {
                this.transform.parent = leftRopeCharecter.transform.GetChild(2).transform;
                this.transform.localPosition = new Vector3(0.3f, 0, 0.45f);
                this.transform.rotation = Quaternion.Euler(-130, -90, 90);

            }
            if (weight == 7)
            {
                this.transform.parent = leftRopeCharecter.transform.GetChild(2).transform;
                this.transform.localPosition = new Vector3(-0.3f, 0, 0.45f);
                this.transform.rotation = Quaternion.Euler(-50, -90, 90);
            }
        }
        else if (rRCDistance.magnitude < 0.9f)
        {
            if (weight == 3)
            {
                this.transform.parent = rightRopeCharecter.transform.GetChild(2).transform;
                this.transform.localPosition = new Vector3(0.3f, 0, 0.45f);
                this.transform.rotation = Quaternion.Euler(-130, -90, 90);
            }
            if (weight == 4)
            {
                this.transform.parent = rightRopeCharecter.transform.GetChild(2).transform;
                this.transform.localPosition = new Vector3(-0.3f, 0, 0.45f);
                this.transform.rotation = Quaternion.Euler(-50, -90, 90);
            }
            if (weight == 7)
            {
                this.transform.parent = rightRopeCharecter.transform.GetChild(2).transform;
                this.transform.localPosition = new Vector3(0.3f, 0, 0.45f);
                this.transform.rotation = Quaternion.Euler(-130, -90, 90);
            }
        }

        else if (lRCDistance.magnitude > 0.9f && rRCDistance.magnitude > 0.9f)
        {
            this.transform.position = initialPosition;
        }

    }
}