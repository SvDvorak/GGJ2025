using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PushableBox : MonoBehaviour
{
    const int PUSH_COLLISION_LAYER_MASK = 1 | (1 << 3) | (1 << 9);

    const float OVERLAP_VERTICAL_OFFSET = 0.6f;
    const float OVERLAP_VERTICAL_SIZE = 0.05f;
    const float OVERLAP_HORIZONTAL_SIZE = 0.4f;

    const float TERMINAL_FALL_SPEED = 10f;

    bool isTryingToPush;

    Vector3 pushDirection;
    float pushAmount = 0f;

    Collider collider;

	float ySpeed = 0f;
    bool isFalling;

	void Awake()
    {
        this.collider = GetComponent<Collider>();
    }

    void FixedUpdate()
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

        SnapToGround();
    }

    void SnapToGround()
    {
        var rayOrigin = this.transform.position;
        rayOrigin.y += OVERLAP_VERTICAL_OFFSET;

        this.isFalling = !Physics.Raycast(rayOrigin, Vector3.down, out var hitInfo, 1f, PUSH_COLLISION_LAYER_MASK);

        if(isFalling)
        {
            this.ySpeed = Mathf.Max(this.ySpeed - 10f * Time.deltaTime, -TERMINAL_FALL_SPEED);
            this.transform.position = this.transform.position + new Vector3(0f, this.ySpeed * Time.deltaTime, 0f);
        }
        else
        {
            this.ySpeed = 0f;
            this.transform.position = hitInfo.point;
		}
	}

    void TryPush()
    {
        Vector3 pushTarget = this.transform.position;

        pushTarget.x = Mathf.Round(pushTarget.x + this.pushDirection.x);
        pushTarget.z = Mathf.Round(pushTarget.z + this.pushDirection.z);

        var overlapTarget = pushTarget;
        overlapTarget.y += OVERLAP_VERTICAL_OFFSET;
        var halfExtents = new Vector3(OVERLAP_HORIZONTAL_SIZE, OVERLAP_VERTICAL_SIZE, OVERLAP_HORIZONTAL_SIZE);

        this.collider.enabled = false;
        bool isBlocked = Physics.CheckBox(overlapTarget, halfExtents, Quaternion.identity, PUSH_COLLISION_LAYER_MASK);
        this.collider.enabled = true;

		if (isBlocked)
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
        transform.DOMove(target, 0.5f);
	}

	public void WantsToPush(Vector3 direction)
    {
        this.pushAmount += Time.deltaTime * 2f;
		this.isTryingToPush = true;
        this.pushDirection = direction;

	}
}
