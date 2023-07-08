using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableCard : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    [SerializeField]
    private Image _image;

    [SerializeField]
    private Transform parentAfterDrag;

    [SerializeField]
    private Sprite[] _cardTypeSprite;

    [SerializeField]
    private Image _cardImage;

    [SerializeField]
    private int _cardID;

    private void Start()
    {
        _cardImage = gameObject.GetComponent<Image>();
        ChooseImage();
    }

    public void ChooseImage()
    {
        switch (_cardID)
        {
            case 0:
                _cardImage.sprite = _cardTypeSprite[_cardID];
                break;
            case 1:
                _cardImage.sprite = _cardTypeSprite[_cardID];
                break;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin drag");
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        _image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Dragging");
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End drag");
        transform.SetParent(parentAfterDrag);
        _image.raycastTarget = true;
    }
}
