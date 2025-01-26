using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PingToPlaySimonSaysSequence : Interactable
{
    public List<AudioClip> soundSequence;
    public GameObject targetExpander;
    public AudioClip pingSound;
    public AudioClip sequenceSuccess;
    public AudioClip sequenceFail;
    
    private bool isPlaying;

    public override void Ping()
    {
        if(isPlaying || SimonSaysManager.completed)
            return;

        isPlaying = true;
        transform.DOPunchScale(Vector3.one * 0.1f, 0.2f, 20);
        var sequence = DOTween.Sequence();
        
        foreach(var clip in soundSequence)
        {
            sequence.AppendInterval(0.8f)
                .AppendCallback(() => PlaySound(clip))
                .AppendInterval(clip.length);
        }

        sequence.AppendCallback(() => isPlaying = false);
    }

    public void PlaySuccess()
    {
        DOTween.Sequence()
            .AppendCallback(() => PlaySound(sequenceSuccess))
            .AppendInterval(sequenceSuccess.length)
            .AppendCallback(() => targetExpander.SetActive(true));
    }

    public void PlayFail()
    {
        PlaySound(sequenceFail);
    }
}