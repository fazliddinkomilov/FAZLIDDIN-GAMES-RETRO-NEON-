using TMPro;
using UnityEngine;
using System.Collections;

public class CheckScript : MonoBehaviour
{
    public TextMeshProUGUI nametext;
    public TextMeshProUGUI addresstext;
    [SerializeField] GameObject SmokeEx;
    bool fall = false;
    public bool checkTypeGreen = true;
    void Start()
    {
        StartCoroutine(AnimOff());
    }

    IEnumerator AnimOff()
    {
        yield return new WaitForSeconds(0.5f);

        Destroy(gameObject.GetComponent<Animator>());

    }

    // Update is called once per frame
    private void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.tag == "Floor" || other.gameObject.tag == "Conveyor")
        {
            fall = true;
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if(fall == true)
        {
            Instantiate(SmokeEx, transform.position, transform.rotation);
        }
        
    }
}
