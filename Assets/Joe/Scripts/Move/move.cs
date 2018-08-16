using System;


using System.Collections;

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class move : MonoBehaviour {
    public float X;
    public float Y;
    Rigidbody rb;
    public float speed = 1;
    public float speedx = 1;
    public float speedy = 1;
    public float upspeed = 1;
    public float startForceX=100;
    public float startForceY = 100;
    Vector3 Vforce = Vector3.zero;

    public Transform Hanging;
    public Transform Locklight;
    public Transform Slide_rail;

    public string scene_name;
    //public InteractibleObject.NetWallObject[] ns;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();

	}
	
	


	void Update () {
        Locklight.position = new Vector3(Hanging.position.x, 0, Hanging.position.z);

        Slide_rail.position = new Vector3(transform.position.x, Slide_rail.position.y, Slide_rail.position.z);
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene(scene_name);
        }

    }

    float rre;
    float rre2;
    void FixedUpdate()
    {
        
        X = Input.GetAxis("Horizontal");
        Y = Input.GetAxis("Vertical");
        
        if (X == 0 && Y == 0)
        {
            rb.velocity = Vector3.zero;
        }
        else
        {
            float fz = rb.velocity.z;
            float fx = rb.velocity.x;
            if (rb.velocity == Vector3.zero)
            {
                //rb.AddForce(transform.right * X * startForceX);
                //rb.AddForce(transform.forward * Y * startForceY);
            }
            

            if (X == 1 || X == -1)
            {
                fx = Mathf.SmoothDamp(fx,speed*X,ref rre,speedx);
                //fx += X * speedx;
            }

                      
            if (Y == 1 || Y == -1)
            {
                fz = Mathf.SmoothDamp(fz, speed*Y, ref rre2, speedy);
                //fz += Y * speedy;
            }


            fx = extension.Clamp<float>(fx, -1, 1);
            fz = extension.Clamp<float>(fz, -1, 1);

            if (X == 0)
                fx = 0;
            if (Y == 0)
                fz = 0;

            Vforce = new Vector3(fx, 0, fz);
            if (X == 1 || X == -1|| Y == 1 || Y == -1)
            {

                rb.velocity = Vforce;

            }
                
        }
        
        

       
    }


}
