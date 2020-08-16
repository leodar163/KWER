using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[CreateAssetMenu(fileName = "NvlEvenementCombat", menuName = "Evénements/Evément de Combat")]
public class EvenementCombat : Evenement
{
    [HideInInspector] public Combat combat;

    [HideInInspector] public string baliseGuer = "<guer>";
    [HideInInspector] public string baliseGuerPourc = "<guer%>";

    [HideInInspector] public string baliseEnnPourc = "<enn%>";
    [HideInInspector] public string baliseEnn = "<enn>";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fuire()
    {

    }

    #region EFFETS COMBAT
    public void TuerGuerrier(int nombre)
    {
        for (int i = 0; i < CaperArgumentInt(nombre, false); i++)
        {
            combat.Guerrier.tribu.demographie.DesengagerGuerrier(true);
        }
    }

    public void FaireFuirGuerrier(int nombre)
    {
        for (int i = 0; i < CaperArgumentInt(nombre, false); i++)
        {
            combat.Guerrier.tribu.demographie.DesengagerGuerrier(false);
        }
    }

    public void TuerGuerrierPourcentage(float pourcentage)
    {
        for (int i = 0; i < CaperArgumentPourcentage(pourcentage, false); i++)
        {
            combat.Guerrier.tribu.demographie.DesengagerGuerrier(true);
        }
    }

    public void FaireFuirGuerrierPourcentage(float pourcentage)
    {
        for (int i = 0; i < CaperArgumentPourcentage(pourcentage, false); i++)
        {
            combat.Guerrier.tribu.demographie.DesengagerGuerrier(false);
        }
    }

    public void TuerEnnemis(int nombre)
    {
        combat.Hostile.nbrCombattant -= CaperArgumentInt(nombre, true);
    }

    public void TuerEnnemisPourcentage(float pourcentage)
    {
        combat.Hostile.nbrCombattant -= CaperArgumentPourcentage(pourcentage, true);
    }

    public void LancerCombat()
    {
        combat.LancerCombat();
    }

    public void LooterEnnemi()
    {
        combat.looterEnnemi();
    }
    #endregion

    private int CaperArgumentInt(int argument, bool ennemi)
    {
        int nombre = argument;
        if (ennemi)
        {
            Hostile hostile = combat.Hostile;
            if (nombre < 0) nombre = math.abs(nombre);
            else if (nombre > hostile.nbrCombattant) nombre = hostile.nbrCombattant;
            
        }
        else
        {
            Guerrier guerrer = combat.Guerrier;
            if (nombre < 0) nombre = math.abs(nombre);
            else if (nombre > guerrer.nbrGuerrier) nombre = guerrer.nbrGuerrier;
        }

        return nombre;
    }

    private int CaperArgumentPourcentage(float argument, bool ennemi)
    {
        float pourcentage = argument;
        if(ennemi)
        {
            Hostile hostile = combat.Hostile;
            if (pourcentage < 0) pourcentage = 0;
            else if (pourcentage > 100) pourcentage = 100;
            return (int)math.round((pourcentage * hostile.nbrCombattant) / 100);
        }
        else
        {
            Guerrier guerrier = combat.Guerrier;
            if (pourcentage < 0) pourcentage = 0;
            else if (pourcentage > 100) pourcentage = 100;
            return (int)math.round((pourcentage * guerrier.nbrGuerrier) / 100);
        }
    }

    protected override string ModifsRetourEffets(string retourEffet)
    {
        string contenu = "";
        string retour = retourEffet;

        //ennemi flat
        if(RecupererContenuBalise(retourEffet,baliseEnn) != null)
        {
            contenu = CaperArgumentInt(int.Parse(RecupererContenuBalise(retourEffet, baliseEnn)), true).ToString();
            retour = RemplacerContenuBalises(retour, baliseEnn, contenu);
            retour = SupprimerBalises(retour, baliseEnn);
        }
        // pourcentage ennemi
        else if(RecupererContenuBalise(retourEffet, baliseEnnPourc) != null)
        {
            contenu = CaperArgumentPourcentage(float.Parse(RecupererContenuBalise(retour, baliseEnnPourc)), true).ToString();
            retour = RemplacerContenuBalises(retour, baliseEnnPourc, contenu);
            retour = SupprimerBalises(retour, baliseEnnPourc);
        }
        //guerrier flat
        else if(RecupererContenuBalise(retourEffet, baliseGuer) != null)
        {
            Debug.Log(retour);
            contenu = CaperArgumentInt(int.Parse(RecupererContenuBalise(retourEffet, baliseGuer)), false).ToString();
            retour = RemplacerContenuBalises(retour, baliseGuer, contenu);
            Debug.Log(retour);
            retour = SupprimerBalises(retour, baliseGuer);
            Debug.Log(retour);
        }
        //guerrier pourcentage
        else if(RecupererContenuBalise(retourEffet, baliseGuerPourc) != null)
        {
            contenu = CaperArgumentPourcentage(float.Parse(RecupererContenuBalise(retour, baliseGuerPourc)), false).ToString();
            retour = RemplacerContenuBalises(retour, baliseGuerPourc, contenu);
            retour = SupprimerBalises(retour, baliseGuerPourc);
        }
        

        return retour;
    }
}
