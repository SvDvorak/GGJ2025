using UnityEngine;

public class BubbleMask : MonoBehaviour
{
    public BubbleChildShading bubbleShading;

    private void Awake()
    {
        GetComponent<MeshRenderer>().enabled = false;
    }

    void Update()
    {
        bubbleShading.UpdateBubble(transform.position, transform.localScale.x / 2);
    }
}