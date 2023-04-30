using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class OptionsButtons : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Button Hover Text:")] 
    [SerializeField] private TMPro.TextMeshProUGUI originalText;

    private Color originalColor;

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

    public void Back() 
    {
        SceneManager.LoadScene("MainMenu");
    }

}
