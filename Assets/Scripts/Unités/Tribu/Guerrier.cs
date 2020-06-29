using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Guerrier : MonoBehaviour
{
    public Tribu tribu;
    public int nbrGuerrier = 0;

    public int attaque = 1;
    public int defense = 1;

    public int attaqueTotale
    {
        get
        {
            return attaque + tribu.bonus.attaqueBonus;
        }
    }

    public int defenseTotale
    {
        get
        {
            return defense + tribu.bonus.defenseBonus;
        }
    }
}
