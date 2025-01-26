using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class ViewDirectionShading : MonoBehaviour
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

    public Tween FadeIn(float time) => meshRenderer.material.DOFade(1, time);
    public Tween FadeOut(float time) => meshRenderer.material.DOFade(0, time);
}