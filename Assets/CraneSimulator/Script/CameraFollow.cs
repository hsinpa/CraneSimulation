using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public Transform target;	

	void Start() {
		if (target != null) {
			this.transform.position = new Vector3(target.position.x, transform.position.y, transform.position.z);	
		}
	}

	// Update is called once per frame
	void Update () {
		if (target != null) {
			if ( Mathf.Abs(this.transform.position.x - target.transform.position.x) <= 0.01) return;

			float followXAxis = Mathf.Lerp(this.transform.position.x, target.transform.position.x, 0.15f);
			this.transform.position = new Vector3(followXAxis, transform.position.y, transform.position.z);	
		}
	}
}
