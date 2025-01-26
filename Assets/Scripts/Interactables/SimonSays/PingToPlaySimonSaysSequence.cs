using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PingToPlaySimonSaysSequence : Interactable
{
    public List<AudioClip> soundSequence;
    public GameObject targetExpander;
    public AudioClip pingSound;
    public AudioClip sequenceSuccess;
    
    private bool isPlaying;

    public override void Ping()
    {
        if(isPlaying || SimonSaysManager.Instance.completed)
            return;

        isPlaying = true;
        transform.DOPunchScale(Vector3.one * 0.1f, 0.2f, 20);
        var sequence = DOTween.Sequence();

        for(var i = 0; i < soundSequence.Count; i++)
        {
            var clip = soundSequence[i];
            var index = i;
            sequence.AppendInterval(0.8f)
                .AppendCallback(() => PlaySound(clip))
                .JoinCallback(() => SimonSaysManager.Instance.PlayGlow(index))
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
}