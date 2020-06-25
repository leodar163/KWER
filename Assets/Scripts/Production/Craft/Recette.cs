using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "nvlleRecette", menuName = "Economie/Recette")]
public class Recette : ScriptableObject
{
    public string nom;
    public int slots;
    public Production production;
    public Production cout;


}
