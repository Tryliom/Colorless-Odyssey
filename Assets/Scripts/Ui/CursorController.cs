using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    [SerializeField] private RunData _runData;
    
    [Header("Ui")]
    [SerializeField] private GameObject _outsideCircleFill;
    [SerializeField] private GameObject _insideCircleFill;
    
    private SpriteRenderer _outsideCircleFillSpriteRenderer;
    private SpriteRenderer _insideCircleFillSpriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        _outsideCircleFillSpriteRenderer = _outsideCircleFill.GetComponent<SpriteRenderer>();
        _insideCircleFillSpriteRenderer = _insideCircleFill.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //TODO: Get if the special ability is ready to display outside circle
        
        //TODO: Get if one of the weapons is ready to display inside circle
        
        // Update the position of the cursor to the mouse position
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousePosition;
        
        Cursor.visible = false;
    }
}
