using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour {
    // public static List<ButtonData> _buttonDatas;

    public enum CampPlayer
    {
        Player0 = 0,
        Player1,
        Player2,
        Player3,
        Player4,
        Player5,
        Player6,
        Player7,
        Player8,
        Player9,
        Player10,
        Player11,
        Player12,
        PlayerNull,

    }
    public static HashSet<Selectable> allSelectables = new HashSet<Selectable>();
    public static HashSet<Selectable> currentlySelected = new HashSet<Selectable>();
    public static HashSet<GameObject> Effects = new HashSet<GameObject>();
    public List<ButtonData> buttonDatas;
    public List<GameUnit> gameUnits;
    public List<GameObject> buliding_buttons;
    public List<string> Descriptions;
    public static List<int> mycamp;
    public static List<List<int>> campData;
    public static int myID;

    public static void SetPlayerData (){

        
    }
}
