using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sponge : MonoBehaviour
{
    [SerializeField] BoxCollider col;
    [SerializeField] Transform SpawnPoint;
    [SerializeField] GameObject SmokeEx;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "HandCheck")
        {
            col.enabled = true;
        }

        if (other.gameObject.tag == "Floor" || other.gameObject.tag == "Conveyor")
        {
            Instantiate(SmokeEx, transform.position, transform.rotation);
            col.transform.position = SpawnPoint.position;
            col.transform.rotation = SpawnPoint.rotation;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "HandCheck")
        {
            col.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "HandCheck")
        {
            col.enabled = false;
        }
    }
}
