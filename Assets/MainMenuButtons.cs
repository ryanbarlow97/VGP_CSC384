using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class MainMenuButtons : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{


    [Header("Button Hover Text:")] 
    [SerializeField] private TMPro.TextMeshProUGUI originalText;

    [Header("Button Hover Sound:")] 
    [SerializeField] private AudioSource buttonSound;
    [SerializeField] private AudioClip clickSound;


    public void OnPointerEnter(PointerEventData eventData)
    {
        originalText.color = Color.white;
    }
        public void OnPointerExit(PointerEventData eventData)
    {
        originalText.color = new Color32(255, 255, 255, 150);
    }

        public void ClickSound()
    {
        buttonSound.PlayOneShot(clickSound);
    }
}
