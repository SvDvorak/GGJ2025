using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class LayeredAudio : MonoBehaviour
{
    const float TRANSITION_TIME = 1.5f;
    const float DEFAULT_TRANSITION_TIME = 0.5f;
    const float AUTO_TRANSITION_DELAY = DEFAULT_TRANSITION_TIME + 3f;
    const float AUTO_TRANSITION_TIME = 4f;

    public AudioClip[] musicLayers;

    public AudioMixerSnapshot fadeInSnapshot;
    public AudioMixerSnapshot defaultSnapshot;
    public AudioMixerSnapshot[] layeredSnapshots;
    public AudioMixerSnapshot transitionLevelSnapshot;

    public AudioMixerGroup sfxDefaultMixerGroup;

    bool isChangingScene = false;

    AudioSource[] sources;

    // Start is called before the first frame update
    void Awake()
    {
        this.sources = GetComponentsInChildren<AudioSource>();

        for(int i = 0; i < this.musicLayers.Length; i++)
        {
            this.sources[i].clip = this.musicLayers[i];
        }

        AutoAssignExistingAudioSourceGroups();
    }

    void AutoAssignExistingAudioSourceGroups()
    {
        var allAudioSources = FindObjectsOfType<AudioSource>(true);

        foreach(var source in allAudioSources)
        {
            // Assume any unassigned audio sources should be mixed as SFX
            if (source.outputAudioMixerGroup == null)
            {
                source.outputAudioMixerGroup = this.sfxDefaultMixerGroup;
            }
        }
    }

    private void Start()
    {
        double scheduledPlayTime = AudioSettings.dspTime + 1f;
        foreach (var s in this.sources)
        {
            s.PlayScheduled(scheduledPlayTime);
        }

    }

    public void OnLevelStart()
    {
		this.fadeInSnapshot.TransitionTo(1f);
	}

    public void OnFadeIn()
    {
        StartCoroutine(FadeInRoutine());
    }

	IEnumerator FadeInRoutine()
    {
		this.defaultSnapshot.TransitionTo(3.5f);

		yield return new WaitForSeconds(4f);
        this.layeredSnapshots[0].TransitionTo(4f);
    }

    public void TransitionToLayer(int layerIndex)
    {
        if (this.isChangingScene) return;

        StopAllCoroutines();

        if(layerIndex > this.layeredSnapshots.Length)
        {
            Debug.LogError($"Layer {layerIndex} not available");
            return;
        }

        this.layeredSnapshots[layerIndex].TransitionTo(TRANSITION_TIME);
    }

    public void TransitionToNextScene()
    {
        StopAllCoroutines();
        this.isChangingScene = true;

		transitionLevelSnapshot.TransitionTo(TRANSITION_TIME);
	}
}
