using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InteractibleObject;
using Utility;

public class Pillar : BaseWorldTransform
{
    public PillarObject pillarObject;

    #region Public Variable	
    public bool HasStrip = false;
    public float sensorHeight;
    public float pillarHeight;
    private float _pillarHeight
    {
        get
        {
            return (pillarHeight * 2 * pillarObject._native_modifier);
        }
    }

    public float width = 1;
    private float _width
    {
        get
        {
            return width * pillarObject._meter_modifier;
        }
    }

    public float height = 3;
    private float _height
    {
        get
        {
            return height * pillarObject._native_modifier;
        }
    }

    private Vector3 center
    {
        get
        {
            return transform.position + (transform.right * height);
        }
    }

    private List<PillarSensor> pillarSensorList = new List<PillarSensor>();

    private Transform _bargage;
    private PillarDetector _pillarDetector;
    private System.Action _pillarDetectorCallback;
    private GameObject stripObject;

    private string last_duduct_item;

    #endregion
    #region Generate Object
    public override void BuildObject()
    {
        Transform mPrefabHolder = ClearHolder();

        Vector3 instPos = new Vector3(0, _height, 0),
                instScale = new Vector3(pillarObject.PillarPrefab.transform.localScale.x,
                            _height, pillarObject.PillarPrefab.transform.localScale.z);

        //Instantiate two identical pillar
        GameObject newPillar1 = GenerateObject(pillarObject.PillarPrefab, mPrefabHolder, instPos, instScale);
        GameObject newPillar2 = GenerateObject(pillarObject.PillarPrefab, mPrefabHolder, instPos, instScale);

        //Set x position, pillar1 is always at left
        newPillar1.transform.localPosition = new Vector3(-(_width),
                            newPillar1.transform.localPosition.y, newPillar1.transform.localPosition.z);

        newPillar1.transform.localPosition = new Vector3((_width),
                            newPillar1.transform.localPosition.y, newPillar1.transform.localPosition.z);

        //Generate horizontal strip, is needed
        if (HasStrip)
        {
            instPos = new Vector3(_width * 0.5f, _pillarHeight - (sensorHeight * 0.2f), 
                                    pillarObject.StripPrefab.transform.localScale.x );
            instScale = new Vector3(pillarObject.StripPrefab.transform.localScale.x,
                        _height, pillarObject.StripPrefab.transform.localScale.z);

            stripObject = GenerateObject(pillarObject.StripPrefab, mPrefabHolder, instPos, instScale);
        }

        //Generate color sensor
        HandleColorSensor(HasStrip, mPrefabHolder, newPillar1.transform);
        HandleColorSensor(HasStrip, mPrefabHolder, newPillar2.transform);
    }

    private GameObject GenerateObject(GameObject p_prefab, Transform p_holder, Vector3 p_pos, Vector3 p_scale)
    {
        GameObject newPillar = GameObject.Instantiate(p_prefab);
        // newPillar.name ="PillarObj[Prefab]";
        newPillar.transform.SetParent(p_holder);

        newPillar.transform.localPosition = p_pos;
        newPillar.transform.localScale = p_scale;
        newPillar.transform.localRotation = p_prefab.transform.rotation;

        return newPillar;
    }

