using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableBox : MonoBehaviour
{
    const int PUSH_COLLISION_LAYER_MASK = 1 | (1 << 3);

    const float OVERLAP_VERTICAL_OFFSET = 0.4f;
    const float OVERLAP_VERTICAL_SIZE = 0.1f;
    const float OVERLAP_HORIZONTAL_SIZE = 0.4f;

    bool isTryingToPush;

    Vector3 pushDirection;
    float pushAmount = 0f;

    void Update()
    {
        if(this.isTryingToPush)
        {
            if(this.pushAmount > 1f)
            {
                TryPush();
                this.pushAmount = 0f;
            }
        }
        else
        {
            this.pushAmount = Mathf.MoveTowards(this.pushAmount, 0f, Time.deltaTime * 4f);
        }

        this.isTryingToPush = false;
    }

    void TryPush()
    {
        Vector3 pushTarget = this.transform.position;

        pushTarget.x = Mathf.Round(pushTarget.x + this.pushDirection.x);
        pushTarget.z = Mathf.Round(pushTarget.z + this.pushDirection.z);

        var overlapTarget = pushTarget;
        overlapTarget.y += OVERLAP_VERTICAL_OFFSET;
        var halfExtents = new Vector3(OVERLAP_HORIZONTAL_SIZE, OVERLAP_VERTICAL_SIZE, OVERLAP_HORIZONTAL_SIZE);

        if(Physics.CheckBox(overlapTarget, halfExtents, Quaternion.identity, PUSH_COLLISION_LAYER_MASK))
        {
            PushBlocked();
        }
        else
        {
            DoPush(pushTarget);
        }
    }

    void PushBlocked()
    {
        Debug.Log("PUSH WAS BLOCKED");
    }

    void DoPush(Vector3 target)
    {
		this.transform.position = target;
	}

	public void WantsToPush(Vector3 direction)
    {
        this.pushAmount += Time.deltaTime * 2f;
		this.isTryingToPush = true;
        this.pushDirection = direction;

	}
}
