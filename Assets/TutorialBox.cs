using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBox : MonoBehaviour
{

    [SerializeField] LevelManager lm;
    [SerializeField] BoxScript BOX;
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
      /*  if(BOX.hasCheck == true)
        {
            if(lm.tutorialSteps<4)
            {
                lm.tutorialSteps += 1;
                lm.NextTutor();
            }
          
        }*/
    }
}
