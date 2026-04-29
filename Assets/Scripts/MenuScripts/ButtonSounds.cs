using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonSounds : MonoBehaviour, IPointerEnterHandler, IMoveHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        AudioManager.PlaySFX(SoundType.UISelect);
    }

    public void OnMove(AxisEventData eventData)
    {
        AudioManager.PlaySFX(SoundType.UISelect);
    }
}
