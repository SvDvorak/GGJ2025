using DG.Tweening;
using StarterAssets;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Pickup : Interactable
{
    private AudioSource pickupSound;
    private bool isAnimating;

    private void Awake()
    {
        pickupSound = GetComponent<AudioSource>();
    }

    public override void Touch()
    {
        if(isAnimating)
            return;

        isAnimating = true;
        pickupSound.Play();
        DOTween.Sequence()
            .Append(transform.DOScale(0, 0.15f).SetEase(Ease.InBounce))
            .AppendCallback(() => BubbleLevels.Instance.ExpandToNextLevel());
    }

    public override void Ping()
    {
        if(isAnimating)
            return;

        isAnimating = true;
        var position = PlayerController.Instance.transform.position;
        var pickupPosition = transform.position;
        var fromPlayer = (pickupPosition - position).normalized;
        DOTween.Sequence()
            .Append(transform.DOMove(pickupPosition + fromPlayer * 0.1f, 0.1f).SetEase(Ease.InBounce))
            .Append(transform.DOMove(pickupPosition, 0.1f).SetEase(Ease.OutBounce))
            .AppendCallback(() => isAnimating = false);
    }
}
