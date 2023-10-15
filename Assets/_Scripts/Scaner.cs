using System.Collections;
using TMPro;
using UnityEngine;

public class Scaner : MonoBehaviour
{

    // Start is called before the first frame update
    BoxSricptSender sender;
    BoxScript mainBox;
    [SerializeField] TextMeshProUGUI ScreenText;
    [SerializeField] TextMeshProUGUI ScreenCityText;
     public GameObject scanAnim;
    [SerializeField] Transform SpawnPoint;
    [SerializeField] GameObject SmokeEx;
    [SerializeField] Transform mainScaner;
    [SerializeField] AudioSource checkedAudio;
    [SerializeField] GameObject ElectroParticle;
    [SerializeField] Transform particleSpawnPos;
    [SerializeField] Animator ScreenEffect;
    [SerializeField] LevelManager lm;

    [SerializeField] private float FireRate = 10;
    [SerializeField] private float lastfired;
   

    
    bool canscan2 = false;

    public bool OnRightHand = true;
    public bool canscan = false;
    void Update()

    {
/*        if (canscan == true)
        {
            if (OnRightHand == true)
            {
                if (OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger))
                {
                    Click();
                }
            }
            else
            {
                if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
                {
                    Click();
                }
            }
          
        }
*/
        
    }

    void Click()
    {
            if (canscan2 == true)
            {
                scanAnim.SetActive(true);
                StartCoroutine(ScanStart(1));
                canscan = false;
                canscan2 = false;


        }

            else
            {
                scanAnim.SetActive(false);
                if (Time.time - lastfired > 1 / FireRate)
                {
                    lastfired = Time.time;
                   /* Instantiate(ElectroParticle, particleSpawnPos.position, particleSpawnPos.rotation);*/
                }
            }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="QR")
        {

            if (canscan == true)
            {
               /* if (OVRInput.Get(OVRInput.Button.Any) && OVRInput.Get(OVRInput.Button.SecondaryHandTrigger))
                {*/
                    sender = other.gameObject.GetComponent<BoxSricptSender>();
                    scanAnim.SetActive(true);
                    StartCoroutine(ScanStart(1));
               /* }*/
                sender = other.gameObject.GetComponent<BoxSricptSender>();
                canscan2 = true;

            }


        }

        if(other.gameObject.tag=="Floor" || other.gameObject.tag == "Conveyor" || other.gameObject.tag == "Trash" || other.gameObject.tag == "Trash")
        {
           

            Instantiate(SmokeEx, transform.position, transform.rotation);
            mainScaner.position = SpawnPoint.position;
            mainScaner.rotation = SpawnPoint.rotation;
            canscan = false;
            StartCoroutine(ScanDeactive());
        }
    }

     IEnumerator ScanDeactive()
    {
        yield return new WaitForSeconds(2);
        canscan = false;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "QR")
        {
            scanAnim.SetActive(false);


        }
    }
    public bool tutor = true;
    // Update is called once per frame
    IEnumerator ScanStart(int waittime)
    {
        yield return new WaitForSeconds(waittime);
        if(tutor==true)
        {
            lm.tutorialSteps += 1;
            lm.NextTutor();
            StartCoroutine(lm.NextTutorCorutine());
            tutor = false;
        }
        ScreenEffect.SetBool("anim", true);
        mainBox = sender.MainScript;
        ScreenText.gameObject.SetActive(true);
        
        ScreenText.text = mainBox.screen_title;
        ScreenCityText.text = mainBox.city_adress;
        checkedAudio.Play();
        scanAnim.SetActive(false);
        StartCoroutine(ScanActive());
    }

    IEnumerator ScanActive()
    {
        yield return new WaitForSeconds(1f);
        canscan = true;
        canscan2 = true;
        ScreenEffect.SetBool("anim", false);
    }

}
