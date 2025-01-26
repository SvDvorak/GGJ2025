using DG.Tweening;
using TMPro;
using UnityEngine;

public class AreaHint : MonoBehaviour
{
    public TextMeshProUGUI HintText;

    private void Start()
    {
        HintText.DOFade(0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        HintText.DOFade(1, 1);
    }

    private void OnTriggerExit(Collider other)
    {
        HintText.DOFade(0, 1);
    }
}
