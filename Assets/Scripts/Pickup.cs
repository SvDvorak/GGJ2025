using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Pickup : MonoBehaviour
{
    private AudioSource pickupSound;
    private bool isAnimating;

    private void Awake()
    {
        pickupSound = GetComponent<AudioSource>();
    }

    public void Trigger()
    {
        if(isAnimating)
            return;
        
        pickupSound.Play();
        DOTween.Sequence()
            .Append(transform.DOScale(0, 0.15f).SetEase(Ease.InBounce))
            .AppendCallback(() => BubbleLevels.Instance.ExpandToNextLevel());

        isAnimating = true;
    }
}
