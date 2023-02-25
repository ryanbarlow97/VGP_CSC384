using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class MainMenuButtons : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Color originalColor;


    [Header("Button Hover Text:")] 
    [SerializeField] private TMPro.TextMeshProUGUI originalText;

    private void Start()
    {
        originalColor = originalText.color;
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        originalText.color = Color.white;
    }
        public void OnPointerExit(PointerEventData eventData)
    {
        originalText.color = originalColor;
    }

    public void Play() 
    {
        SceneManager.LoadScene("MainGame");
    }

    public void Leaderboards() 
    {
        SceneManager.LoadScene("Leaderboards");
    }

    public void Options() 
    {
        SceneManager.LoadScene("Options");
    }

    public void Quit() 
    {
        Application.Quit();
    }

}
