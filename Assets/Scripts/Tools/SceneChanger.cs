using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
	[Min(0)]
	public float TimeToTransition;
	public Animator animator;
    public AudioEvent StartGameSFX;

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
	
    public void Exit()
    {
        Application.Quit();
    }

    public void ResetScene(float delay = 0)
    {
		StartCoroutine(ChangeScene(SceneManager.GetActiveScene().buildIndex, delay));
    }

	IEnumerator ChangeScene(int levelIndex, float delay = 0)
	{
        yield return new WaitForSeconds(delay);
		animator.SetTrigger("FadeIn");
		yield return new WaitForSeconds(TimeToTransition);
		SceneManager.LoadScene(levelIndex);
	}

    private void Start() {
        StartGameSFX.Play(GetComponent<AudioSource>());
    }
}
