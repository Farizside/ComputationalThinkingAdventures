using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpecialDroppable : MonoBehaviour, IDropHandler
{
    public bool isCable;

    public Sprite cross;
    public Sprite straight;
    
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

            if (isCable)
            {
                if (draggableObject.name == "Kabel Cross")
                {
                    gameObject.GetComponent<Image>().sprite = cross;
                    draggableObject.parentAfterDrag = transform;
                    dropped.GetComponent<Image>().enabled = false;
                }else if (draggableObject.name == "Kabel Straight")
                {
                    gameObject.GetComponent<Image>().sprite = straight;
                    draggableObject.parentAfterDrag = transform;
                    dropped.GetComponent<Image>().enabled = false;
                }
            }
            else
            {
                if (draggableObject.name == "Kabel Cross" || draggableObject.name == "Kabel Straight" ) return;
                draggableObject.parentAfterDrag = transform;
            }
        }
    }
}
