using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class JoystickControl : MonoBehaviour
{
    public Transform topOfJoystick;

    [SerializeField] float forwardBackwardTilt = 0;
    [SerializeField] float sideToSideTill = 0;
    [SerializeField] float a = -20;
    [SerializeField] float b = 20;
    bool canUse = false;

    private void Start()
    {
    
    }

    private void Update()
    {
        if (OVRInput.Get(OVRInput.Button.PrimaryHandTrigger))
        {
            canUse = true;
        }
        else
        {
            canUse = false;
        }
            /* LimitRot();*/
            /* transform.localPosition = new Vector3(transform.localPosition.x, 0, 0);*/
        forwardBackwardTilt = topOfJoystick.rotation.eulerAngles.x;



        if (forwardBackwardTilt < 355 && forwardBackwardTilt > 290)
        {
            forwardBackwardTilt = Math.Abs(forwardBackwardTilt - 360);
     
            Debug.Log("Backward" + forwardBackwardTilt);
        }
        else if (forwardBackwardTilt > 5 && forwardBackwardTilt < 74)
        {
            Debug.Log("Forward" + forwardBackwardTilt);
     
        }

        sideToSideTill = topOfJoystick.rotation.eulerAngles.z;
    }
    void LimitRot()
    {
        Vector3 headFix = transform.eulerAngles;
        headFix.x = (headFix.x > 180) ? headFix.x - 360 : headFix.x;
        headFix.x = Mathf.Clamp(headFix.x, a, b);
        transform.rotation = Quaternion.Euler(headFix);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("PlayerHand"))
        {
            if(canUse==true)
            {
                transform.LookAt(other.transform.position, transform.up);
            }
                

            
            
        }
    }

}
