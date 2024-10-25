using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Dropable : MonoBehaviour, IDropHandler
{
    public Type type;
    public enum Type
    {
        Question,
        Answer,
    }

    private RectTransform _rectTransform;
    private GridLayoutGroup _grid;

    private void Start()
    {
        if (type == Type.Question) return;
        
        _rectTransform = GetComponent<RectTransform>();
        _grid = GetComponent<GridLayoutGroup>();
        var rect = _rectTransform.rect;
        _grid.cellSize = new Vector2(rect.width, rect.height);
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            GameObject dropped = eventData.pointerDrag;
            Draggable draggableObject = dropped.GetComponent<Draggable>();
            
            draggableObject.parentAfterDrag = transform;
        }
    }
}
