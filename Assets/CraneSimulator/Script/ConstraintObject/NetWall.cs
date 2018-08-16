using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InteractibleObject;
using Utility;
using System.Linq;

public class NetWall : BaseWorldTransform {
	public NetWallObject pillarObject;

	#region Public Variable
	public float netHeight;
	private float _netHeight {
		get {
			return (netHeight * pillarObject._native_modifier * 2);
		}
	}

	public float width = 1;
	private float _width {
		get {
			return width * pillarObject._meter_modifier;
		}
	}

	public float height = 3;
	private float _height {
		get {
			return height * pillarObject._native_modifier;
		}
	}

	public float length = 2;
	private float _length {
		get {
			return length * pillarObject._meter_modifier * 0.65f;
		}
	}

	private Vector3 center, bt_left, bt_right, top_left, top_right;
	private List<Vector3> corners = new List<Vector3>();

	private List<PillarSensor> pillarSensorList = new List<PillarSensor>();

	private Transform _bargage;
	private PillarDetector _pillarDetector;
	private System.Action _pillarDetectorCallback;

	private string last_duduct_item;

	#endregion
	#region Generate Object
	public override void BuildObject() {
		Transform mPrefabHolder = ClearHolder();

		Vector3 instPos = new Vector3(0, _height, 0);

		//Instantiate two identical pillar
		GameObject[] newNetWalls = new GameObject[2];
		newNetWalls[0] = GenerateObject(pillarObject.NetWallPrefab, mPrefabHolder, instPos, Vector3.one); 
		newNetWalls[1] = GenerateObject(pillarObject.NetWallPrefab, mPrefabHolder, instPos, Vector3.one); 

		//Set x position, pillar1 is always at left
		newNetWalls[0].transform.localPosition = new Vector3(-(_width * 0.5f), 
							newNetWalls[0].transform.localPosition.y, newNetWalls[0].transform.localPosition.z  );

		newNetWalls[1].transform.localPosition = new Vector3(_width * 0.5f, 
							newNetWalls[1].transform.localPosition.y, newNetWalls[1].transform.localPosition.z  );

		foreach (GameObject s_netWall in newNetWalls) {
			Transform pillarOne = s_netWall.transform.Find("Pillar-1");
			pillarOne.localScale = new Vector3 (pillarOne.localScale.x, _height, pillarOne.localScale.z);

			Transform pillarTwo = s_netWall.transform.Find("Pillar-2");
			pillarTwo.localScale = new Vector3 (pillarTwo.localScale.x, _height, pillarTwo.localScale.z);
			pillarTwo.localPosition = pillarTwo.localPosition + (Vector3.forward * _length*0.96f);

			Transform NetWall = s_netWall.transform.Find("NetWall");
			NetWall.localPosition = NetWall.localPosition + (transform.up * _netHeight * 0.46f) + (Vector3.forward * _length * 0.48f);
			NetWall.localScale = new Vector3 (NetWall.localScale.x, _netHeight, _length);

		}


		//Generate color sensor
	}

	private GameObject GenerateObject(GameObject p_prefab, Transform p_holder, Vector3 p_pos, Vector3 p_scale) {
		GameObject newPillar =  GameObject.Instantiate(p_prefab);
		// newPillar.name ="PillarObj[Prefab]";
		newPillar.transform.SetParent(p_holder);

		newPillar.transform.localPosition = p_pos;
		newPillar.transform.localScale = p_scale;
		newPillar.transform.localRotation = p_prefab.transform.rotation;

		return newPillar;
	}


	private GameObject GenerateColorSensor(GameObject p_prefab, Transform p_relative_holder, Transform finalHolder,
	 Vector3 p_pos, Vector3 p_scale, Material p_material, string p_score_key) {
		GameObject newPillar =  GameObject.Instantiate(p_prefab);
		newPillar.transform.SetParent(p_relative_holder);
		newPillar.transform.localPosition = p_pos;
		newPillar.transform.localScale = p_scale;

		newPillar.transform.SetParent(finalHolder);
		newPillar.GetComponent<Renderer>().material = p_material;

		Renderer renderer = newPillar.GetComponent<Renderer>();
		pillarSensorList.Add(new PillarSensor(renderer, p_score_key ));
		return newPillar;
	}
	

