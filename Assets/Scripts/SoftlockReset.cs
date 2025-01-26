using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SoftlockReset : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
			var lt = FindObjectOfType<LevelTransition>();
            if(lt == null)
            {
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
			}
			else
			{
                lt.TransitionToScene(SceneManager.GetActiveScene().buildIndex);
			}
		}
	}
}
