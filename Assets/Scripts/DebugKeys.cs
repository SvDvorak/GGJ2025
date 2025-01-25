using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugKeys : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKey(KeyCode.E))
            Time.timeScale = 5;
        else if(Input.GetKey(KeyCode.Q))
            Time.timeScale = 0.2f;
        else
            Time.timeScale = 1;
    }
}
