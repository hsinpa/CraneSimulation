using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StartServer : MonoBehaviour {
    testClient tc = new testClient();
    TestServer ts = new TestServer();
    public List<string> jSONOs;
    public string ip;
    // Use this for initialization



    public void CreateServer()
    {
        ts.Listen();
    } 

    void Start() {
        int i = 0;
        foreach (Selectable selectable in GameMaster.allSelectables)
        {
            selectable.ID = i;
            i += 1;
        }

    }

    // Update is called once per frame
    void Update() {

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("st");
            tc.ConnectServer(ip,this);
        }
        if (jSONOs.Count > 0)
        {
            JSONObject JSONData = new JSONObject();
            foreach (string s in jSONOs)
            {
                JSONData = new JSONObject(s);
                Debug.Log(JSONData.ToString());
                switch (JSONData.GetField("Type").str)
                {
                    case "Move":
                        Debug.Log(JSONData.GetField("Type").str);
                        foreach (Selectable selectable in GameMaster.allSelectables)
                        {
                            
                            Debug.Log(JSONData.GetField("ObjectID").num.ToString());
                            
                            if (JSONData.GetField("ObjectID").num == selectable.ID)
                            {
                                
                                selectable.UnitMove
                                    (
                                    extension.StringToVector3(JSONData.GetField("Point").str),
                                    extension.StringToVector3(JSONData.GetField("Pos").str)
                                    );
                            }

                        }

                        break;
                    case "PlayerID":
                        Debug.Log(JSONData.GetField("PlayerID").num.ToString());
                        foreach (Selectable selectable in GameMaster.allSelectables)
                        {
                            GameMaster.myID = JSONData.GetField("PlayerID").num;

                            selectable.SetCamp(selectable.ownerID);

                        }

                        break;

                }
               
            }
            jSONOs.Clear();

        }
        /*
        if (Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("UP");
            JSONObject json = new JSONObject();


            json.AddField("ID", gameObject.name);
            json.AddField("pos", transform.position.ToString());
            json.AddField("num", 1231515656);
        }*/
    }


    public void onData_toServer(JSONObject Data)
    {

        

        tc.SckSSend(Data.ToString());


    }

    public void p(string s)
    {
       
    }


    private void OnApplicationQuit()
    {
        tc.Quit();
        ts.Quit();
    }

}


