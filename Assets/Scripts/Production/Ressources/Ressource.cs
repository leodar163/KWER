    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "nvlleRessource", menuName = "Economie/Ressource")]
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
    [TextArea(10,20)]
    public string texteInfobulle;
}
