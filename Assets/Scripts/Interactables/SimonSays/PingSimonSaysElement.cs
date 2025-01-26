using System;
using DG.Tweening;
using UnityEngine;

public class PingSimonSaysElement : Interactable
{
    public AudioClip clip;
    public event Action<PingSimonSaysElement> Pinged;
    
    public override void Ping()
    {
        if(SimonSaysManager.completed)
            return;
        
        PlaySound(clip);
        DOTween.Sequence()
            .AppendInterval(clip.length)
            .AppendCallback(() => Pinged?.Invoke(this));
    }
}