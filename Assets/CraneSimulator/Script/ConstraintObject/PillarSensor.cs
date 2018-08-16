using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PillarSensor {
	public Renderer renderer;
	public string scoreKey;	

	public PillarSensor(Renderer p_renderer, string p_score_key) {
		renderer = p_renderer;
		scoreKey = p_score_key;
	}
}
