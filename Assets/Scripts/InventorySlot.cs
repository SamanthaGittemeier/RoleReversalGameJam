using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    [SerializeField]
    private TMP_Text _grabText;

    private void Start()
    {
        _grabText.gameObject.SetActive(false);
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            GameObject dropped = eventData.pointerDrag;
            DraggableCard draggableCard = dropped.GetComponent<DraggableCard>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Card")
        {
            collision.GetComponent<DraggableCard>().SetTransform(transform);
            collision.GetComponent<DraggableCard>().AssignText(_grabText);
        }
    }
}
