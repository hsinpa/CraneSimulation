using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace controlpanel
{
    /// <summary>
    /// 基本操作
    /// </summary>
    public class BasisControl : MonoBehaviour,IOperating
    {
        ControlMain main;



        public void Enter(ControlMain controlMain)
        {
            main = controlMain;
        }

        public void Exit()
        {
            
        }

        public void Cancel()
        {
            
        }

        public void Confirm()
        {
            ClickRay();
        }

        public void ConUpdate()
        {
            updateRay();
            if (Input.GetKey(KeyCode.Space))
                Cursor.lockState = CursorLockMode.Confined;

        }
      
       

       
        /// <summary>
        /// 滑鼠點擊事件
        /// </summary>
        void ClickRay()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, main.Modulelayer))
            {
                
                if (main.current_unit != hit.collider.gameObject.GetComponent<GameUnit>())
                {
                    main.current_unit = hit.collider.gameObject.GetComponent<GameUnit>();
                    main.current_unit.Enter(main);
                    AddButton(main.current_unit.buttonDatas);
                }
            }
            else
            {
                if (main.current_unit != null)
                {
                    main.current_unit.Exit();
                    main.current_unit = null;
                    foreach(GameObject g in main.buttons)
                    {
                        g.gameObject.SetActive(false);
                        //Destroy(g);
                        
                    }
                }
                main.module = null;
            }
        }

        void updateRay()
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, main.Planelayer))
            {
                Debug.DrawLine(ray.origin, hit.point);
                //return true;
            }
            else
            {
                //return false;
            }
        }

        
        public void AddButton(List<ButtonData> buttonDatas)
        {
            
            Transform t = GameObject.Find("Canvas").transform;
            Text DescriptionUI = GameObject.Find("DescriptionUI").GetComponent<Text>();
            
            for (int i=0; i<buttonDatas.Count; i++)
            {
                GameObject bt = main.GM.buliding_buttons[i];
                bt.name = i.ToString();
                bt.SetActive(true);
                bt.transform.Find("Icon").GetComponent<Image>().sprite = buttonDatas[i].sprite;
                main.GM.Descriptions[i] = buttonDatas[i].Description;
                
               
                //ButtonEvent be = bt.GetComponent<ButtonEvent>();
                //be.SetButton(buttonDatas[i], main.current_unit);
                main.buttons.Add(bt);
            }
            
        }


    }
}