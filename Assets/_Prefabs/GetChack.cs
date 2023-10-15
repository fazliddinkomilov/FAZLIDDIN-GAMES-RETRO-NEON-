using TMPro;
using UnityEngine;

public class GetChack : MonoBehaviour
{
    [SerializeField] GameObject greenCheck;
    [SerializeField] GameObject redCheck;
    [SerializeField] BoxScript mainScr;

    [SerializeField] TextMeshProUGUI checkName;
    [SerializeField] TextMeshProUGUI checkAddress;
    CheckScript check;

    [SerializeField] LevelManager lm;
    [SerializeField] BoxScript BOX;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag =="chek")
        {
            mainScr.hasCheck = true;
            
            check = other.gameObject.GetComponent<CheckScript>();
            checkName.text = check.nametext.text;
            checkAddress.text = check.addresstext.text;
            mainScr.check_name = check.nametext.text;
            mainScr.check_adress = check.addresstext.text;

            if(BOX.tutorialboxx == true)
            {
                 /* if(BOX.hasCheck==false)
                {*/
                    lm.tutorialSteps += 1;
                    lm.NextTutor();
                
                     
                    
            }
          

            if (check.checkTypeGreen == true)
            {
                mainScr.checkType = "Approved";
                greenCheck.SetActive(true);
                redCheck.SetActive(false);
            }
            else
            {
                mainScr.checkType = "NotApproved";
                redCheck.SetActive(true);
                greenCheck.SetActive(false);
            }

            Destroy(other.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
