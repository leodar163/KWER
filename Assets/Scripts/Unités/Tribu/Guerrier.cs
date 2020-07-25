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
    public int degatMoral = 20;

    public bool jetonAttaque = true;

    public int attaqueTotale
    {
        get
        {
            return attaque + tribu.bonus.bonusAttaque;
        }
    }

    public int defenseTotale
    {
        get
        {
            return defense + tribu.bonus.bonusDefense;
        }
    }

    public int degatMoralTotal
    {
        get
        {
            return degatMoral + tribu.bonus.bonusDegatMoral;
        }
    }

    public void EngagementGeneral()
    {
        tribu.expedition.RappelerExpeditions();
        tribu.expedition.LancerExpeditions();
        for (int i = 0; i < tribu.demographie.listePopsCampement.Count; i++)
        {
            tribu.demographie.EngagerGuerrier();
        }
    }

    public void DesengagementGeneral()
    {
        for (int i = 0; i < nbrGuerrier; i++)
        {
            tribu.demographie.DesengagerGuerrier(false);
        }
    }
}
