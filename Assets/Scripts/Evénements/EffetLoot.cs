using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffetLoot : MonoBehaviour
{
    public void LooterNourriture(float montant)
    {
        ControleSouris.Actuel.tribuControlee.stockRessources.EncaisserRessource("Nourriture", montant);
    }
    public void LooterPierre(float montant)
    {
        ControleSouris.Actuel.tribuControlee.stockRessources.EncaisserRessource("Pierre", montant);
    }
    public void LooterPeau(float montant)
    {
        ControleSouris.Actuel.tribuControlee.stockRessources.EncaisserRessource("Peau", montant);
    }
    public void LooterPigment(float montant)
    {
        ControleSouris.Actuel.tribuControlee.stockRessources.EncaisserRessource("Pigment", montant);
    }
    public void LooterOutil(float montant)
    {
        ControleSouris.Actuel.tribuControlee.stockRessources.EncaisserRessource("Outil", montant);
    }
}
