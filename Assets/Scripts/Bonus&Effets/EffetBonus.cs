using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EffetBonus : MonoBehaviour
{
    private Tribu tribuKiSubit;

    public Tribu TribuKiSubit
    {
        get
        {
            if (tribuKiSubit == null) return Tribu.TribukiJoue;
            else return tribuKiSubit;
        }
        set
        {
            tribuKiSubit = value;
        }
    }
    public void ajouterBonusAttaque(int montant)
    {
        TribuKiSubit.bonus.bonusAttaque += montant;
    }

    public void ajouterBonusDefense(int montant)
    {
        TribuKiSubit.bonus.bonusDefense += montant;
    }

    public void AjouterBonusDegatMoral(int montant)
    {
        if (montant > 0) montant = montant + TribuKiSubit.guerrier.degatMoralTotal > 100 ? 100 - tribuKiSubit.guerrier.degatMoralTotal : montant;
        else if (montant < 0) montant = montant + TribuKiSubit.guerrier.degatMoralTotal < 0 ? TribuKiSubit.guerrier.degatMoralTotal : montant;
        tribuKiSubit.bonus.bonusDegatMoral += montant;
    }

    public void AssignerMultiplicateurCoutPop(float montant)
    {
        tribuKiSubit.bonus.bonusMultCoutPop = montant;
    }

    public void AssignerMutliplicateurStockage(float montant)
    {
        tribuKiSubit.bonus.bonusMultStockage = montant;
    }
}
