using UnityEngine;
using UnityEngine.EventSystems;

public class TouchZone : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public static bool touch;
    public void OnPointerDown(PointerEventData eventData)
    {
        touch = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        touch = false;
    }
}
