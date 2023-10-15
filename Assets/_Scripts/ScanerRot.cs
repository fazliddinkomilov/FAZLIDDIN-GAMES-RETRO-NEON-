using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanerRot : MonoBehaviour
{
    bool inhand = false;
    GameObject playerHand;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "PlayerHand")
        {
            inhand = true;
            playerHand = other.gameObject;
        }
       
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "PlayerHand")
        {
            inhand = false;
        }
    }

    private void Update()
    {
        if(inhand==true)
        {
            transform.rotation = playerHand.transform.rotation;
        }

    }
}
