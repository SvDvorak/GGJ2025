using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBubble : MonoBehaviour
{
    public BubbleChildShading bubbleShading;

    // Update is called once per frame
    void Update()
    {
        this.bubbleShading.UpdateBubble(this.transform.position, this.transform.localScale.x / 2);
    }
}
