using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootstepAudio : MonoBehaviour
{
    const float FOOSTEP_TRAVEL_DISTANCE = 0.5f;

    const float PITCH_MIN = 1.2f;
    const float PITCH_MAX = 1.4f;

    const float VOLUME_MIN = 0.5f;
    const float VOLUME_MAX = 1f;

	public AudioClip[] clips;
    public AudioSource audio;

    int lastIndex = 0;

    Vector3 lastPos;
    float accDistance;

    void Start()
    {
        lastPos = this.transform.position;
    }

    void PlayFootstep()
    {
        lastIndex = (lastIndex + Random.Range(0, clips.Length - 1)) % clips.Length;

        //this.audio.Stop();
        
        this.audio.pitch = Random.Range(PITCH_MIN, PITCH_MAX);
        this.audio.volume = Random.Range(VOLUME_MIN, VOLUME_MAX);
        
        this.audio.PlayOneShot(clips[lastIndex]);
    }

    void Update()
    {
        var pos = this.transform.position;

        var delta = pos - lastPos;

		lastPos = pos;

		delta.y = 0f;
        accDistance += delta.magnitude;

        if(accDistance > FOOSTEP_TRAVEL_DISTANCE)
        {
            accDistance = accDistance % FOOSTEP_TRAVEL_DISTANCE;
            PlayFootstep();
		}
        

	}
}
