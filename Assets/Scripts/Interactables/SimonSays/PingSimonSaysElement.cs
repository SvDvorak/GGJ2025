using System;
using DG.Tweening;
using UnityEngine;

public class PingSimonSaysElement : Interactable
{
    public AudioClip clip;
    public event Action<PingSimonSaysElement> Pinged;
    
    public override void Ping()
    {
        if(SimonSaysManager.Instance.completed)
            return;
        
        transform.DOPunchScale(Vector3.one * 0.1f, 0.2f, 20);
        PlaySound(clip);
        DOTween.Sequence()
            .AppendInterval(1)
            .AppendCallback(() => Pinged?.Invoke(this));
    }
}