using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public LayerMask lm;
    public Vector3 v3;
    //public GameObject gameO;
    public bool b;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        RaycastHit hit;

        if (Physics.Raycast(transform.position+new Vector3(0,2,0), -Vector3.up, out hit,100f, lm))
        {
            v3 = hit.point;
            b = true;

        }
        else
        {
           // b = false;
            if (b == true)
            {
                MainGameManager.DeductScore(DeductionStandard.Movement.exceed_path);
                Debug.Log("out");
                //Instantiate(gameO, v3, transform.rotation);
                b = false;
            }

            if (b == false)
            {

            }

        }


    }
}
