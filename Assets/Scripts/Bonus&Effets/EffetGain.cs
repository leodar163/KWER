using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffetGain : MonoBehaviour
{
    public void GainNourriture(float montant)
    {
        ControleSouris.Actuel.tribuControlee.stockRessources.EncaisserRessource("Nourriture", montant);
    }
    public void GainPierre(float montant)
    {
        ControleSouris.Actuel.tribuControlee.stockRessources.EncaisserRessource("Pierre", montant);
    }
    public void GainPeau(float montant)
    {
        ControleSouris.Actuel.tribuControlee.stockRessources.EncaisserRessource("Peau", montant);
    }
    public void GainPigment(float montant)
    {
        ControleSouris.Actuel.tribuControlee.stockRessources.EncaisserRessource("Pigment", montant);
    }
    public void GainOutil(float montant)
    {
        ControleSouris.Actuel.tribuControlee.stockRessources.EncaisserRessource("Outil", montant);
    }
}
