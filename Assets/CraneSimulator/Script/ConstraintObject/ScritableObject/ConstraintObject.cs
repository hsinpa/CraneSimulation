using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InteractibleObject
{
	public abstract class ConstraintObject : ScriptableObject {
		public AudioClip touch_sound_clip;
		
		public float _meter_modifier = 5;

		public float _native_modifier {
			get {
				return _meter_modifier / 2.48f;
			}
		}

		// public abstract void OnCollision(Collider obj);
	}	
}
