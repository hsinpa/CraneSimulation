using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InteractibleObject {
	[CreateAssetMenu (menuName = "ContraintObject/NetWall")]
	public class NetWallObject : ConstraintObject {
		public GameObject NetWallPrefab;

		public float slight_collision_velocity;
		public float strong_collision_velocity;
	}
}