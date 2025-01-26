using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class PlayerPush : MonoBehaviour
{
    public const float PUSH_RAY_VERTICAL_OFFSET = 0.25f;
    public const float PUSH_RAY_ALT_VERTICAL_OFFSET = 0.85f;

	public const int PUSHABLE_MASK = (1 << 3);


	public Camera playerCamera;
    Inputs input;

    void Awake()
    {
        if(this.playerCamera == null)
        {
            this.playerCamera = Camera.main;
        }
        this.input = GetComponent<Inputs>();
    }

    // Update is called once per frame
    void Update()
    {
        var inputVector = this.input.move;
        if(inputVector != Vector2.zero)
        {
            UpdateDesiredPush(inputVector);
        }
    }

    void UpdateDesiredPush(Vector2 inputVector)
    {
        Vector2 inputVec = inputVector;

		var worldRight = ProjectHorizontal(this.playerCamera.transform.right);
		var worldForward = ProjectHorizontal(this.playerCamera.transform.forward);

		inputVec.x = SnapInput(inputVec.x);
        inputVec.y = SnapInput(inputVec.y);

        if (inputVec == Vector2.zero) return;

        if(inputVec.magnitude > 1f)
        {
            inputVec = inputVec.normalized;
        }

        Vector3 worldInput = inputVec.x * worldRight + inputVec.y * worldForward;

        if(!TryMatchWorldAxis(worldInput, out Vector3 worldSnappedInput))
        {
            return;
        }

        var rayOrigin = this.transform.position;
        var rayOrigin2 = rayOrigin;
        rayOrigin.y += PUSH_RAY_VERTICAL_OFFSET;
        rayOrigin2.y += PUSH_RAY_ALT_VERTICAL_OFFSET;

        Debug.DrawRay(rayOrigin, worldSnappedInput, Color.red);

        CastPushRay(rayOrigin, worldSnappedInput);
        CastPushRay(rayOrigin2, worldSnappedInput);
    }

    static readonly Vector3[] VALID_PUSH_VECTORS =
    {
        new Vector3(1f, 0f, 0f),
        new Vector3(-1f, 0f, 0f),
        new Vector3(0f, 0f, 1f),
        new Vector3(0f, 0f, -1f),
    };

    bool TryMatchWorldAxis(Vector3 input, out Vector3 worldAxisInput)
    {
        float best = -1000f;
        Vector3 bestVec = Vector3.forward;
        foreach(var v in VALID_PUSH_VECTORS)
        {
            float dot = Vector3.Dot(input, v);
			if (dot > best)
            {
                best = dot;
                bestVec = v;
            }
        }
        worldAxisInput = bestVec;
        return true;
    }

    void CastPushRay(Vector3 origin, Vector3 direction)
    {
		if (Physics.Raycast(origin, direction, out var hitInfo, 1f, PUSHABLE_MASK))
		{
			var pushable = hitInfo.collider.GetComponent<PushableBox>();
			pushable.WantsToPush(direction);
		}
	}

    static Vector3 ProjectHorizontal(Vector3 direction)
    {
        direction.y = 0f;
        return direction.normalized;
    }

    static float SnapInput(float axis)
    {
        if(axis > 0.3f)
        {
            return 1f;
        }
        else if(axis < -0.3f)
        {
            return -1f;
        }
        return 0f;
    }
}
