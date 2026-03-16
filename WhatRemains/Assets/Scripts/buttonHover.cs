using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class buttonHover : MonoBehaviour
{
    public TMP_Text text;
    public Color normalColour = Color.white;
    public Color hoverColour = Color.red;

    public void OnPointerEnter(PointerEventData eventData)
    {
        text.color = hoverColour;
    }

    public void OnPointExit(PointerEventData eventData)
    {
        text.color = normalColour; 
    }
}
