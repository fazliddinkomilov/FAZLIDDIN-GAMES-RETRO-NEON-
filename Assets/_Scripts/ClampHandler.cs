using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClampHandler : MonoBehaviour
{
    Vector3 xP;
    public bool opened = false;
    [SerializeField] GameObject fireCol;

    // Start is called before the first frame update
    void Start()
    {
        xP = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        if(opened==false)
        {
             transform.position = new Vector3(xP.x, Mathf.Clamp(transform.position.y -0.005f, 1.34f, 2.15f), xP.z);
        }
        else
        {
            transform.position = new Vector3(xP.x, Mathf.Clamp(transform.position.y + 0.01f, 1.34f, 2.15f), xP.z);
        }
    
       if(transform.position.y <= 1.55f)
        {
            fireCol.SetActive(false);
        }
       else
        {
            fireCol.SetActive(true);
        }
     
    

    }
}
