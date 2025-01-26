using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelTransition : MonoBehaviour
{
    bool inProgress = false;

    public Image image;
    LayeredAudio audio;

    public AudioSource levelStart;
    public AudioSource levelEnd;

    void Start()
    {
        this.audio = FindObjectOfType<LayeredAudio>();
		StartCoroutine(FadeIn());
    }

    public void TransitionToScene(int sceneIndex)
    {
        if (this.inProgress) return;

        this.inProgress = true;

        StartCoroutine(SceneTransition(sceneIndex));
    }

    IEnumerator SceneTransition(int sceneIndex)
    {
		this.levelEnd.Play();
		this.audio?.TransitionToNextScene();

		yield return new WaitForSeconds(1.5f);
        image.CrossFadeAlpha(1f, 2f, false);

		yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(sceneIndex);
	}


	IEnumerator FadeIn()
    {
		this.audio?.OnLevelStart();
		yield return new WaitForSeconds(0.5f);
		this.levelStart.Play();


		yield return new WaitForSeconds(1f);
		this.audio?.OnFadeIn();

		image.CrossFadeAlpha(0f, 6f, false);
        
        yield return new WaitForSeconds(1f);
    }

    public static void OnLevelExitTriggered()
    {
        FindObjectOfType<LevelTransition>()?.TransitionToScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
