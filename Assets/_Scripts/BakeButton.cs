using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BakeButton : MonoBehaviour
{
    
    public ClampHandler bake;
    bool tutor = true;
    [SerializeField] LevelManager lm;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("HandCheck") || other.CompareTag("HandCheckLeft"))
        {
           bake.opened = true;

            if(tutor == true && lm.gamelevel==2)
            {
                lm.tutorialSteps += 1;
                lm.NextTutor();
                tutor = false;
            }
        }
    }
    // Update is called once per frame
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("HandCheck") || other.CompareTag("HandCheckLeft"))
        {
            bake.opened = false;
        }
    }
}
