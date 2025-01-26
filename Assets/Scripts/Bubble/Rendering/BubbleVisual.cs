using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class BubbleVisual : MonoBehaviour
{
    private MeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void Update()
    {
        meshRenderer.material.SetVector("_ViewDir", (transform.position - Camera.main.transform.position).normalized);
    }
}