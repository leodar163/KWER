using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor;
using Unity.Mathematics;

[CustomEditor(typeof(Evenement), true)]
public class EvenementEditor : Editor
{
    private Evenement evenement;
    int oih = 0;


    public override void OnInspectorGUI()
    {
        evenement = (Evenement)target;

        base.OnInspectorGUI();

        foreach (Evenement.Choix choix in evenement.listeChoix)
        {
            if (choix.effets.GetPersistentEventCount() == 0)
            {
                UnityEditor.Events.UnityEventTools.AddPersistentListener(choix.effets, evenement.FermerFenetreEvenement);
            }
        }

        EcrireEffets();


        GUILayout.Space(20);
        if(GUILayout.Button("SAUVEGARDER"))
        {
            Sauvegarder();
        }
    }
    private void Sauvegarder()
    {
        AssetDatabase.Refresh();
        EditorUtility.SetDirty(target);
        AssetDatabase.SaveAssets();
        EcrireEffets();
    }
    private void EcrireEffets()
    {
        SerializedProperty effet;
        SerializedProperty cible;
        SerializedProperty methode;
        SerializedProperty argument;

        SerializedObject so = new SerializedObject(evenement);

        if (oih == 0)
        {
            
            //SerializedProperty it = so.GetIterator();
            //while (it.Next(true))
            //{ // or NextVisible, also, the bool argument specifies whether to enter on children or not
            //    Debug.Log(it.propertyPath);
            //}
            oih++;
        }

        for (int i = 0; i < evenement.listeChoix.Count; i++)
        {
            effet = so.FindProperty("listeChoix.Array.data[" + i + "].effets.m_PersistentCalls.m_Calls.Array");
            Evenement.Choix choix = evenement.listeChoix[i];
            InitialiserListeRetour(choix.retoursEffets, effet.arraySize);

            for (int j = 0; j < effet.arraySize; j++)
            {
                cible = effet.FindPropertyRelative("data[" + j + "].m_Target");
                methode = effet.FindPropertyRelative("data[" + j + "].m_MethodName");
                argument = effet.FindPropertyRelative("data[" + j + "].m_Arguments.m_FloatArgument");
                string retour = "";
                EvenementCombat evenementCombat = (EvenementCombat)evenement;

                if (methode.stringValue.Contains("Gain"))
                {
                    retour = methode.stringValue.Remove(0, 4);

                    if (argument.floatValue > 0)
                    {

                        retour += "<color=#" + ColorUtility.ToHtmlStringRGBA(ListeCouleurs.Defaut.couleurTexteBonus) + ">+"
                            + argument.floatValue;
                    }
                    else if (argument.floatValue < 0)
                    {
                        retour += "<color=#" + ColorUtility.ToHtmlStringRGBA(ListeCouleurs.Defaut.couleurAlerteTexteInterface) + ">" 
                            + argument.floatValue;
                    }
                }
                else if (methode.stringValue.Contains("Fuir"))
                {
                    if (methode.stringValue.Contains("Pourcentage"))
                    {
                        retour += "<color=#" + ColorUtility.ToHtmlStringRGBA(ListeCouleurs.Defaut.couleurAlerteTexteInterface) + ">"
                            + evenementCombat.baliseGuerPourc + argument.intValue + evenement.finBalise + " guerriers s'enfuient.";
                    }
                    else
                    {
                        argument = effet.FindPropertyRelative("data[" + j + "].m_Arguments.m_IntArgument");
                        retour += "<color=#" + ColorUtility.ToHtmlStringRGBA(ListeCouleurs.Defaut.couleurAlerteTexteInterface) + ">"
                            + evenementCombat.baliseGuer + argument.intValue + evenement.finBalise + " guerriers s'enfuient.";
                    }
                }
                else if (methode.stringValue.Contains("Tuer"))
                {
                    if (methode.stringValue.Contains("Guerrier"))
                    {
                        if (methode.stringValue.Contains("Pourcentage"))
                        {
                            retour += "<color=#" + ColorUtility.ToHtmlStringRGBA(ListeCouleurs.Defaut.couleurAlerteTexteInterface) + ">"
                               + evenementCombat.baliseGuerPourc + argument.floatValue + evenement.finBalise + " guerriers tués";
                        }
                        else
                        {
                            argument = effet.FindPropertyRelative("data[" + j + "].m_Arguments.m_IntArgument");
                            retour += "<color=#" + ColorUtility.ToHtmlStringRGBA(ListeCouleurs.Defaut.couleurAlerteTexteInterface) + ">"
                                + evenementCombat.baliseGuer + argument.intValue + evenement.finBalise + " guerriers tués";
                        }
                    }
                    else if (methode.stringValue.Contains("Ennemis"))
                    {
                        if (methode.stringValue.Contains("Pourcentage"))
                        {

                            retour += "<color=#" + ColorUtility.ToHtmlStringRGBA(ListeCouleurs.Defaut.couleurTexteBonus) + ">"
                                + evenementCombat.baliseEnnPourc + argument.floatValue + evenement.finBalise + " ennemis meurent";
                        }
                        else
                        {
                            argument = effet.FindPropertyRelative("data[" + j + "].m_Arguments.m_IntArgument");
                            retour += "<color=#" + ColorUtility.ToHtmlStringRGBA(ListeCouleurs.Defaut.couleurTexteBonus) + ">"
                                + evenementCombat.baliseEnn + argument.intValue + evenement.finBalise + " ennemis meurent";
                        }
                    }
                }
                else if (methode.stringValue.Contains("Piocher"))
                {
                    int index = 0; 
                    retour = "";

                    ClusterEvenement cluster = (ClusterEvenement)cible.objectReferenceValue;
                    for (int g = 0; g < cluster.listeEvenements.Count; g++)
                    {
                        if (cluster.listeEvenements[g].descriptionRapide != "")
                        {
                            if (index > 0) retour += "\n";
                            if (cluster.listeEvenements[g].proba <= 25)
                            {
                                retour += " - <color=#" + ColorUtility.ToHtmlStringRGBA(ListeCouleurs.Defaut.couleurAlerteTexteInterface) + ">"
                               + "Faible chance " + "<color=\"white\">"
                               + cluster.listeEvenements[g].descriptionRapide;
                            }
                            else if (cluster.listeEvenements[g].proba > 25 && cluster.listeEvenements[g].proba <= 60)
                            {
                                retour += " - <color=#" + ColorUtility.ToHtmlStringRGBA(ListeCouleurs.Defaut.couleurAlerteMoyenne) + ">"
                               + "Chance moyenne " + "<color=\"white\">"
                               + cluster.listeEvenements[g].descriptionRapide;
                            }
                            else
                            {
                                retour += " - <color=#" + ColorUtility.ToHtmlStringRGBA(ListeCouleurs.Defaut.couleurTexteBonus) + ">"
                               + "Forte chance " + "<color=\"white\">"
                               + cluster.listeEvenements[g].descriptionRapide;
                            }
                            index++;
                        }
                    }
                }
                else if (cible.objectReferenceValue is Buff)
                {
                    Buff buff = (Buff)cible.objectReferenceValue;
                    retour = buff.Retours;
                }
                else if (methode.stringValue.Contains("Bonus"))
                {
                    if (methode.stringValue.Contains("Attaque"))
                    {
                        char pluriel = '\0';
                        if (math.abs(argument.intValue) != 1) pluriel = 's';

                        if(argument.intValue > 0)
                        {
                            retour = "<color=#" + ColorUtility.ToHtmlStringRGBA(ListeCouleurs.Defaut.couleurTexteBonus) +">+" 
                                + argument.intValue + "point"+ pluriel +" d'attaque ";
                        }
                        else if(argument.intValue < 0)
                        {
                            retour = "<color=#" + ColorUtility.ToHtmlStringRGBA(ListeCouleurs.Defaut.couleurAlerteTexteInterface) + ">"
                                + argument.intValue + "point" + pluriel + " d'attaque ";
                        }
                    }
                    else if (methode.stringValue.Contains("Defense"))
                    {
                        char pluriel = '\0';
                        if (Math.Abs(argument.intValue) != 1) pluriel = 's';

                        if (argument.intValue > 0)
                        {
                            retour = "<color=#" + ColorUtility.ToHtmlStringRGBA(ListeCouleurs.Defaut.couleurTexteBonus) + ">+"
                                + argument.intValue + "point" + pluriel + " de défense ";
                        }
                        else if (argument.intValue < 0)
                        {
                            retour = "<color=#" + ColorUtility.ToHtmlStringRGBA(ListeCouleurs.Defaut.couleurAlerteTexteInterface) + ">"
                                + argument.intValue + "point" + pluriel + " de défense ";
                        }
                    }
                }
                else
                {
                    continue;
                }
                choix.retoursEffets[j] = retour;
            }
        }
    }

    private void InitialiserListeRetour(List<string> retours, int ettendue)
    {
        int difference = ettendue - retours.Count;
        if(difference > 0)
        {
            for (int i = 0; i < retours.Count; i++)
            {
                retours[i] = "";
            }
            for (int i = 0; i < difference; i++)
            {
                retours.Add("");
            }
        }
        else if(difference <= 0)
        {
            for (int i = 0; i < retours.Count; i++)
            {
                retours[i] = "";
            }
        }
    }
}
