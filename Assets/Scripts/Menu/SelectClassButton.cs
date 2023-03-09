using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectClassButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textButton;
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private RunData _runData;
    
    [Header("To Set")]
    [SerializeField] private GameObject _classTypePrefab;

    private bool _hovered;
    private GameObject _classTypeSpawned;
    
    private Animator _animator;
    
    private static readonly int Running = Animator.StringToHash("Running");

    // Start is called before the first frame update
    void Start()
    {
        // Spawn the classtype prefab above the button
        Vector2 positionToSpawn = Camera.main.ScreenToWorldPoint(_rectTransform.position);
        _classTypeSpawned = Instantiate(_classTypePrefab, positionToSpawn, Quaternion.identity);
        
        // Set the name of the button to the name of the class
        _textButton.text = _classTypePrefab.GetComponent<ClassType>().Name;
        
        _animator = _classTypeSpawned.GetComponent<Animator>();
        _classTypeSpawned.GetComponent<MovementController>().enabled = false;
        _classTypeSpawned.transform.localScale = new Vector3(2f, 2f, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        if (_runData.ClassType != _classTypePrefab)
        {
            _animator.SetBool(Running, false);
        }
    }
    
    public void OnClick()
    {
        _runData.ClassType = _classTypePrefab;
        
        _animator.SetBool(Running, true);
    }
}
