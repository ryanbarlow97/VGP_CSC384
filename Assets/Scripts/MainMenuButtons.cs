using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class MainMenuButtons : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{


    [Header("Button Hover Text:")] 
    [SerializeField] private TMPro.TextMeshProUGUI originalText;




    public void OnPointerEnter(PointerEventData eventData)
    {
        originalText.color = Color.white;
    }
        public void OnPointerExit(PointerEventData eventData)
    {
        originalText.color = new Color32(255, 255, 255, 150);
    }

    public void Play() 
    {

    }

    public void Leaderboards() 
    {
        SceneManager.LoadScene("Leaderboard");
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
