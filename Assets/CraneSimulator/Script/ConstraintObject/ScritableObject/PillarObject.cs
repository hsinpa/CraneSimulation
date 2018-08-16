using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InteractibleObject {
	[CreateAssetMenu (menuName = "ContraintObject/Pillar")]
	public class PillarObject : ConstraintObject {
		public GameObject PillarPrefab;
		public GameObject StripPrefab;

		public float slight_collision_velocity;
		public float strong_collision_velocity;
	}
}