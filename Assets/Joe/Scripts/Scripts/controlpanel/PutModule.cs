using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Building
{
    public class PutModule : MonoBehaviour
    {
        public LayerMask lm;
        public IPutUp putUp;
        // Use this for initialization
        
        void Start()
        {
           
        }

        // Update is called once per frame
        void Update()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, lm))
            {
                Debug.DrawLine(ray.origin, hit.point);
                if (Input.GetButtonDown("Fire1"))
                {
                    
                }
                if (Input.GetButtonDown("Fire2"))
                {
                    
                }
            }
        }


        /// <summary>
        /// 設定模組
        /// </summary>
        public void SetModule(IPutUp p)
        {
            putUp = p;
        }


        /// <summary>
        /// 放置模組
        /// </summary>
        public void Put()
        {

        }

    }
}