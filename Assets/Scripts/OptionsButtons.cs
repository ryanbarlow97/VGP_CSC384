using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class OptionsButtons : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
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

    public void Back() 
    {
        SceneManager.LoadScene("MainMenu");
    }

}
