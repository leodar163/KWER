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

    public void AssignerMutliplicateurProduction(float montant)
    {
        tribuKiSubit.bonus.bonusMultProd = montant;
    }

    public void AssignerMutliplicateurProductionNourriture(float montant)
    {
        tribuKiSubit.bonus.bonusMultProdNourriture = montant;
    }

    public void AssignerMutliplicateurProductionPierre(float montant)
    {
        tribuKiSubit.bonus.bonusMultProdPierre = montant;
    }

    public void AssignerMutliplicateurProductionPeau(float montant)
    {
        tribuKiSubit.bonus.bonusMultProdPeau = montant;
    }

    public void AssignerMutliplicateurProductionOutil(float montant)
    {
        tribuKiSubit.bonus.bonusMultProdOutil = montant;
    }

    public void AssignerMutliplicateurProductionPigment(float montant)
    {
        tribuKiSubit.bonus.bonusMultProdPigment = montant;
    }

    public void AjouterBonusDeplacement(int montant)
    {
        tribuKiSubit.bonus.bonusPointDeplacement += montant;
    }

    public void AjouterBonusDeplacementToutesTribus(int montant)
    {
        foreach (BonusTribu bonus in FindObjectsOfType<BonusTribu>())
        {
            tribuKiSubit.bonus.bonusPointDeplacement += montant;
        }
    }
}
