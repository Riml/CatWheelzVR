using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class joystick : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IDragHandler
{
    private Image bgimg;
    private Image joystickimg;
    private Vector3 inputvector;
    public void Start()
    {
        bgimg = GetComponent<Image>();
        joystickimg = transform.GetChild(0).GetComponent<Image>();
    }
    public virtual void OnDrag(PointerEventData ped)
    {
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(bgimg.rectTransform, ped.position, ped.pressEventCamera, out pos))
        {
            pos.x = (pos.x / bgimg.rectTransform.sizeDelta.x);
            pos.y = (pos.y / bgimg.rectTransform.sizeDelta.y);

            inputvector = new Vector3( pos.x * 2 + 1,0, pos.y * 2 - 1);
            inputvector = (inputvector.magnitude > 1.0f) ? inputvector.normalized : inputvector;

            joystickimg.rectTransform.anchoredPosition = new Vector3
                (inputvector.x * (bgimg.rectTransform.sizeDelta.x / 3),
                inputvector.z * (bgimg.rectTransform.sizeDelta.y / 3));
            
        }
    }
    public virtual void OnPointerDown(PointerEventData ped)
    {
        OnDrag(ped);

    }
    public virtual void OnPointerUp(PointerEventData ped)
    {
       inputvector = Vector3.zero;
        joystickimg.rectTransform.anchoredPosition = Vector3.zero;
    }
  
    public float Horizontal()
    {
        if (inputvector.x != 0)
        {
            return inputvector.x;
        }
        else return Input.GetAxis("Horizontal");
    }
    public float Vertical()
    {
        if (inputvector.z != 0)

        { return inputvector.z; }
        else return Input.GetAxis("Vertical");
    }

}
