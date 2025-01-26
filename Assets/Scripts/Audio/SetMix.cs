using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SetMix : MonoBehaviour
{
    public AudioMixerSnapshot snapshot;

	// Start is called before the first frame update
	void Start()
    {
        snapshot.TransitionTo(0f);
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
