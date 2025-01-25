using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovablePlatform : MonoBehaviour
{

    Vector3 startPosition;
    Vector3 extendedPosition;

    public Vector3 extensionMovement;
    public float extensionTime = 1f;

    public bool startExtended = false;

    [System.NonSerialized] public bool isExtended;

    Vector3 tweenStart;
    Vector3 tweenEnd;
    float tweenEndTime;
    [System.NonSerialized] public bool isMoving;

    Rigidbody rigidbody;

    void Awake()
    {
        this.rigidbody = GetComponent<Rigidbody>();


		this.startPosition = this.transform.localPosition;
        this.extendedPosition = this.startPosition + this.extensionMovement;

        if(this.startExtended)
        {
            this.transform.localPosition = this.extendedPosition;
            this.isExtended = true;
        }
    }

	[ContextMenu("Toggle")]
	public void Toggle()
    {
        if(this.isExtended)
        {
            Retract();
        }
        else
        {
            Extend();
        }
    }

    [ContextMenu("Retract")]
    public void Retract()
    {
        TweenTo(this.startPosition);
        this.isExtended = false;
    }

	[ContextMenu("Extend")]
	public void Extend()
    {
        TweenTo(this.extendedPosition);
        this.isExtended = true;
    }

    void TweenTo(Vector3 localPosition)
    {
        this.tweenEndTime = Time.time + this.extensionTime;
        this.tweenStart = this.transform.localPosition;
        this.tweenEnd = localPosition;
        this.isMoving = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(this.isMoving)
        {
            if(Time.time > this.tweenEndTime)
            {
                this.isMoving = false;
                this.rigidbody.position = this.tweenEnd;
                return;
            }

            float t = 1f - ((this.tweenEndTime - Time.time) / this.extensionTime);
            float smoothT = Mathf.SmoothStep(0f, 1f, t);
            
            this.rigidbody.position = Vector3.Lerp(this.tweenStart, this.tweenEnd, smoothT);
        }
    }
}
