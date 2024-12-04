using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [HideInInspector] public Transform parentAfterDrag;
    [SerializeField] public bool isDuplicatedOnDrag = false;

    private Image _image;
    private GameObject _duplicateObject;

    private void Start()
    {
        _image = GetComponent<Image>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isDuplicatedOnDrag)
        {
            _duplicateObject = Instantiate(gameObject, transform.parent);
            _duplicateObject.name = gameObject.name;
        }
        else
        {
            parentAfterDrag = transform.parent;
        }

        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        _image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDrag);
        _image.raycastTarget = true;
        isDuplicatedOnDrag = false;
        
        if (transform.parent == null)
        {
            Destroy(gameObject);
        }
    }
}