	private Transform ClearHolder() {
		Quaternion tempRotation = Quaternion.identity;

		if (transform.Find("PrefabHolder") != null)	{
			Transform prefabHolder = transform.Find("PrefabHolder");
			tempRotation =  prefabHolder.localRotation;
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
	public override void SetUp(Transform p_bargage) {
		_bargage = p_bargage;
		_pillarDetectorCallback = OnRaycastDetectObject;

		center = transform.position + (transform.forward * _length*0.5f);

		corners.Add(center + (transform.forward * _length*0.5f) + (transform.right * _width * 0.5f));
		corners.Add( center + (transform.forward * _length*0.5f) - (transform.right * _width * 0.5f));

		corners.Add( center - (transform.forward * _length*0.5f) + (transform.right * _width * 0.5f));
		corners.Add(center - (transform.forward * _length*0.5f) - (transform.right * _width * 0.5f));
		ReorganizedPillarPosition();

		BuildObject();
		_pillarDetector = new PillarDetector( center + (transform.up * _height * 2), 
		transform.rotation, _height * 2, _width*1.5f, _length,OnRaycastDetectObject, General.interactableLayerMask);
	}

	public void Update() {
		if (_bargage == null && _pillarDetector == null) return;

		float dist = (Mathf.Abs(_bargage.position.x - center.x) + Mathf.Abs(_bargage.position.z - center.z) );
		// Debug.Log(dist);
		if (dist < _length*0.75f) {
			_pillarDetector.OnUpdate();
		} else if (last_duduct_item != ""){
			last_duduct_item = "";
		}
	}

	public void OnRaycastDetectObject() {
		Debug.Log("Bargage is being detect");
		Vector3 bargageBottomPos = _bargage.GetComponent<Renderer>().bounds.min;
		
		//Check if bargage is out from the x,z range of pillar
		if (top_right.x - top_left.x >= _length -0.2f) {
			if ( bargageBottomPos.z < bt_left.z || bargageBottomPos.z > top_right.z) {
				Debug.Log("Horizontal Fail game, out of x,z range");
				return;
			}
		} else {
			if (bargageBottomPos.x > top_right.x || bargageBottomPos.x < top_left.x){
				Debug.Log("Verticle Fail game, out of x,z range");
				return;
			}
		}

		// //Check if bargage is out from y axis, but still within x,z range
		if (bargageBottomPos.y > _height * 2) {
			if (last_duduct_item != DeductionStandard.Constraint.over_object_height)
				MainGameManager.DeductScore(DeductionStandard.Constraint.over_object_height);

			Debug.Log("Deduct 30 points, out of y range");
			return;
		}
		
		if (bargageBottomPos.y < _height) {
			if (last_duduct_item != DeductionStandard.Constraint.below_object_height)
				MainGameManager.DeductScore(DeductionStandard.Constraint.below_object_height);
			Debug.Log("Below netwall");
			return;
		}
		
		hasPassed = true;
	}

	public override void OnCollisionHandler(Collision collision, Transform p_transform) {
		Debug.Log("From : "+p_transform.name +", Collider : " +collision.gameObject.name);
		Rigidbody colliderRigid = collision.gameObject.GetComponent<Rigidbody>();

		float hitPower = colliderRigid.velocity.magnitude;
		if (hitPower > pillarObject.slight_collision_velocity && hitPower < pillarObject.strong_collision_velocity) {
			MainGameManager.DeductScore(DeductionStandard.Constraint.slight_impact);
		} else if (hitPower >= pillarObject.strong_collision_velocity) {
			//Give rigidbody to pillar
			Rigidbody pillarRigid = p_transform.gameObject.GetComponent<Rigidbody>();
			if (pillarRigid == null) pillarRigid = p_transform.gameObject.AddComponent<Rigidbody>();
			pillarRigid.AddForce( (colliderRigid.velocity * 5));

			MainGameManager.DeductScore(DeductionStandard.Constraint.knock_down);
		}
	}

	public void ReorganizedPillarPosition() {
		//Define TopLeft
			List<Vector3> tops = corners.OrderByDescending(x => x.z).ToList();
			top_left = (tops[0].x > tops[1].x) ? tops[1] : tops[0];
			top_right = (tops[0].x > tops[1].x) ? tops[0] : tops[1];

			bt_left = (tops[2].x > tops[3].x) ? tops[2] : tops[3];
			bt_right = (tops[2].x > tops[3].x) ? tops[3] : tops[2];
	}

	#endregion
}
