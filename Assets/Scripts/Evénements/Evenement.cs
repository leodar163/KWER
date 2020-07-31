using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using TMPro;

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

    [HideInInspector] public string finBalise = "<fin>";

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
        Tribu.TribukiJoue.interactionTribu.EntrerEnInteraction(false);
    }

    public string InfoBulleComplete(int index)
    {
        Choix choix = listeChoix[index];
        string infobulleComplete = choix.infobulle;

        for (int j = 0; j < choix.retoursEffets.Count; j++)
        {
            if (choix.retoursEffets[j] != "" && choix.retoursEffets[j] != null)
            {
                choix.retoursEffets[j] = ModifsRetourEffets(choix.retoursEffets[j]);
                infobulleComplete += "\n" + choix.retoursEffets[j];
            }
        }
        return infobulleComplete;
    }

    protected virtual string ModifsRetourEffets(string retourEffet)
    {
        string retour = retourEffet;
        return retour;
    }

    #region BALISES
    protected string RecupererContenuBalise(string contenant, string balise)
    {
        if (contenant.Contains(balise))
        {
            int indexDebut = contenant.IndexOf(balise) + balise.Length;
            int indexFin = contenant.IndexOf(finBalise);
            string contenu = contenant.Substring(indexDebut, indexFin - indexDebut);
            return contenu;
        }
        else return null;
    }

    protected string RemplacerContenuBalises(string texte, string balise, string remplacement)
    {
        if (texte.Contains(balise))
        {
            string retour = texte;
            int indexDebut = retour.IndexOf(balise) + balise.Length;
            int indexFin = retour.IndexOf(finBalise);
            retour = retour.Remove(indexDebut, indexFin - indexDebut);
            retour = retour.Insert(indexDebut, remplacement);
            return retour;
        }
        else return null;
    }

    protected string SupprimerBalises(string texte,string balise)
    {
        if (texte.Contains(balise))
        {
            string retour = texte;
            retour = retour.Replace(balise, "");
            retour = retour.Replace(finBalise,"");
            return retour;
        }
        else return null;
    }
    #endregion
}
