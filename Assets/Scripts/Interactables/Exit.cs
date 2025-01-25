using DG.Tweening;
using StarterAssets;
using UnityEngine.SceneManagement;

public class Exit : Interactable
{
    public override void Touch()
    {
        DOTween.Sequence()
            .Append(MovePlayerOut())
            .AppendCallback(() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1));
    }

    private Tween MovePlayerOut()
    {
        PlayerController.Instance.enabled = false;
        return PlayerController.Instance.transform.DOMoveY(20, 1).SetEase(Ease.InSine);
    }
}