    private void HandleColorSensor(bool hasStrip, Transform parent, Transform p_stick_pillar)
    {
        Vector3 scaleSize = new Vector3(pillarObject.PillarPrefab.transform.localScale.x + 0.01f,
                                        sensorHeight,
                                        pillarObject.PillarPrefab.transform.localScale.z + 0.01f);
        Material whiteMaterial = Resources.Load<Material>("Prefab/Material/white"),
                redMaterial = Resources.Load<Material>("Prefab/Material/red"),
                yellowMaterial = Resources.Load<Material>("Prefab/Material/yellow");

        GameObject whiteSensor = GenerateColorSensor(pillarObject.PillarPrefab, parent, p_stick_pillar, Vector3.zero, scaleSize, whiteMaterial, DeductionStandard.Constraint.white_range),
                    redSensor = GenerateColorSensor(pillarObject.PillarPrefab, parent, p_stick_pillar, Vector3.zero, scaleSize, redMaterial, DeductionStandard.Constraint.red_range),
                    yellowSensor = GenerateColorSensor(pillarObject.PillarPrefab, parent, p_stick_pillar, Vector3.zero, scaleSize, yellowMaterial, DeductionStandard.Constraint.yellow_range);

        if (hasStrip)
        {
            Vector3 startPosition = new Vector3(p_stick_pillar.position.x, _pillarHeight + sensorHeight, p_stick_pillar.position.z);
            whiteSensor.transform.position = startPosition;
            yellowSensor.transform.position = new Vector3(startPosition.x, startPosition.y + (sensorHeight * 1 * 2), startPosition.z);
            redSensor.transform.position = new Vector3(startPosition.x, startPosition.y + (sensorHeight * 2 * 2), startPosition.z);

        }
        else
        {
            GameObject redSensor2 = GenerateColorSensor(pillarObject.PillarPrefab, parent, p_stick_pillar, Vector3.zero, scaleSize, redMaterial, DeductionStandard.Constraint.red_range),
                        yellowSensor2 = GenerateColorSensor(pillarObject.PillarPrefab, parent, p_stick_pillar, Vector3.zero, scaleSize, yellowMaterial, DeductionStandard.Constraint.yellow_range);

            Vector3 startPosition = new Vector3(p_stick_pillar.position.x, _height, p_stick_pillar.position.z);
            whiteSensor.transform.position = startPosition;
            yellowSensor.transform.position = new Vector3(startPosition.x, startPosition.y + (sensorHeight * 1 * 2), startPosition.z);
            redSensor.transform.position = new Vector3(startPosition.x, startPosition.y + (sensorHeight * 2 * 2), startPosition.z);

            yellowSensor2.transform.position = new Vector3(startPosition.x, startPosition.y - (sensorHeight * 1 * 2), startPosition.z);
            redSensor2.transform.position = new Vector3(startPosition.x, startPosition.y - (sensorHeight * 2 * 2), startPosition.z);
        }
    }

    private GameObject GenerateColorSensor(GameObject p_prefab, Transform p_relative_holder, Transform finalHolder,
     Vector3 p_pos, Vector3 p_scale, Material p_material, string p_score_key)
    {
        GameObject newPillar = GameObject.Instantiate(p_prefab);
        newPillar.transform.SetParent(p_relative_holder);
        newPillar.transform.localPosition = p_pos;
        newPillar.transform.localScale = p_scale;

		newPillar.GetComponent<Collider>().isTrigger = true;

        newPillar.transform.SetParent(finalHolder);
        newPillar.GetComponent<Renderer>().material = p_material;

        Renderer renderer = newPillar.GetComponent<Renderer>();
        pillarSensorList.Add(new PillarSensor(renderer, p_score_key));
        return newPillar;
    }


    private Transform ClearHolder()
    {
        Quaternion tempRotation = Quaternion.identity;

        if (transform.Find("PrefabHolder") != null)
        {
            Transform prefabHolder = transform.Find("PrefabHolder");
            tempRotation = prefabHolder.localRotation;
            Object.DestroyImmediate(transform.Find("PrefabHolder").gameObject);
        }
        GameObject mPrefabHolder = new GameObject("PrefabHolder");
        mPrefabHolder.transform.SetParent(transform);
        mPrefabHolder.transform.localPosition = Vector3.zero;
        mPrefabHolder.transform.localRotation = tempRotation;

        return mPrefabHolder.transform;
    }
    #endregion

    #region In-Game Method
    public override void SetUp(Transform p_bargage)
    {
        _bargage = p_bargage;
        _pillarDetectorCallback = OnRaycastDetectObject;
        BuildObject();

        Vector3 center = transform.position;
        _pillarDetector = new PillarDetector(center + (transform.up * _height * 2),
        transform.rotation, _height * 2, _width * 2f, 0.1f, OnRaycastDetectObject, General.interactableLayerMask);
    }

    public void Update()
    {
        if (_bargage == null && _pillarDetector == null) return;


        float dist = (Mathf.Abs(_bargage.position.x - center.x) + Mathf.Abs(_bargage.position.z - center.z)) * 0.5f;
        if (dist < _width)
        {
            _pillarDetector.OnUpdate();
        }
        else if (last_duduct_item != "")
        {
            last_duduct_item = "";
        }
    }

