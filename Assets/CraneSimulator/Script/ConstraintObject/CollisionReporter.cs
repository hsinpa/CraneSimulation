using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionReporter : MonoBehaviour {
    BaseWorldTransform _baseCharacter;
    public bool isBreakable = false;

    private int collision_cold_down = 1;
    private float cold_down_time = 0;

    void Start() {
        _baseCharacter = GetComponentInParent<BaseWorldTransform>();
    }

    public bool IsBreakUp() {
        return (_baseCharacter.transform.Find(transform.name) == null);
    }

    //Break from parent.tranfrom, to outer layer
    public void SeperateFromMainBody() {
        transform.SetParent(_baseCharacter.transform.parent);

        Rigidbody2D itemRigid = GetComponent<Rigidbody2D>();
        if (!itemRigid) gameObject.AddComponent<Rigidbody2D>();

        Collider2D boxCollider =  GetComponent<Collider2D>();
        if (boxCollider) boxCollider.enabled = true;
    }

	void OnCollisionEnter(Collision collision) {        
        if (cold_down_time > Time.time) return;
        Debug.Log("Layer " + collision.gameObject.layer);
        if (collision.gameObject.layer != General.interactableLayer) return;
        // if (IsBreakUp()) return;

        cold_down_time = Time.time + collision_cold_down;
         _baseCharacter.OnCollisionHandler(collision, transform);
    }
}
