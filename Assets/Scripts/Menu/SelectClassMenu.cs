using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using UnityEngine;

public class SelectClassMenu : MonoBehaviour
{
    [SerializeField] private RunData _runData;

    [Header("UiElements")] 
    [SerializeField] private GameObject _informationPanel;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    
    [Header("Buttons")]
    [SerializeField] private GameObject _startButton;
    
    private GameObject _lastClassType = null;

    // Start is called before the first frame update
    void Start()
    {
        _runData.ClassType = null;
        _startButton.SetActive(false);
        _informationPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (_runData.ClassType != null && (_lastClassType == null || _runData.ClassType != _lastClassType.gameObject))
        {
            _lastClassType = _runData.ClassType;
            
            _startButton.SetActive(true);
            _informationPanel.SetActive(true);
            
            _nameText.text = _lastClassType.GetComponent<ClassType>().Name;
            _descriptionText.text = _lastClassType.GetComponent<ClassType>().Description;
        }
    }

    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Playtest");
    }
}