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
    private SpriteRenderer _cardImage;

    [SerializeField]
    private int _cardID;
    [SerializeField]
    private int _roomHealth;

    private void Start()
    {
        _cardImage = gameObject.GetComponent<SpriteRenderer>();
        ChooseImage();
        _roomHealth = 5;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if the tag is palladin and the card type is the specific room then deal damage
        if (collision.tag == "Paladin" && _cardID == 0)
        {
            collision.GetComponent<Hero>().TakeDamage(_roomHealth);
            _roomHealth--;
        }
        if (collision.tag == "Rogue" && _cardID == 1)
        {
            collision.GetComponent<Hero>().TakeDamage(_roomHealth);
            _roomHealth--;
        }
        if (collision.tag == "Mage" && _cardID == 2)
        {
            collision.GetComponent<Hero>().TakeDamage(_roomHealth);
            _roomHealth--;
        }
        else if (collision.tag == "Palladin" && _cardID != 0 || collision.tag == "Rogue" && _cardID != 1 || collision.tag == "Mage" && _cardID != 2)
        {
            _roomHealth--;
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
