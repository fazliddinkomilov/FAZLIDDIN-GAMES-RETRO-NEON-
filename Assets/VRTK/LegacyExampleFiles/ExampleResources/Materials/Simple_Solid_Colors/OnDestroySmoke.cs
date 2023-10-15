using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDestroySmoke : MonoBehaviour
{
    [SerializeField] GameObject SmokeEx;
    public void DestroyGm()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void OnDestroy()
    {
        Instantiate(SmokeEx, transform.position, transform.rotation);
    }
}
