using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DirectionDropDownMenu : MonoBehaviour
{
    [SerializeField] Sprite[] arrowSprites;
    public void OnLeftDirectionPressed()
    {
        transform.parent.GetComponentInParent<Drop>().slotDirection = Movement.Direction.GAUCHE;
        transform.parent.GetComponent<Image>().sprite = arrowSprites[0];
    }

    public void OnRightDirectionPressed()
    {
        transform.parent.GetComponentInParent<Drop>().slotDirection = Movement.Direction.DROITE;
        transform.parent.GetComponent<Image>().sprite = arrowSprites[1];
    }

    public void OnUpDirectionPressed()
    {
        transform.parent.GetComponentInParent<Drop>().slotDirection = Movement.Direction.HAUT;
        transform.parent.GetComponent<Image>().sprite = arrowSprites[2];
    }

    public void OnDownDirectionPressed()
    {
        transform.parent.GetComponentInParent<Drop>().slotDirection = Movement.Direction.BAS;
        transform.parent.GetComponent<Image>().sprite = arrowSprites[3];
    }
}
