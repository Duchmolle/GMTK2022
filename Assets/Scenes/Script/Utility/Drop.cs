using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drop : MonoBehaviour, IDropHandler
{
    private RectTransform rectTransform;
    private Vector2 rectStartPos;

    [SerializeField] GameObject dropDownMenu;

    public Movement.Direction slotDirection;

    public bool isOccupied;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if(transform.childCount > 0)
        {
            isOccupied = true;
        }
        else
        {
            isOccupied = false;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        //Debug.Log("OnDrop");
        //Debug.Log(rectTransform.anchoredPosition);
        if(eventData.pointerDrag != null)
        {
            // on enregistre la position initial de l'ancre du slot avant l'opération;
            rectStartPos = rectTransform.anchoredPosition;
            // on assigne le dé comme étant enfant du slot;
            eventData.pointerDrag.gameObject.transform.parent = rectTransform;
            // on change la position de l'ancre du slot avant d'assigner la position de l'ancre du slot à l'ancre du dé
            rectTransform.anchoredPosition = new Vector2(50f, -50f);
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = rectTransform.anchoredPosition;

            // on redonne à l'ancre du slot sa position initial
            rectTransform.anchoredPosition = rectStartPos;
        }
    }

    public void OnChooseSelectionClicked(GameObject button)
    {
        dropDownMenu.transform.SetParent(button.transform.GetChild(0));
        dropDownMenu.transform.localPosition = new Vector3(0, -30, 0);
        dropDownMenu.SetActive(true);
    }

}