    // void OnDrawGizmos()
    // {
    //     Gizmos.color = Color.yellow;
    //     Gizmos.DrawSphere(center, 0.5f);

    // 	Gizmos.color = Color.black;
    //     Gizmos.DrawSphere(transform.position, 0.5f);


    //     Gizmos.color = Color.blue;
    //     Gizmos.DrawSphere(transform.position + ( transform.right * _width), 0.5f);
    // }

    public void OnRaycastDetectObject()
    {
        Debug.Log("Bargage is being detect");
        Vector3 bargageBottomPos = _bargage.GetComponent<Renderer>().bounds.min;


        //Check if bargage is out from the x,z range of pillar
        Vector3 leftPillar = transform.position,
                rightPillar = transform.position + (transform.right * _width);

        if (rightPillar.x - leftPillar.x >= _width - 0.2f)
        {
            if (bargageBottomPos.x < leftPillar.x || bargageBottomPos.x > rightPillar.x)
            {
                Debug.Log("Horizontal Fail game, out of x,z range");
                DeductScore(DeductionStandard.ExamFail.move_pass_buildings);

                return;
            }
        }
        else
        {
            if (bargageBottomPos.z > leftPillar.z || bargageBottomPos.z < rightPillar.z)
            {
                Debug.Log("Verticle Fail game, out of x,z range");
                DeductScore(DeductionStandard.ExamFail.move_pass_buildings);

                return;
            }
        }

        //Check if bargage is out from y axis, but still within x,z range
        if (bargageBottomPos.y > _height * 2)
        {
            if (last_duduct_item != DeductionStandard.Constraint.over_object_height)
                DeductScore(DeductionStandard.Constraint.over_object_height);

            Debug.Log("Deduct 30 points, out of y range");
            return;
        }

        //Check the color range
        float bargageBottom = bargageBottomPos.y;
        for (int i = 0; i < pillarSensorList.Count; i++)
        {
            if (bargageBottom < pillarSensorList[i].renderer.bounds.max.y && bargageBottom > pillarSensorList[i].renderer.bounds.min.y)
            {
                if (pillarSensorList[i].scoreKey == DeductionStandard.Constraint.white_range) {
                    MoveCamera();
                    return;
                }

                if (last_duduct_item != pillarSensorList[i].scoreKey)
                    DeductScore(pillarSensorList[i].scoreKey);
                return;
            }
        }

        //Confirm bargage is in the black area, conduct deduction to score then...
        if (last_duduct_item != DeductionStandard.Constraint.black_range)
            DeductScore(DeductionStandard.Constraint.black_range);

    }

    public override void OnCollisionHandler(Collision collision, Transform p_transform)
    {
        Debug.Log("From : " + p_transform.name + ", Collider : " + collision.gameObject.name);
        Rigidbody colliderRigid = collision.gameObject.GetComponent<Rigidbody>();
        float constPower = 4;
        float hitPower = colliderRigid.velocity.magnitude;
        Debug.Log("Hitpower " + hitPower);
        if (hitPower > pillarObject.slight_collision_velocity && hitPower < pillarObject.strong_collision_velocity)
        {
            DeductScore(DeductionStandard.Constraint.slight_impact, true);
        }
        else if (hitPower >= pillarObject.strong_collision_velocity)
        {
            //Give rigidbody to pillar
            AddRigidBody(p_transform.gameObject, colliderRigid.velocity, constPower);

            if (HasStrip && stripObject != null)
            {
                AddRigidBody(stripObject, colliderRigid.velocity, constPower);
            }

            DeductScore(DeductionStandard.Constraint.knock_down, true);
        }
    }

    private void AddRigidBody(GameObject p_gameobject, Vector3 p_power_direction, float p_power)
    {
		p_power_direction = new Vector3(p_power_direction.x, 0, p_power_direction.z);
        Rigidbody pillarRigid = p_gameobject.GetComponent<Rigidbody>();
        if (pillarRigid == null) pillarRigid = p_gameobject.AddComponent<Rigidbody>();
        pillarRigid.AddForce(p_power_direction * p_power);
    }
    #endregion
}
