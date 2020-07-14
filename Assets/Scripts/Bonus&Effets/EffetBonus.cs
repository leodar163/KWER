﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffetBonus : MonoBehaviour
{

    public void ajouterBonusAttaque(int montant)
    {
        Tribu.tribuQuiJoue.bonus.attaqueBonus += montant;
    }

    public void ajouterBonusAttaque(int montant, Tribu tribu)
    {
        tribu.bonus.attaqueBonus += montant;
    }

    public void ajouterBonusDefense(int montant)
    {
        Tribu.tribuQuiJoue.bonus.defenseBonus += montant;
    }

    public void ajouterBonusDefense(int montant, Tribu tribu)
    {
        tribu.bonus.defenseBonus += montant;
    }
}