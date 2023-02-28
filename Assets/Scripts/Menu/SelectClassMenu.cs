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
    [SerializeField] private GameObject _mageButton;
    [SerializeField] private GameObject _slimeButton;
    [SerializeField] private GameObject _startButton;
    
    [Header("ClassTypes")]
    [SerializeField] private GameObject _mage;
    [SerializeField] private GameObject _slime;
    
    [Header("PlayerPosition")]
    [SerializeField] private Transform _playerSpawnPoint;
    
    private GameObject _currentPlayer;
    
    // Start is called before the first frame update
    void Start()
    {
        _startButton.SetActive(false);
        _informationPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (_runData.classType != null)
        {
            _startButton.SetActive(true);
            _informationPanel.SetActive(true);
            
            if (_runData.classType == _mage)
            {
                _slimeButton.SetActive(false);
            }
            else
            {
                _mageButton.SetActive(false);
            }
        }
    }
    
    public void SelectMage()
    {
        _runData.classType = _mage;
        
        _nameText.text = _mage.GetComponent<ClassType>().Name;
        _descriptionText.text = _mage.GetComponent<ClassType>().Description;
        
        // Remove old player if it exists and create new one with new class
    }
    
    public void SelectSlime()
    {
        _runData.classType = _slime;
        
        _nameText.text = _slime.GetComponent<ClassType>().Name;
        _descriptionText.text = _slime.GetComponent<ClassType>().Description;
    }
    
    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Playtest");
    }
}