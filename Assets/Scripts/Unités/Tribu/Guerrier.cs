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

    [Tooltip("Pourcentage de chance que l'ennemi s'enfuit quand un des siens est tué. \nL'ennemi a une résistance morale qui soustrait cette valeur.")]
    [Range(0, 100)]
    public int degatsMoraux = 20;

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
