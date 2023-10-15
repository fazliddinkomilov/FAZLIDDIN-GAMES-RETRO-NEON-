using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XYZ2 : MonoBehaviour
{ 	
	public float RSX;
	public float RSY;
	public float RSZ;
    // Start is called before the first frame update
    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {
       transform.Rotate(RSX ,RSY,RSZ);  
    }
}
