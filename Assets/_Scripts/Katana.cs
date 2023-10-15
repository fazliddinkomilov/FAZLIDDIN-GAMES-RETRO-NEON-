using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Katana : MonoBehaviour
{
    public KatanaHolder holder;
    [SerializeField] GameObject SmokeEx;
    bool des = false;
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Floor" || col.gameObject.tag == "Coveyor" || col.gameObject.tag == "Trash" || col.gameObject.tag == "Bake")
        {
            Destroy(gameObject);
            des = true;
        }
    }

    private void Update()
    {
        if (transform.position.y >= 1.45 || transform.position.y <= 1.2 || transform.position.x <= -10.702)
        {
            gameObject.GetComponent<Rigidbody>().useGravity = true;
            StartCoroutine(KatanaHolderActive());
           
        }
        if (transform.position.y < -20)

        {
            Destroy(gameObject);
        }
    }

        private void OnTriggerExit(Collider other)
    {
     /*   if (other.gameObject.tag == "HammerSnapPose")
        {

            gameObject.GetComponent<Rigidbody>().isKinematic = false;
        }*/
    }

    IEnumerator KatanaHolderActive()
    {
        yield return new WaitForSeconds(0.5f);
        holder.GetComponent<Collider>().enabled = true;
        holder.TutorialNext();


    }
    // Update is called once per frame
    void OnDestroy()
    {
        if(des==true)
        {
            Instantiate(SmokeEx, transform.position, transform.rotation);
        }
        holder.GetComponent<Collider>().enabled = false;
        holder.SpawnKatana();

    }
}
