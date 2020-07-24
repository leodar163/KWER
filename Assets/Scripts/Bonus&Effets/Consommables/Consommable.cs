using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NvConsommable", menuName = "Bonus & Effets/Consommable")]
public class Consommable : ScriptableObject
{
    [HideInInspector] public Amenagement amenagement;
    [HideInInspector] public Buff buff;

    [TextArea]
    public string texteInfobulle;
    [Space]
    public Sprite icone;
    [Space]
    public typeConsommable type;

    public enum typeConsommable {amenagement = 0, buff =  1}

    
}
