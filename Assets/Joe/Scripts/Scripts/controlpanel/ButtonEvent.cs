using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonEvent {
    GameUnit unit;
    public Image Icon;
    public GameObject DescriptionUI;
    public Text Description;
    public void SetButton(ButtonData buttonData,GameUnit u)
    {
        unit = u;
        Icon.sprite = buttonData.sprite;
        Description.text = buttonData.Description;
    }

   
}
