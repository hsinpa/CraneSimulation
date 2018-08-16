using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarDetector {
	private int sensor_num, layerNum;
	private Vector3 center;
	private Quaternion rotation;
	private float raycastWidth, raycastHeight, raycastLength;
	private System.Action callback;

	private float collision_cold_down = 5f;
    private float cold_down_time = 0;


	public PillarDetector(Vector3 p_center, Quaternion p_rotation, float p_raycast_height, float p_raycast_width,
		float p_raycast_length, System.Action p_callback, int p_layer=0) {
		center = p_center;
		rotation = p_rotation;
		raycastHeight = p_raycast_height;
		raycastWidth = p_raycast_width;
		raycastLength = p_raycast_length;
		callback = p_callback;
		layerNum = p_layer;
	}

	public void OnUpdate() {
		if (cold_down_time > Time.time) return;

		Collider[] colliders = Physics.OverlapBox( center, new Vector3(raycastWidth, raycastHeight, raycastLength), rotation, layerNum);

		if (colliders.Length >  0) {
			callback();
			cold_down_time = Time.time + collision_cold_down;
		}
	}


	
}
