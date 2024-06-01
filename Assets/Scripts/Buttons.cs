using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Buttons : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private TextMeshProUGUI _playText;

    public void OnPointerEnter(PointerEventData eventData)
    {
        _playText.text = "Are you still thinking?";
        _playText.fontSize = 10;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _playText.text = "Play";
        _playText.fontSize = 18;
    }
}
