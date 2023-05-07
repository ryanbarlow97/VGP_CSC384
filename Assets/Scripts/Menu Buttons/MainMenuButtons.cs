using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class MainMenuButtons : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Color originalColor;
    private TransitionEffect transitionEffect;

    [Header("Button Hover Text:")] 
    [SerializeField] private TMPro.TextMeshProUGUI originalText;

    private void Start()
    {
        originalColor = originalText.color;
        transitionEffect  = FindObjectOfType<TransitionEffect>();
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
        transitionEffect.LoadScene("SaveSlots");
    }
    public void Replays() 
    {
        transitionEffect.LoadScene("GameReplay");
    }
    public void Leaderboards() 
    {
        transitionEffect.LoadScene("Leaderboards");
    }

    public void Options() 
    {
        transitionEffect.LoadScene("Options");
    }

    public void Quit() 
    {
        Application.Quit();
    }

}
