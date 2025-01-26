using System;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class PingEffect : MonoBehaviour
{
    public AudioSource audio;
    public AudioClip sound;
    public float maxSize;

    private void Start()
    {
        var meshRenderer = GetComponent<MeshRenderer>();
        audio.clip = sound;
        audio.Play();
        DOTween.Sequence()
            .Append(transform.DOScale(maxSize, 1).SetEase(Ease.OutSine))
            .Join(meshRenderer.material.DOFade(0, 0.4f).SetDelay(0.6f))
            .AppendCallback(() => Destroy(gameObject));
    }

    public void OnTriggerEnter(Collider other)
    {
        var interactable = other.GetComponent<Interactable>();
        if(interactable)
            interactable.Ping();
    }
}
