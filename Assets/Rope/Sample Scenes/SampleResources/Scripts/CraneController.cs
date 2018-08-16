using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obi;

public class CraneController : MonoBehaviour {

	ObiRopeCursor[] cursor;
	ObiRope[] rope;
    public float speed=0.6f;
    float t;
    public float ttime =15;
	// Use this for initialization
	void Start () {
		cursor = GetComponentsInChildren<ObiRopeCursor>();
        rope = new ObiRope[cursor.Length];
        for(int i = 0; i < cursor.Length; i++)
        {
            rope[i] = cursor[i].GetComponent<ObiRope>();
        }
		
	}
	
	// Update is called once per frame
	void Update () {

        if (t<ttime)
        {
            t += Time.deltaTime;
            if (t > 1)
            {
                if (t < 8)
                {
                    for (int i = 0; i < cursor.Length; i++)
                    {
                        if (rope[i].RestLength > 0f)
                            cursor[i].ChangeLength(rope[i].RestLength - 1f * Time.deltaTime);
                    }
                }
                else
                {
                    for (int i = 0; i < cursor.Length; i++)
                    {
                        cursor[i].ChangeLength(rope[i].RestLength + 1f * Time.deltaTime);
                    }

                }
            }

        }
        else
        {
            if (Input.GetKey(KeyCode.R))
            {
                for (int i = 0; i < cursor.Length; i++)
                {
                    if (rope[i].RestLength > 0f)
                        cursor[i].ChangeLength(rope[i].RestLength - 1f * Time.deltaTime * speed);
                }

            }

            if (Input.GetKey(KeyCode.F))
            {
                for (int i = 0; i < cursor.Length; i++)
                {
                    cursor[i].ChangeLength(rope[i].RestLength + 1f * Time.deltaTime * speed);
                }

            }
        }
		

		
	}
    private void FixedUpdate()
    {
        
    }
}
