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
        TribuKiSubit.bonus.bonusMultCoutPop = montant;
    }

    public void AssignerMutliplicateurStockage(float montant)
    {
        TribuKiSubit.bonus.bonusMultStockage = montant;
    }

    public void AssignerMultProd(float montant)
    {
        TribuKiSubit.bonus.bonusMultProd = montant;
    }

    public void AssignerMultProdNourriture(float montant)
    {
        TribuKiSubit.bonus.bonusMultProdNourriture = montant;
    }

    public void AssignerMultProdPierre(float montant)
    {
        TribuKiSubit.bonus.bonusMultProdPierre = montant;
    }

    public void AssignerMultProdPeau(float montant)
    {
        TribuKiSubit.bonus.bonusMultProdPeau = montant;
    }

    public void AssignerMultProdOutil(float montant)
    {
        TribuKiSubit.bonus.bonusMultProdOutil = montant;
    }

    public void AssignerMultProdPigment(float montant)
    {
        TribuKiSubit.bonus.bonusMultProdPigment = montant;
    }

    public void AssignerMultProdToutesTribus(float montant)
    {
        foreach (BonusTribu bonus in FindObjectsOfType<BonusTribu>())
        {
            bonus.bonusMultProd = montant;
        }
    }

    public void AssignerMultProdNourritureToutesTribus(float montant)
    {
        foreach (BonusTribu bonus in FindObjectsOfType<BonusTribu>())
        {
            bonus.bonusMultProdNourriture = montant;
        }
    }

    public void AssignerMultProdPierreToutesTribus(float montant)
    {
        foreach (BonusTribu bonus in FindObjectsOfType<BonusTribu>())
        {
            bonus.bonusMultProdPigment = montant;
        }
    }

    public void AssignerMultProdPeauToutesTribus(float montant)
    {
        foreach (BonusTribu bonus in FindObjectsOfType<BonusTribu>())
        {
            bonus.bonusMultProdPeau = montant;
        }
    }

    public void AssignerMultProdOutilToutesTribus(float montant)
    {
        foreach (BonusTribu bonus in FindObjectsOfType<BonusTribu>())
        {
            bonus.bonusMultProdOutil = montant;
        }
    }

    public void AssignerMultProdPigmentToutesTribus(float montant)
    {
        foreach (BonusTribu bonus in FindObjectsOfType<BonusTribu>())
        {
            bonus.bonusMultProdPigment = montant;
        }
    }

    public void AjouterBonusDeplacement(int montant)
    {
        TribuKiSubit.bonus.bonusPointDeplacement += montant;
    }

    public void AjouterBonusDeplacementToutesTribus(int montant)
    {
        foreach (BonusTribu bonus in FindObjectsOfType<BonusTribu>())
        {
            bonus.bonusPointDeplacement += montant;
        }
    }
}
