using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LeaderboardButtons  : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Button Hover Text:")] 
    [SerializeField] private TMPro.TextMeshProUGUI originalText;
    private TransitionEffect transitionEffect;

    private Color originalColor;

    private void Start()
    {
        originalColor = originalText.color;
        transitionEffect = FindObjectOfType<TransitionEffect>();
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
        Time.timeScale = 1;
        transitionEffect.LoadScene("MainMenu");
    }
}
