using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace controlpanel {
    public class ControlMain : MonoBehaviour{
        bool onUI = false;
        public void OnUI(bool b)
        {
            onUI = b;
        }
        public LayerMask Planelayer;
        public LayerMask Modulelayer;
        public GameUnit current_unit;
        public Building.ICore core;
        public Building.IModule module;
        public List<IOperating> operating = new List<IOperating>();
        public GameObject button;
        public List<GameObject> buttons;
        public GameMaster GM;

        private void Awake()
        {
            GameObject.Find("GameMaster").GetComponent<GameMaster>();
        }


        void Start()
        {

            /*Type[] types = current_unit.GetType().GetInterfaces();
            foreach(Type t in types)
            {
                Debug.Log(t.Name);
                if(t.Name == "IModule")
                {
                    
                    module = current_unit as Building.IModule;
                    Debug.Log(module.GetType().Name);

                }
            }
            Type tp = current_unit.GetType().BaseType;
            if (tp.Name == "BuilingMain")
            {

                Building.BuilingMain builingMain = current_unit as Building.BuilingMain;
                Debug.Log(builingMain.GetType().Name);

            }
            */
            
            IOperating iop = gameObject.AddComponent<BasisControl>();
            AddOperating(iop);
            
        }


        public void AddOperating(IOperating iop)
        {
            
            operating.Add(iop);
            operating[operating.Count - 1].Enter(this);
        }

        public void RemoveOperating()
        {

            operating.Remove(operating[operating.Count - 1]);
            operating[operating.Count - 1].Exit();
        }

        void Update()
        {
            if (onUI==false) {//確認不是點在UI上
                if (Input.GetButtonDown("Fire1"))
                {
                    operating[operating.Count - 1].Confirm();
                }
                if (Input.GetButtonDown("Fire2"))
                {
                    operating[operating.Count - 1].Cancel();
                }
                operating[operating.Count - 1].ConUpdate();
                
            }

        }





        /// <summary>
        /// 啟用建造
        /// </summary>
        public void Put(string num)
        {

        }


    }
}
