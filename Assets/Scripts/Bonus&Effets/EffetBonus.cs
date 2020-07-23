using System.Collections;
using System.Collections.Generic;
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
        TribuKiSubit.bonus.attaqueBonus += montant;
    }

    public void ajouterBonusDefense(int montant)
    {
        TribuKiSubit.bonus.defenseBonus += montant;
    }
}
