using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public ConveyorSimple[] belts;
    bool stop = false;
    [SerializeField] SpawnerScript spawner;
    int speed = 1;

    [SerializeField] Material[] ButtonColors;
    [SerializeField] MeshRenderer ButtonRenderer;
    bool butCol = false;
    [SerializeField] Money money;
    [SerializeField] GameObject[] Texts;
    [SerializeField] GameObject RedLights;

    [SerializeField] GameObject BallPrefab;
    private GameObject LastBall;
    [SerializeField] Transform Player;

    [SerializeField] Transform BallSwapwPos;
    public float FireRate = 2;  // The number of bullets fired per second
    public float fireRateMod = 2;
    [SerializeField] private float lastfired;
    [SerializeField] LevelManager lm;
    [SerializeField] GameObject TutoralStartButtons;
    [SerializeField] ButtonScript buttonTut;


    // Start is called before the first frame update
    void Update()
    {
        if(Input.GetKey(KeyCode.UpArrow))
        {
            ChangeSpeed(3);
        }

        if (stop == true)
        { 
            if (Time.time - lastfired > 1 / FireRate)
            {
                lastfired = Time.time;
                {
                    LastBall = Instantiate(BallPrefab, BallSwapwPos.position, transform.rotation);
                    LastBall.GetComponent<BallCanvas>().ball = -1;
                    LastBall.transform.LookAt(Player);
              
                    money.money -= 1;
                   
                    LastBall.GetComponent<BallCanvas>().Red.SetActive(true);
                }
            }
        }


    }
    public bool tutorial = true;
    public void ButtonPressed()
    {
      
        if (stop==false)
        {
            for(int i = 0; i<belts.Length; i++)
            {
                belts[i].enabled = false;
            }
            spawner.gameObject.SetActive(false);
            spawner.TurnMode(false);
            stop = true;
            Texts[0].SetActive(false);
            Texts[1].SetActive(true);
            RedLights.SetActive(true);
               
        }
        else
        {
            for (int i = 0; i < belts.Length; i++)
            {
                belts[i].enabled = true;
            }
            spawner.TurnMode(true);
            stop = false;
            Texts[1].SetActive(false);
            Texts[0].SetActive(true);
            RedLights.SetActive(false);

    
        }

       
        
    }

  public void SwitchCololr()
    {
        if(butCol==false)
        {
            ButtonRenderer.material = ButtonColors[1];
            butCol = true;
            
        }
        else
        {
            ButtonRenderer.material = ButtonColors[0];
            butCol = false;
        }
    }

    public void ChangeToRed()
    {
        ButtonRenderer.material = ButtonColors[1];
        butCol = true;
    }

  

    public void ChangeSpeed(int speedButton)
    {
        if (lm.gamelevel == 1)
        {
            if (buttonTut.tutorial == true)
            {
                TutoralStartButtons.SetActive(true);
                lm.tutorialSteps += 1;
                lm.NextTutor();
                buttonTut.tutorial = false;
            }

        }


        speed = speedButton;
        ButtonRenderer.material = ButtonColors[0];
        if (lm.Tutorial == false)
        {
            if (speed == 1)
            {
                for (int i = 0; i < belts.Length; i++)
                {
                    belts[i].speed = 0.49f;
                }
                spawner.delay = 20;

            }
            else if (speed == 2)
            {
                for (int i = 0; i < belts.Length; i++)
                {
                    belts[i].speed = 0.7f;
                }
                spawner.delay = 15;

            }
            else if (speed == 3)
            {
                for (int i = 0; i < belts.Length; i++)
                {
                    belts[i].speed = 1.2f;
                }
                spawner.delay = 12;

            }

            spawner.TurnMode(false);
            spawner.TurnMode(true);

        }
 
        OVRInput.SetControllerVibration(0.2f, 0.2f, OVRInput.Controller.RTouch);
        OVRInput.SetControllerVibration(0.2f, 0.2f, OVRInput.Controller.LTouch);
    }


}
