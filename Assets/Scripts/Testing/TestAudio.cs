using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAudio : MonoBehaviour
{
    LayeredAudio layeredAudio;

    private void Start()
    {
        this.layeredAudio = FindObjectOfType<LayeredAudio>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            layeredAudio.TransitionToLayer(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            layeredAudio.TransitionToLayer(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            layeredAudio.TransitionToLayer(2);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            layeredAudio.TransitionToLayer(3);
        }
    }
}
