using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
using UnityEditor.Events;
using UnityEditor;

[CreateAssetMenu(fileName = "NvlEvenement", menuName = "Evénements/Evément")]
public class Evenement : ScriptableObject
{
    [SerializeField] private GameObject effets;

    [Space]
    public string titre;
    public string description;
    [Space]
    public Sprite illustration;
    [Space]
    public List<Choix> listeChoix = new List<Choix>();

    [Serializable]
    public struct Choix
    {
        public string description;
        public string infobulle;

        public UnityEvent effets;

        public Choix(string descriptionChoix, string texteInfoBulle)
        {
            description = descriptionChoix;
            infobulle = texteInfoBulle;
            effets = new UnityEvent();
        }
    }

    private void OnValidate()
    {
        foreach(Choix choix in listeChoix)
        {
            if (choix.effets.GetPersistentEventCount() == 0)
            {    
                UnityEventTools.AddPersistentListener(choix.effets, FermerFenetreEvenement);
            }
        }
    }

    public void FermerFenetreEvenement()
    {
        if (InterfaceEvenement.Defaut)
        {
            InterfaceEvenement.Defaut.FermerFenetreEvenement();
        }
    }

    public void LancerEvenement()
    {
        if (InterfaceEvenement.Defaut)
        {
            InterfaceEvenement.Defaut.OuvrirFenetreEvenement(this);
        }
    }

    public void Sauvegarder()
    {
        AssetDatabase.Refresh();
        EditorUtility.SetDirty(this);
        AssetDatabase.SaveAssets();
    }
}
