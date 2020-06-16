using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Diagnostics;

[ExecuteInEditMode]
[CreateAssetMenu(fileName = "NvlChoix", menuName = "Evénements/Choix")]
public class Choix : ScriptableObject
{
    public string description;
    public string infobulle;

    public UnityEvent effets = new UnityEvent();

    private void Awake()
    {

        if (effets.GetPersistentEventCount() == 0)
        {
            UnityEditor.Events.UnityEventTools.AddPersistentListener(effets, InterfaceEvenement.Defaut.FermerFenetreEvenement);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
