using DG.Tweening;
using StarterAssets;
using UnityEngine;

public class Expander : Interactable
{
    public AudioClip pickupSound;
    public AudioClip pingSound;
    
    private bool isAnimating;

    public override void Touch()
    {
        if(isAnimating)
            return;

        isAnimating = true;
        PlaySound(pickupSound);
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
