using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FenetreRecapCombat : FenetreEvenementCombat
{   
    [Space]
    [SerializeField] private TextMeshProUGUI attaqueGuerrier = null;
    [SerializeField] private TextMeshProUGUI defenseGuerrier = null;
    [Space]
    [SerializeField] private TextMeshProUGUI attaqueHostile = null;
    [SerializeField] private TextMeshProUGUI defenseHostile = null;
    [Space]
    [SerializeField] private TextMeshProUGUI mortsGuerrier = null;
    [SerializeField] private TextMeshProUGUI mortsHostile = null;

    // Update is called once per frame
    void Update()
    {
        
    }
}
