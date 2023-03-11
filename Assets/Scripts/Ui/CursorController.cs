using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorController : MonoBehaviour
{
    [SerializeField] private RunData _runData;
    
    [Header("Ui")]
    [SerializeField] private GameObject _outsideCircleFill;
    [SerializeField] private GameObject _insideCircleFill;
    
    private Image _outsideCircleFillSpriteRenderer;
    private Image _insideCircleFillSpriteRenderer;
    
    private WeaponsController _weaponsController;

    // Start is called before the first frame update
    void Start()
    {
        _outsideCircleFillSpriteRenderer = _outsideCircleFill.GetComponent<Image>();
        _insideCircleFillSpriteRenderer = _insideCircleFill.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        //TODO: Get if the special ability is ready to display outside circle
        
        if (_weaponsController != null)
        {
            _insideCircleFillSpriteRenderer.fillAmount = 1f - _weaponsController.GetWeaponCooldownPercentage();
        }
        
        // Update the position of the cursor to the mouse position
        Vector2 mousePosition = Input.mousePosition;
        transform.position = mousePosition;
        
        Cursor.visible = false;
    }
    
    public void SetWeaponsController(WeaponsController weaponsController)
    {
        _weaponsController = weaponsController;
    }
}
