using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using controlpanel;

public class GameUnit : MonoBehaviour {

    public string nameUI;
    public string Description;
    public Sprite sprite;
    
    public EventFlag.UnitStatus unitStatus;
    public List<ButtonData> buttonDatas;

    ControlMain main;

    public void Enter(ControlMain controlMain)
    {
        main = controlMain;

    }

    public void Exit()
    {

    }

}
