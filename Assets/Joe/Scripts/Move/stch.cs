using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stch : MonoBehaviour {
    public EnterPlane._Status status;
    public bool ispoint;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Target")
        {
            
            if (ispoint)
            {
                other.GetComponent<EnterPlane>().intarget = true;
            }
            else
            {
                other.GetComponent<EnterPlane>().status = status;
            }
        }
    }
}
