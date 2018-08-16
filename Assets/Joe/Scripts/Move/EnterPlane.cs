using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterPlane : MonoBehaviour {
    public move _move;
    public enum _Status
    {
        Start,
        Move,
        End,
        Null

    }
    public _Status status;
    RaycastHit hit;
    public float ground_hight;
    float gt;
    Rigidbody rb;
    public float Impact_force = 1;
    public bool intarget;
    //public 
    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        switch (status)
        {
            case _Status.Start:


                break;
            case _Status.Move:

                //if (Physics.Raycast(transform.position, out hit, 100))
                /*if (transform.position.y + ground_hight > 0.5)
                {
                    MainGameManager.DeductScore(DeductionStandard.Movement.exceed_height_50cm);
                }*/

                


                break;
            case _Status.End:

                if(transform.position.y + ground_hight > 0.15|| transform.position.y + ground_hight< 0.25)
                {
                    gt += Time.deltaTime;
                }

                

                break;

        } 
	}
    private void OnCollisionEnter(Collision collision)
    {
        switch (status)
        {
            case _Status.Start:


                break;
            case _Status.Move:

                MainGameManager.DeductScore(DeductionStandard.Movement.bargage_touch_ground);

                break;
            case _Status.End:

                if (collision.relativeVelocity.y > Impact_force)
                {
                    MainGameManager.DeductScore(DeductionStandard.Ending.bargage_touch_ground);
                }

                if (gt<2)
                {
                    MainGameManager.DeductScore(DeductionStandard.Ending.no_stop_before_landing);
                }

                if(collision.gameObject.tag == "Ground")
                {
                    if (intarget)
                    {
                        MainGameManager.DeductScore(DeductionStandard.Ending.touch_examine_line);
                    }
                    else
                    {
                        MainGameManager.DeductScore(DeductionStandard.Ending.wrong_landing_position);
                    }
                    
                }
                break;

        }
    }

 
}
