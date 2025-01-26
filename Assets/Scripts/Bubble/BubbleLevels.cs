using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class BubbleLevels : MonoBehaviour
{
    public static BubbleLevels Instance;

    public Transform bubbleMask;
    public List<float> cameraSizes;
    public LayeredAudio layeredAudio;
    public AudioSource expandSound;
    
    private List<MeshRenderer> levels;
    private int currentLevel;
    private Vector3 cameraOffset;

    private void Awake()
    {
        cameraOffset = Camera.main.transform.position;
        Instance = this;
        currentLevel = 0;

        levels = GetComponentsInChildren<MeshRenderer>().ToList();
        foreach(var level in levels)
        {
            level.enabled = false;
        }
        
        UpdateMaskLevel(0);
    }

    public void ExpandToNextLevel()
    {
        currentLevel += 1;

        UpdateMaskLevel(2);
    }

    private void UpdateMaskLevel(float time)
    {
        var level = levels[currentLevel];
        bubbleMask.DOScale(level.transform.localScale, time).SetEase(Ease.OutBounce);
        bubbleMask.DOMove(level.transform.position, time).SetEase(Ease.InOutSine);
        Camera.main.DOOrthoSize(cameraSizes[currentLevel], time);
        Camera.main.transform.DOMove(level.transform.position + cameraOffset, time);
        layeredAudio.TransitionToLayer(currentLevel);
        expandSound.Play();
    }
}
