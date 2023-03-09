using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Relic : MonoBehaviour
{
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private Rarity _rarity;

    public string Name => _name;
    public string Description => _description;
    public Rarity Rarity => _rarity;
}
