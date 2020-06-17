using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffetLoot : MonoBehaviour
{
    public void LooterNourriture(float montant)
    {
        ControleSouris.Actuel.tribuControlee.stockRessources.RessourcesEnStock.AugmenterGain("Nourriture", montant);
    }
    public void LooterPierre(float montant)
    {
        ControleSouris.Actuel.tribuControlee.stockRessources.RessourcesEnStock.AugmenterGain("Pierre", montant);
    }
    public void LooterPeau(float montant)
    {
        ControleSouris.Actuel.tribuControlee.stockRessources.RessourcesEnStock.AugmenterGain("Peau", montant);
    }
    public void LooterPigment(float montant)
    {
        ControleSouris.Actuel.tribuControlee.stockRessources.RessourcesEnStock.AugmenterGain("Pigment", montant);
    }
    public void LooterOutil(float montant)
    {
        ControleSouris.Actuel.tribuControlee.stockRessources.RessourcesEnStock.AugmenterGain("Outil", montant);
    }
}
