using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.Events;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "NvlEvenementCombat", menuName = "Evénements/Evément de Combat")]
public class EvenementCombat : Evenement
{
    [HideInInspector] public Combat combat;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Combattre()
    {
    }

    public void Fuire()
    {

    }

    public void TuerGuerrier(int nombre)
    {
        Guerrier guerrier = combat.Guerrier;
        if (nombre < 0) nombre = math.abs(nombre);
        if (nombre > guerrier.nbrGuerrier) nombre = guerrier.nbrGuerrier;

        for (int i = 0; i < nombre; i++)
        {
            guerrier.tribu.demographie.DesengagerGuerrier(true);
        }
    }

    public void FaireFuirGuerrier(int nombre)
    {
        Guerrier guerrier = combat.Guerrier;
        if (nombre < 0) nombre = math.abs(nombre);
        if (nombre > guerrier.nbrGuerrier) nombre = guerrier.nbrGuerrier;

        for (int i = 0; i < nombre; i++)
        {
            guerrier.tribu.demographie.DesengagerGuerrier(false);
        }
    }
}
