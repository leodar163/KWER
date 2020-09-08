using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffetSysteme : MonoBehaviour
{
    public void QuitterJeu()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void CheckerGameOverGeneral()
    {
        if (InfoTribus.ListeOrdonneeDesTribus == null ||InfoTribus.ListeOrdonneeDesTribus.Length <= OptionsJeu.Defaut.nbrTribuGameOver )
        {
            InterfaceEvenement.Defaut.evenementGameoverGen.LancerEvenementImmediat();
        }
    }
}
