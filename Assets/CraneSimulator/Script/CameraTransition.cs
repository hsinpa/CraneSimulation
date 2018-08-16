using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTransition : MonoBehaviour {
	
	bool EnableTransition;
	int speed;
	Vector3 distination;

	MeshRenderer meshRenderer;
	Material material;
	Color color;

	public void TransitionTo(int p_speed, Vector3 p_distination) {
		EnableTransition = true;
		speed = p_speed;
		distination = p_distination;

		meshRenderer = transform.Find("SurroundCircle").GetComponent<MeshRenderer>();
		material = meshRenderer.material;
		color = material.GetColor("_Color");
	}

	private void Update() {
		if (EnableTransition) {
			if (color.a > 0.9f) {
				transform.position = distination;
				speed = -speed;
			}

			color = new Color(color.r, color.b, color.g, (color.a + (speed*Time.deltaTime)) );
			material.SetColor("_Color", color);

			if (color.a < 0.1f && speed < 0) {
				color = new Color(color.r, color.b, color.g, 0) ;
				material.SetColor("_Color", color );
				EnableTransition = false;
			}
		}
	}

}
