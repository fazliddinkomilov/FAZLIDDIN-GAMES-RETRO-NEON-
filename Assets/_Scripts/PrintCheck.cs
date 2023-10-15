using System.Collections;
using TMPro;
using UnityEngine;

public class PrintCheck : MonoBehaviour
{
    [SerializeField] GameObject checkprefab;
    GameObject currentCheck;

    [SerializeField] TextMeshProUGUI nameTextScreen;
    [SerializeField] TextMeshProUGUI addressTextScreen;

    [SerializeField] Transform SpawnPoint;
    [SerializeField] Transform ParentObj;
    CheckScript chek;

    GameObject LastCheck;

    int delay = 5;
    public void Print()
    {   

        if(delay>=5)
        {
            if (LastCheck!=null)
            {
                Destroy(LastCheck);
            }
            currentCheck = Instantiate(checkprefab, SpawnPoint.position, SpawnPoint.rotation);
            LastCheck = currentCheck;
            currentCheck.transform.parent = transform.parent;
            chek = currentCheck.GetComponent<CheckScript>();
            chek.nametext.text = nameTextScreen.text;
            chek.addresstext.text = addressTextScreen.text;


            StartCoroutine(ResMethod());
            delay = 0;
        }
       
        
    }

    IEnumerator ResMethod()
    {
        yield return new WaitForSeconds(5);
        delay = 5;
    }
}
