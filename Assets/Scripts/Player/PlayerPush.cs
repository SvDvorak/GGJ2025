using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class PlayerPush : MonoBehaviour
{
    public const float PUSH_RAY_VERTICAL_OFFSET = 0.25f;
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
        Vector2 snappedInput = inputVector;

        snappedInput.x = SnapInput(snappedInput.x);
        snappedInput.y = SnapInput(snappedInput.y);

        if (snappedInput == Vector2.zero) return;

        if (Mathf.Abs(snappedInput.x) > 0f && Mathf.Abs(snappedInput.y) > 0f)
        {
            // Only allow pushing in a single axis
            return;
        }

        var worldRight = ProjectHorizontal(this.playerCamera.transform.right);
        var worldForward = ProjectHorizontal(this.playerCamera.transform.forward);

        Vector3 worldInput = worldRight * snappedInput.x + worldForward * snappedInput.y;

        var rayOrigin = this.transform.position;
        rayOrigin.y += PUSH_RAY_VERTICAL_OFFSET;

        Debug.DrawRay(rayOrigin, worldInput, Color.red);

        if(Physics.Raycast(rayOrigin, worldInput, out var hitInfo, 1f, PUSHABLE_MASK))
        {
            var pushable = hitInfo.collider.GetComponent<PushableBox>();
            pushable.WantsToPush(worldInput);
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
