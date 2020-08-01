using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "NvlAmenagement", menuName = "Economie/Amenagement")]
public class Amenagement : ScriptableObject
{
    public List<TuileTerrain> terrainsAmenageables = new List<TuileTerrain>();

    public int Slots;

    public int palier;

    public List<Sprite> spritesEte = new List<Sprite>();
    public List<Sprite> spritesHiver = new List<Sprite>();

    public Production gainAmenagementEte;
    public Production gainAmenagementHiver;

    public string texteInfobulle;

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
        clone.texteInfobulle = texteInfobulle;

        return clone;
    }
}
