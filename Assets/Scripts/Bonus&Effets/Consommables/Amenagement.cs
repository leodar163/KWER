using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "NvlAmenagement", menuName = "Economie/Amenagement")]
public class Amenagement : ScriptableObject
{
    public static UnityEvent eventAmenagement = new UnityEvent();
    [HideInInspector]public List<string> terrainsAmenageables = new List<string>();

    [HideInInspector]public int Slots;

    [HideInInspector] public int palier;

    [HideInInspector] public List<Sprite> spritesEte = new List<Sprite>();
    [HideInInspector] public List<Sprite> spritesHiver = new List<Sprite>();

    [HideInInspector] public Production gainAmenagementEte;
    [HideInInspector] public Production gainAmenagementHiver;

    [HideInInspector] public string Effets;

    public void AmenagerTuile(TuileManager tuile)
    {
        tuile.tuileAmenagement.AmenagerTuile(this);
    }

    public Amenagement Cloner()
    {
        Amenagement clone = CreateInstance<Amenagement>();

        clone.terrainsAmenageables = terrainsAmenageables;
        clone.Slots = Slots;
        clone.palier = palier;
        clone.spritesEte = new List<Sprite>(spritesEte);
        clone.spritesHiver = new List<Sprite>(spritesHiver);
        clone.gainAmenagementEte = gainAmenagementEte;
        clone.Effets = Effets;

        return clone;
    }
}
