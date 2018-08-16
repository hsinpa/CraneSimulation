using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseWorldTransform : MonoBehaviour{
    public abstract void SetUp(Transform p_bargage);
    public abstract void OnCollisionHandler(Collision collision, Transform transform);
    public abstract void BuildObject();

    private float record_time;
    protected float append_time = 5;
    public bool hasPassed = false;

    public float xPos = 0;
    
    protected bool CheckTimer() {
        //Reject if still in cool down
        if (record_time > Time.time) return false;
        record_time = Time.time + append_time;

        return true;
    }

    protected IEnumerator ToNextCameraPosition() {
        yield return new WaitForSeconds(1.5f);

        if (hasPassed && xPos != 0) {
            MainGameManager.Instance.ChangeCameraXPosition(xPos);
        }
    }

    protected void DeductScore(string key, bool repeatable = false)
    {

        if (!hasPassed || repeatable)
        {
            MainGameManager.DeductScore(key);
        }

        int sibingIndex = transform.GetSiblingIndex();
        if (sibingIndex > 0 && !transform.parent.GetChild(sibingIndex - 1).GetComponent<BaseWorldTransform>().hasPassed)
            return;

        MoveCamera();
    }

    protected void MoveCamera()
    {
        if (hasPassed) return;
        hasPassed = true;
        StartCoroutine(ToNextCameraPosition());
    }

}
