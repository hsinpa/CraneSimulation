using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace noooo {
    public class TRANGOTO : MonoBehaviour {
        public Transform jjjj;
        // Use this for initialization
        void Start() {

        }

        // Update is called once per frame
        void Update() {
            transform.position = jjjj.position;
            transform.rotation = jjjj.rotation;
        }
    }
}
