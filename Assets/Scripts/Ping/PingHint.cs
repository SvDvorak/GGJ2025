using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PingHint : MonoBehaviour
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
