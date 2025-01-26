using DG.Tweening;
using StarterAssets;
using UnityEngine.SceneManagement;

public class Exit : Interactable
{
    public override void Touch()
    {
        DOTween.Sequence()
            .Append(MovePlayerOut());
        //.AppendCallback(() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1));

        LevelTransition.OnLevelExitTriggered();
    }

    private Tween MovePlayerOut()
    {
        PlayerController.Instance.enabled = false;
        var playerTransform = PlayerController.Instance.transform;
        return DOTween.Sequence()
            .Append(playerTransform.DOMove(transform.position, 1))
            .Append(playerTransform.DOMoveY(20, 1).SetEase(Ease.InSine));
    }
}