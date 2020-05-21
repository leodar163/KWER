using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "nvlleRessource", menuName = "Ressource")]
public class Ressource : ScriptableObject
{

    [HideInInspector]public string nom
    {
        get
        {
            return name;
        }
    }
    
    [HideInInspector]public Sprite icone;   
}
