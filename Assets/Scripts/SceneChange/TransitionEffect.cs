using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TransitionEffect : MonoBehaviour
{
    public Animator transitionAnim;

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    IEnumerator LoadSceneAsync(string sceneName)
    {
        transitionAnim.SetTrigger("Start");
        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(sceneName);
    }
}
