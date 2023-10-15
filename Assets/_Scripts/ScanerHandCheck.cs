using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanerHandCheck : MonoBehaviour
{
    [SerializeField] Scaner scanerScr;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "HandCheck")
        {
            scanerScr.canscan = true;
            scanerScr.OnRightHand = true;
        }
        if (other.gameObject.tag == "HandCheckLeft")
        {
            scanerScr.canscan = true;
            scanerScr.OnRightHand = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "HandCheck")
        {
            scanerScr.canscan = false;
        }
        if (other.gameObject.tag == "HandCheck")
        {
            scanerScr.canscan = false;
        }
    }

    // Update is called once per frame
/*    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("PlayerHand"))
        {
            if (OVRInput.Get(OVRInput.Button.PrimaryHandTrigger) && OVRInput.Get(OVRInput.Button.SecondaryHandTrigger))
            {
                scanerScr.canscan = true;
                scanerScr.scanAnim.SetActive(true);
            }
            else
            {
                scanerScr.canscan = false;
                scanerScr.scanAnim.SetActive(false);
            }

        }
    }*/
}
