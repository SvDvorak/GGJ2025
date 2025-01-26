using System;
using DG.Tweening;
using StarterAssets;
using UnityEngine;

public class Expander : Interactable
{
    public AudioClip pickupSound;
    public AudioClip pingSound;
    
    private bool isAnimating;
    private Sequence idleSequence;

    private void Start()
    {
        var originalScale = transform.localScale;
        transform.localScale = Vector3.zero;
        idleSequence = DOTween.Sequence()
            .Append(transform.DOScale(originalScale, 1))
            .Append(transform.DOLocalMoveY(transform.localPosition.y + 0.2f, 2).SetEase(Ease.InOutSine)
                .SetLoops(int.MaxValue, LoopType.Yoyo))
            .Join(transform.DOBlendableLocalRotateBy(new Vector3(0, 180, 0), 2)
                .SetEase(Ease.Linear)
                .SetLoops(int.MaxValue, LoopType.Incremental));
    }

    public override void Touch()
    {
        if(isAnimating)
            return;

        isAnimating = true;
        PlaySound(pickupSound);
        idleSequence.Kill();
        DOTween.Sequence()
            .Append(transform.DOScale(0, 0.15f).SetEase(Ease.InBounce))
            .AppendCallback(() => BubbleLevels.Instance.ExpandToNextLevel());
    }

    public override void Ping()
    {
        if(isAnimating)
            return;

        isAnimating = true;
        PlaySound(pingSound);
        var position = PlayerController.Instance.transform.position;
        var pickupPosition = transform.position;
        var fromPlayer = (pickupPosition - position).normalized;
        DOTween.Sequence()
            .Append(transform.DOPunchPosition(fromPlayer * 0.1f, 0.2f, 20))
            .AppendCallback(() => isAnimating = false);
    }
}
