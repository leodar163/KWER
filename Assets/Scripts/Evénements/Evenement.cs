using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEditor.Events;
using UnityEditor;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;
using System;

[CreateAssetMenu(fileName = "NvlEvenement", menuName = "Evénements/Evément")]
public class Evenement : ScriptableObject
{
    [SerializeField] private GameObject effets;

    [Space]
    public string titre;
    [TextArea]
    public string description;
    [Space]
    public Sprite illustration;
    [Space]
    public List<Choix> listeChoix = new List<Choix>();

    [Serializable]
    public struct Choix
    {
        public string description;
        [TextArea]
        public string infobulle;
        [HideInInspector] public List<string> retoursEffets;

        public UnityEvent effets;
        
        public Choix(string descriptionChoix, string texteInfoBulle)
        {
            description = descriptionChoix;
            infobulle = texteInfoBulle;
            effets = new UnityEvent();
            retoursEffets = new List<string>();
        }
    }

    protected virtual void OnValidate()
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

    public string InfoBulleComplete(int index)
    {
        Choix choix = listeChoix[index];
        string infobulleComplete = choix.infobulle;

        for (int j = 0; j < choix.retoursEffets.Count; j++)
        {
            if (choix.retoursEffets[j] != "" && choix.retoursEffets[j] != null)
            {
                infobulleComplete += "\n" + choix.retoursEffets[j];
            }
        }
        return infobulleComplete;
    }
}
