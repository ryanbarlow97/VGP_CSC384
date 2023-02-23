using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{


    [Header("Button Hover Text:")] 
    [SerializeField] private TMPro.TextMeshProUGUI originalText;

    [Header("Button Hover Sound:")] 
    [SerializeField] private AudioSource hoverSound;
    public void OnPointerEnter(PointerEventData eventData)
    {
        originalText.color = Color.white;
        hoverSound.Play();

    }
        public void OnPointerExit(PointerEventData eventData)
    {
        originalText.color = new Color32(255, 255, 255, 150);

    }
}
