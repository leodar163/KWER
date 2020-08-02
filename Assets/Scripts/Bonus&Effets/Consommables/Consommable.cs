﻿using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

[CreateAssetMenu(fileName = "NvConsommable", menuName = "Bonus & Effets/Consommable")]
public class Consommable : ScriptableObject
{
    [HideInInspector] public Amenagement amenagement;
    [HideInInspector] public Buff buff;

    [TextArea]
    [SerializeField] private string texteInfoBulle;
    [HideInInspector] public string texteRetour;
    public string TexteInfoBulle
    {
        get
        {
            string retour = texteInfoBulle;
            retour += texteInfoBulle.Length > 0 ? '\n' + texteRetour : texteRetour;
            return texteInfoBulle + '\n' +texteRetour;
        }
    }
    [Space]
    public Sprite icone;
    [Space]
    public typeConsommable type;

    public enum typeConsommable {amenagement = 0, buff =  1}

    
}
