using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System;

public class Selectable : MonoBehaviour, ISelectHandler, IPointerClickHandler, IDeselectHandler {

   
    public HashSet<Selectable> enemySelected = new HashSet<Selectable>();

    Renderer myRenderer;
    public int ID;
    [SerializeField]
    GameObject unselectedGameObject;
    //Material unselectedMaterial;
    [SerializeField]
    GameObject selectedGameObject;


    public enum UnitStatus
    {
        Idle,
        Run,
        Attack

    }
    public UnitStatus status;


    public int ownerID = 0;
    
    public Animator ani;
    public Transform targets;
    public float attack_radius;
    public Vector3 move_point;
    public float move_speed;
    RaycastHit hit;
    public StartServer startServer;

    public GameObject bullet;
    public float bullet_speed;
    
    public void Fire()
    {
        GameObject b = Instantiate(bullet, transform.position + transform.forward  + transform.up*0.5f , transform.rotation);
        b.transform.LookAt(targets.position);
        b.GetComponent<Rigidbody>().AddForce(b.transform.forward * bullet_speed);
        if (Vector3.Distance(transform.position, targets.transform.position) > attack_radius)
        {

        }
    }

    public void SetCamp(int num)
    {
        ownerID = num;
        foreach (Selectable selectable in GameMaster.allSelectables)
        {

            if (selectable.ownerID != ownerID)
            {
                enemySelected.Add(selectable);
            }

        }

    }
    void Awake()
    {
        GameMaster.allSelectables.Add(this);
        startServer = GameObject.Find("Game").GetComponent<StartServer>();
        foreach(int ID in GameMaster.mycamp)
        {
            
        }
        

        //myRenderer = GetComponent<Renderer>();
    }
    void Start()
    {
        
       
        ani = GetComponent<Animator>();
    }


    public void UnitMove(Vector3 point,Vector3 pos)
    {
        move_point = point;
        transform.position = pos;
        status = UnitStatus.Run;
    }

    // Update is called once per frame
    void Update()
    {

        if (GameMaster.currentlySelected.Contains(this))
        {
            
            if (Input.GetMouseButtonDown(1))
            {
                
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
                {
                   
                    if (hit.transform.tag == "Unit")
                    {
                        targets = hit.transform;
                        status = UnitStatus.Attack;
                    }
                    else
                    {
                        JSONObject json = new JSONObject();


                        json.AddField("Type", "Move");
                        json.AddField("ObjectID", ID);
                        json.AddField("Point", hit.point.ToString());
                        json.AddField("Pos", transform.position.ToString());
                        
                        
                        
                        startServer.onData_toServer(json);
                       // move_point = hit.point;
                       // status = UnitStatus.Run;
                    }

                }
            }
        }

        switch (status)
        {
            case UnitStatus.Idle:
                ani.SetFloat("Run", 0);
                foreach (Selectable selectable in enemySelected)
                {
                    if (Vector3.Distance(transform.position,selectable.transform.position)<attack_radius)
                    {
                        targets = selectable.transform;
                        status = UnitStatus.Attack;
                    }
                }
                break;
            case UnitStatus.Run:
                ani.SetFloat("Run", 1);
                transform.LookAt(move_point);
                transform.position += transform.forward * move_speed * Time.deltaTime;
                if (Vector3.Distance(transform.position, move_point) < 0.08)
                {
                    status = UnitStatus.Idle;
                    move_point = Vector3.zero;
                }
                break;
            case UnitStatus.Attack:
                transform.LookAt(targets.position);
                ani.SetFloat("Run", -1);
                break;

        }
    }

    


    public void OnPointerClick(PointerEventData eventData)
    {
        if (!Input.GetKey(KeyCode.LeftControl) && !Input.GetKey(KeyCode.RightControl))
        {
            DeselectAll(eventData);
        }
        OnSelect(eventData);
    }

    public void OnSelect(BaseEventData eventData)
    {
        if (ownerID == GameMaster.myID)
        {
            GameMaster.currentlySelected.Add(this);
            selectedGameObject.SetActive(true);
        }
        
    }

    public void OnDeselect(BaseEventData eventData)
    {
        selectedGameObject.SetActive(false);
    }

    public static void DeselectAll (BaseEventData eventData)
    {
        foreach(Selectable selectable in GameMaster.currentlySelected)
        {
            selectable.OnDeselect(eventData);
        }
        GameMaster.currentlySelected.Clear();
    }
}
