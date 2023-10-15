using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCheck : MonoBehaviour
{
    Animator anim;
    [SerializeField] bool RedCheck = false;
    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if(RedCheck==false)
        {
            if (other.gameObject.tag == "GreenCheck")
            {

                anim.SetBool("open", true);


                StartCoroutine(CloseDoor());
            }
        }
        else
        {
            if (other.gameObject.tag == "RedCheck")
            {

                anim.SetBool("open", true);


                StartCoroutine(CloseDoor());
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        /*        if (other.gameObject.tag == "Enemy")
                {
                    anim.SetBool("open", false);
                }*/
    }

    IEnumerator CloseDoor()
    {
        yield return new WaitForSeconds(3);
        anim.SetBool("open", false);

    }
}
