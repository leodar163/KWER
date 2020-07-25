using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "nvlleRecette", menuName = "Economie/Recette")]
public class Recette : ScriptableObject
{
    public string nom;
    public int slots;


    public enum TypeOutput { Ressources, Consommable }
    [Space]
    public TypeOutput typeOutput;
    
    [Space]
    [HideInInspector] public Consommable consommable;
    [HideInInspector] public Production production;
    [Space]
    public Production inputParPop;
    [HideInInspector] public Production cout;

    

    
}
