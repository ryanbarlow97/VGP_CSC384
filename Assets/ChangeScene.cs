using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public AudioSource src;
    
    public AudioClip srcOne;
    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        audioSource = GetComponent<AudioSource>();
    }
    public void changeToMainMenu()
    {

        src.PlayOneShot(srcOne);
        SceneManager.LoadScene("MainMenu");

    }
        public void changeToOptions()
    {
        SceneManager.LoadScene("Options");
    }
        public void changeToLeaderboards()
    {
        SceneManager.LoadScene("Leaderboard");
    }

}
