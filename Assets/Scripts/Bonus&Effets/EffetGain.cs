using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffetGain : MonoBehaviour
{
    public void GainNourriture(float montant)
    {

        Tribu.TribukiJoue.stockRessources.EncaisserRessource("Nourriture", montant);
    }
    public void GainPierre(float montant)
    {
        Tribu.TribukiJoue.stockRessources.EncaisserRessource("Pierre", montant);
    }
    public void GainPeau(float montant)
    {
        Tribu.TribukiJoue.stockRessources.EncaisserRessource("Peau", montant);
    }
    public void GainPigment(float montant)
    {
        Tribu.TribukiJoue.stockRessources.EncaisserRessource("Pigment", montant);
    }
    public void GainOutil(float montant)
    {
        Tribu.TribukiJoue.stockRessources.EncaisserRessource("Outil", montant);
    }
}
