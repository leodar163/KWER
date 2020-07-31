using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "NvlAmenagement", menuName = "Bonus & Effets/Amenagement")]
public class Amenagement : ScriptableObject
{
    public List<TuileTerrain> terrainsAmenageables = new List<TuileTerrain>();

    public void AmenagerTuile(TuileManager tuile)
    {
        
    }
}
