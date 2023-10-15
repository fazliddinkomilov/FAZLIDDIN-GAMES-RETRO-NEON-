using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialArrow : MonoBehaviour
{
    [SerializeField] Transform box;
    [SerializeField] Transform boxWrong;
    [SerializeField] GameObject boxForb;
    [SerializeField] Transform bag;
    [SerializeField] Transform boxDirty;
    [SerializeField] Transform scaner;
    [SerializeField] Transform checkPrinter;
    [SerializeField] Transform checkPrinterRed;
    [SerializeField] Transform city;
    [SerializeField] Transform trash;
    [SerializeField] Transform center;
    [SerializeField] LevelManager lm;
    bool tutor = true;
    [SerializeField] Transform player;
    [SerializeField] GameObject screenArrow;

    public GameObject[] TutorialScreens;
    public GameObject[] TutorialScreens2;
    public GameObject[] TutorialScreens3;
    public GameObject[] TutorialScreens4;
    public GameObject[] TutorialScreens5;

    private void OnTriggerEnter(Collider other)
    {
        if(lm.gamelevel==0)
        {
         if(other.gameObject.tag=="PlayerHand")
            {
                if(tutor==true)
                {
                    gameObject.GetComponent<Collider>().enabled = false;
                    lm.tutorialSteps += 1;
                    lm.NextTutor();
                    tutor = false;
                }
    
            }
        }
   
    }

    private void Start()
    {        
       
            StartCoroutine(FiveWait());
        
    }

    IEnumerator FiveWait()
    {

        yield return new WaitForSeconds(5);

        if (lm.gamelevel == 2)
        {
            boxForb.SetActive(true);
            lm.tutorialSteps += 1;
            lm.NextTutor();
        }
        if(lm.gamelevel >= 4)
        {
            boxDirty.gameObject.SetActive(true);
        }
   
        

    }
    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player);

        
        if(lm.Tutorial==false)
        {
            Destroy(gameObject);
        }

        if(lm.gamelevel==0)
        {
            if(lm.tutorialSteps==0)
            {
                transform.position = new Vector3(box.position.x, transform.position.y, box.position.z); 
            }
            else if(lm.tutorialSteps == 1 || lm.tutorialSteps == 6)
            {
                transform.position = new Vector3(scaner.position.x, 1.827f, scaner.position.z);
            }
            else if (lm.tutorialSteps == 2 || lm.tutorialSteps == 7)
            {
                transform.position = new Vector3(center.position.x, 1.827f, center.position.z);
            }
            else if(lm.tutorialSteps == 3)
            {
                transform.position = new Vector3(checkPrinter.position.x, 1.827f, checkPrinter.position.z);
            }
            else if(lm.tutorialSteps == 8)
            {
                transform.position = new Vector3(checkPrinterRed.position.x, 1.827f, checkPrinterRed.position.z);
            }
            else if (lm.tutorialSteps == 4)
            {
                screenArrow.SetActive(true);
                transform.position = new Vector3(city.position.x, 2.435f, city.position.z);
            }
            else if(lm.tutorialSteps==5)
            {
                boxWrong.gameObject.SetActive(true);
                gameObject.GetComponent<Collider>().enabled = true;
                transform.position = new Vector3(boxWrong.position.x, 1.995f, boxWrong.position.z);
                tutor = true;
            }
            else if(lm.tutorialSteps == 9)
            {
                transform.position = new Vector3(trash.position.x, trash.position.y, trash.position.z);
            }

            if(lm.tutorialSteps>=5)
            {
                if (boxWrong != null)
                {
                    boxWrong.gameObject.SetActive(true);
                }
               
            }
        }
        else if(lm.gamelevel==1)
        {
            if (lm.tutorialSteps == 0)
            {
                transform.position = new Vector3(-10.157f, 2.13f, 2.862f);
            }
            else
            {
                transform.position = new Vector3(999f, 999f, 999f);
            }
        }
        else if (lm.gamelevel == 2)
        {
            if (lm.tutorialSteps == 0)
            {
                transform.position = new Vector3(-10.066f, 1.461f, 4.202f);
            }
            else if(lm.tutorialSteps == 1)
            {
                transform.position = new Vector3(-10.674f, 1.672f, 3.907f);
            }
            else if (lm.tutorialSteps == 2)
            {
                transform.position = new Vector3(-11.243f, 2.195f, 4.189f);
            }
            else
            {
                transform.position = new Vector3(999f, 999f, 999f);
            }
        }
        else if (lm.gamelevel == 3)
        {
            if (lm.tutorialSteps == 0)
            {
                transform.position = new Vector3(-10.572f, 1.437f, 3.394f);
            }
            else if (lm.tutorialSteps == 1)
            {
                transform.position = new Vector3(bag.position.x, 2.1911f, bag.position.z);
            }
            else
            {
                transform.position = new Vector3(999f, 999f, 999f);
            }
        }
        else if (lm.gamelevel == 4)
        {
            if (lm.tutorialSteps == 0)
            {
                transform.position = new Vector3(boxDirty.position.x, 1.995f, boxDirty.position.z);
            }
            else
            {
                transform.position = new Vector3(999f, 999f, 999f);
            }
        }



    }
}
