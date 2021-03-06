﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor;
using UnityEditor.Events;
using UnityEngine;
using UnityEngine.Events;

[CustomEditor(typeof(Buff), true)]
public class BuffEditor : Editor 
{
    [SerializeField] private GameObject effetRef;
    Buff buff;
    Color typeSelectionne = new Color(20, 20, 150);

    public override void OnInspectorGUI()
    {
        buff = (Buff)target;

        base.OnInspectorGUI();

        if(buff.compteurTour)
        {
            GUILayout.Space(10);
            GUILayout.Label("Nombre de tour que dure l'effet");
            buff.nombreTour = EditorGUILayout.IntField(buff.nombreTour);
            GUILayout.Space(30);
        }

        DessinerBoutonType();

        GUILayout.Space(30);

        if (GUILayout.Button("SAUVEGARDER"))
        {
            Sauvegarder();
        }

        EcrireEffets();

        buff.nombreTour = Math.Abs(buff.nombreTour);
    }

    private void DessinerBoutonType()
    {
        GUILayoutOption[] options = new GUILayoutOption[2] { GUILayout.Width(150), GUILayout.Height(30) };

        GUILayout.Label("Type de décompte");
        GUILayout.Space(10);

        ColorerSelection(buff.compteurTour);
        if (GUILayout.Button("Compteur de Tour", options))
        {
            buff.compteurTour = true;
            buff.tpsDuneTechno = false;
            buff.tpsDunEvent = false;
        }

        ColorerSelection(buff.tpsDunEvent);
        if (GUILayout.Button("Buff d'événement", options))
        {
            buff.compteurTour = false;
            buff.tpsDuneTechno = false;
            buff.tpsDunEvent = true;
        }

        ColorerSelection(buff.tpsDuneTechno);
        if (GUILayout.Button("Buff de technologie", options))
        {
            buff.compteurTour = false;
            buff.tpsDuneTechno = true;
            buff.tpsDunEvent = false;
        }
        ColorerSelection(false);
    }

    private void ColorerSelection(bool selectionne)
    {
        if (selectionne)
        {
            GUI.backgroundColor = typeSelectionne;
        }
        else
        {
            GUI.backgroundColor = Color.white;
        }
    }

    private void EcrireEffets()
    {
        SerializedProperty effet;
        SerializedProperty cible;
        SerializedProperty methode;
        SerializedProperty argumentFloat;
        SerializedProperty argumentInt;

        SerializedObject so = new SerializedObject(buff);

        effet = so.FindProperty("effets.m_PersistentCalls.m_Calls.Array");

        InitialiserListeRetour(buff.listeEffetsRetours, effet.arraySize);

        EffetBonus bonus = effetRef.GetComponent<EffetBonus>();
        for (int i = 0; i < effet.arraySize; i++)
        {
            cible = effet.FindPropertyRelative("data[" + i + "].m_Target");
            methode = effet.FindPropertyRelative("data[" + i + "].m_MethodName");
            argumentFloat = effet.FindPropertyRelative("data[" + i + "].m_Arguments.m_FloatArgument");
            argumentInt = effet.FindPropertyRelative("data[" + i + "].m_Arguments.m_IntArgument");
            string retour = "";

            //Remet à 0 le nombre de listener persitent
            for (int j = 0; j < buff.antiEffets.GetPersistentEventCount(); j++)
            {
                UnityEventTools.RemovePersistentListener(buff.antiEffets, j);
            }

            if (methode.stringValue.Contains("Bonus"))
            {
                if (methode.stringValue.Contains("Attaque"))
                {
                    char pluriel = '\0';
                    if (Math.Abs(argumentInt.intValue) != 1) pluriel = 's';
                    if (argumentInt.intValue > 0)
                    {
                        retour = "<color=#" + ColorUtility.ToHtmlStringRGBA(ListeCouleurs.Defaut.couleurTexteBonus) + ">+"
                            + argumentInt.intValue + "<color=\"white\">" + " point" + pluriel + " d'attaque";
                    }
                    else if (argumentInt.intValue < 0)
                    {
                        retour = "<color=#" + ColorUtility.ToHtmlStringRGBA(ListeCouleurs.Defaut.couleurAlerteTexteInterface) + ">"
                            + argumentInt.intValue + "<color=\"white\">" + " point" + pluriel + " d'attaque";
                    }

                    UnityAction<int> callback = new UnityAction<int>(bonus.ajouterBonusAttaque);
                    UnityEventTools.AddIntPersistentListener(buff.antiEffets, callback, argumentInt.intValue * -1);
                }
                else if (methode.stringValue.Contains("Defense"))
                {
                    char pluriel = '\0';
                    if (Math.Abs(argumentInt.intValue) != 1) pluriel = 's';
                    if (argumentInt.intValue > 0)
                    {
                        retour = "<color=#" + ColorUtility.ToHtmlStringRGBA(ListeCouleurs.Defaut.couleurTexteBonus) + ">+"
                            + argumentInt.intValue + "<color=\"white\">" + " point" + pluriel + " de défense";
                    }
                    else if (argumentInt.intValue < 0)
                    {
                        retour = "<color=#" + ColorUtility.ToHtmlStringRGBA(ListeCouleurs.Defaut.couleurAlerteTexteInterface) + ">"
                            + argumentInt.intValue + "<color=\"white\">" + " point" + pluriel + " de défense";
                    }

                    UnityAction<int> callback = new UnityAction<int>(bonus.ajouterBonusDefense);
                    UnityEventTools.AddIntPersistentListener(buff.antiEffets, callback, argumentInt.intValue * -1);
                }
                else if (methode.stringValue.Contains("DegatMoral"))
                {
                    if (argumentInt.intValue > 0)
                    {
                        retour = "<color=#" + ColorUtility.ToHtmlStringRGBA(ListeCouleurs.Defaut.couleurTexteBonus) + ">++" +
                            "<color=\"white\"> dégat moral";
                    }
                    else if (argumentInt.intValue < 0)
                    {
                        retour = "<color=#" + ColorUtility.ToHtmlStringRGBA(ListeCouleurs.Defaut.couleurAlerteTexteInterface) + ">--" +
                            "<color=\"white\"> dégat moral";
                    }
                    UnityAction<int> callback = new UnityAction<int>(bonus.AjouterBonusDegatMoral);
                    UnityEventTools.AddIntPersistentListener(buff.antiEffets, callback, argumentInt.intValue * -1);
                }
                else if (methode.stringValue.Contains("Deplacement"))
                {
                    char pluriel = '\0';
                    if (Math.Abs(argumentInt.intValue) != 1) pluriel = 's';
                    if (argumentInt.intValue > 0)
                    {
                        retour = "<color=#" + ColorUtility.ToHtmlStringRGBA(ListeCouleurs.Defaut.couleurTexteBonus) + ">+"
                            + argumentInt.intValue + "<color=\"white\"> point"+pluriel+ " de déplacement";
                    }
                    else if (argumentInt.intValue < 0)
                    {
                        retour = "<color=#" + ColorUtility.ToHtmlStringRGBA(ListeCouleurs.Defaut.couleurAlerteTexteInterface) +
                            argumentInt.intValue + "<color=\"white\"> point" + pluriel + " de déplacement";
                    }

                    if(methode.stringValue.Contains("ToutesTribus"))
                    {
                        retour += " pour toutes les tribus";
                        UnityAction<int> callback = new UnityAction<int>(bonus.AjouterBonusDeplacementToutesTribus);
                        UnityEventTools.AddIntPersistentListener(buff.antiEffets, callback, argumentInt.intValue * -1);
                    }
                    else
                    {
                        UnityAction<int> callback = new UnityAction<int>(bonus.AjouterBonusDeplacement);
                        UnityEventTools.AddIntPersistentListener(buff.antiEffets, callback, argumentInt.intValue * -1);
                    }
                }
        
                else if (methode.stringValue.Contains("MultiplicateurCoutPop"))
                {
                    if (argumentFloat.floatValue > 1)
                    {
                        retour = "<color=#" + ColorUtility.ToHtmlStringRGBA(ListeCouleurs.Defaut.couleurAlerteTexteInterface) + ">" +
                            "++<color=\"white\"> Cout d'une population";
                    }
                    else if (argumentFloat.floatValue < 1)
                        retour = "<color=#" + ColorUtility.ToHtmlStringRGBA(ListeCouleurs.Defaut.couleurTexteBonus) + ">" +
                            "--<color=\"white\"> Cout d'une population";
                    if (argumentFloat.floatValue == 0)
                    {
                        retour = "Cout d'une populaiton< color =#" + ColorUtility.ToHtmlStringRGBA(ListeCouleurs.Defaut.couleurTexteBonus) + "> gratuit";
                    }

                    UnityAction<float> callback = new UnityAction<float>(bonus.AssignerMultiplicateurCoutPop);
                    UnityEventTools.AddFloatPersistentListener(buff.antiEffets, callback, argumentFloat.floatValue * -1);
                }
                else if (methode.stringValue.Contains("MultiplicateurStockage"))
                {
                    if (argumentFloat.floatValue > 1)
                    {
                        retour = "<color=#" + ColorUtility.ToHtmlStringRGBA(ListeCouleurs.Defaut.couleurAlerteTexteInterface) + ">" +
                            "++<color=\"white\"> Capacité de stockage";
                    }
                    else if (argumentFloat.floatValue < 1)
                        retour = "<color=#" + ColorUtility.ToHtmlStringRGBA(ListeCouleurs.Defaut.couleurTexteBonus) + ">" +
                            "--<color=\"white\"> Capacité de stockage";
                    if (argumentFloat.floatValue == 0)
                    {
                        retour = "Capacité de stockage< color =#" + ColorUtility.ToHtmlStringRGBA(ListeCouleurs.Defaut.couleurAlerteTexteInterface) + "> ANNULEE";
                    }

                    UnityAction<float> callback = new UnityAction<float>(bonus.AssignerMutliplicateurStockage);
                    UnityEventTools.AddFloatPersistentListener(buff.antiEffets, callback, argumentFloat.floatValue * -1);
                }
                else if (methode.stringValue.Contains("MultProd"))
                {
                    if(methode.stringValue.Contains("Nourriture"))
                    {
                        if (argumentFloat.floatValue > 1)
                        {
                            retour = "<color=#" + ColorUtility.ToHtmlStringRGBA(ListeCouleurs.Defaut.couleurAlerteTexteInterface) + ">" +
                                "++<color=\"white\"> Production Nourriture";
                        }
                        else if (argumentFloat.floatValue < 1)
                            retour = "<color=#" + ColorUtility.ToHtmlStringRGBA(ListeCouleurs.Defaut.couleurTexteBonus) + ">" +
                                "--<color=\"white\"> Production Nourriture";
                        if (argumentFloat.floatValue == 0)
                        {
                            retour = "Production Nourriture<color =#" + 
                                ColorUtility.ToHtmlStringRGBA(ListeCouleurs.Defaut.couleurAlerteTexteInterface) + "> ANNULEE";
                        }

                        if (methode.stringValue.Contains("ToutesTribus"))
                        {
                            retour += " pour toutes les tribus";
                            UnityAction<float> callback = new UnityAction<float>(bonus.AssignerMultProdNourritureToutesTribus);
                            UnityEventTools.AddFloatPersistentListener(buff.antiEffets, callback, argumentInt.intValue * -1);
                        }
                        else
                        {
                            UnityAction<float> callback = new UnityAction<float>(bonus.AssignerMultProdNourriture);
                            UnityEventTools.AddFloatPersistentListener(buff.antiEffets, callback, argumentInt.intValue * -1);
                        }
                    }
                    else if (methode.stringValue.Contains("Pierre"))
                    {
                        if (argumentFloat.floatValue > 1)
                        {
                            retour = "<color=#" + ColorUtility.ToHtmlStringRGBA(ListeCouleurs.Defaut.couleurAlerteTexteInterface) + ">" +
                                "++<color=\"white\"> Production Pierre";
                        }
                        else if (argumentFloat.floatValue < 1)
                            retour = "<color=#" + ColorUtility.ToHtmlStringRGBA(ListeCouleurs.Defaut.couleurTexteBonus) + ">" +
                                "--<color=\"white\"> Production Pierre";
                        if (argumentFloat.floatValue == 0)
                        {
                            retour = "Production Pierre<color =#" +
                                ColorUtility.ToHtmlStringRGBA(ListeCouleurs.Defaut.couleurAlerteTexteInterface) + "> ANNULEE";
                        }

                        if (methode.stringValue.Contains("ToutesTribus"))
                        {
                            retour += " pour toutes les tribus";
                            UnityAction<float> callback = new UnityAction<float>(bonus.AssignerMultProdPierreToutesTribus);
                            UnityEventTools.AddFloatPersistentListener(buff.antiEffets, callback, argumentInt.intValue * -1);
                        }
                        else
                        {
                            UnityAction<float> callback = new UnityAction<float>(bonus.AssignerMultProdPierre);
                            UnityEventTools.AddFloatPersistentListener(buff.antiEffets, callback, argumentInt.intValue * -1);
                        }
                    }
                    else if(methode.stringValue.Contains("Peau"))
                    {
                        if (argumentFloat.floatValue > 1)
                        {
                            retour = "<color=#" + ColorUtility.ToHtmlStringRGBA(ListeCouleurs.Defaut.couleurAlerteTexteInterface) + ">" +
                                "++<color=\"white\"> Production Peau";
                        }
                        else if (argumentFloat.floatValue < 1)
                            retour = "<color=#" + ColorUtility.ToHtmlStringRGBA(ListeCouleurs.Defaut.couleurTexteBonus) + ">" +
                                "--<color=\"white\"> Production Peau";
                        if (argumentFloat.floatValue == 0)
                        {
                            retour = "Production Peau<color =#" +
                                ColorUtility.ToHtmlStringRGBA(ListeCouleurs.Defaut.couleurAlerteTexteInterface) + "> ANNULEE";
                        }

                        if (methode.stringValue.Contains("ToutesTribus"))
                        {
                            retour += " pour toutes les tribus";
                            UnityAction<float> callback = new UnityAction<float>(bonus.AssignerMultProdPeauToutesTribus);
                            UnityEventTools.AddFloatPersistentListener(buff.antiEffets, callback, argumentInt.intValue * -1);
                        }
                        else
                        {
                            UnityAction<float> callback = new UnityAction<float>(bonus.AssignerMultProdPeau);
                            UnityEventTools.AddFloatPersistentListener(buff.antiEffets, callback, argumentInt.intValue * -1);
                        }
                    }
                    else if (methode.stringValue.Contains("Outil"))
                    {
                        if (argumentFloat.floatValue > 1)
                        {
                            retour = "<color=#" + ColorUtility.ToHtmlStringRGBA(ListeCouleurs.Defaut.couleurAlerteTexteInterface) + ">" +
                                "++<color=\"white\"> Production Outil";
                        }
                        else if (argumentFloat.floatValue < 1)
                            retour = "<color=#" + ColorUtility.ToHtmlStringRGBA(ListeCouleurs.Defaut.couleurTexteBonus) + ">" +
                                "--<color=\"white\"> Production Outil";
                        if (argumentFloat.floatValue == 0)
                        {
                            retour = "Production Outil<color =#" +
                                ColorUtility.ToHtmlStringRGBA(ListeCouleurs.Defaut.couleurAlerteTexteInterface) + "> ANNULEE";
                        }

                        if (methode.stringValue.Contains("ToutesTribus"))
                        {
                            retour += " pour toutes les tribus";
                            UnityAction<float> callback = new UnityAction<float>(bonus.AssignerMultProdOutilToutesTribus);
                            UnityEventTools.AddFloatPersistentListener(buff.antiEffets, callback, argumentInt.intValue * -1);
                        }
                        else
                        {
                            UnityAction<float> callback = new UnityAction<float>(bonus.AssignerMultProdOutil);
                            UnityEventTools.AddFloatPersistentListener(buff.antiEffets, callback, argumentInt.intValue * -1);
                        }
                    }
                    else if (methode.stringValue.Contains("Pigment"))
                    {
                        if (argumentFloat.floatValue > 1)
                        {
                            retour = "<color=#" + ColorUtility.ToHtmlStringRGBA(ListeCouleurs.Defaut.couleurAlerteTexteInterface) + ">" +
                                "++<color=\"white\"> Production Pigment";
                        }
                        else if (argumentFloat.floatValue < 1)
                            retour = "<color=#" + ColorUtility.ToHtmlStringRGBA(ListeCouleurs.Defaut.couleurTexteBonus) + ">" +
                                "--<color=\"white\"> Production Pigment";
                        if (argumentFloat.floatValue == 0)
                        {
                            retour = "Production Pigment<color =#" +
                                ColorUtility.ToHtmlStringRGBA(ListeCouleurs.Defaut.couleurAlerteTexteInterface) + "> ANNULEE";
                        }

                        if (methode.stringValue.Contains("ToutesTribus"))
                        {
                            retour += " pour toutes les tribus";
                            UnityAction<float> callback = new UnityAction<float>(bonus.AssignerMultProdPigmentToutesTribus);
                            UnityEventTools.AddFloatPersistentListener(buff.antiEffets, callback, argumentInt.intValue * -1);
                        }
                        else
                        {
                            UnityAction<float> callback = new UnityAction<float>(bonus.AssignerMultProdPigment);
                            UnityEventTools.AddFloatPersistentListener(buff.antiEffets, callback, argumentInt.intValue * -1);
                        }
                    }
                    else
                    {
                        if (argumentFloat.floatValue > 1)
                        {
                            retour = "<color=#" + ColorUtility.ToHtmlStringRGBA(ListeCouleurs.Defaut.couleurAlerteTexteInterface) + ">" +
                                "++<color=\"white\"> Production";
                        }
                        else if (argumentFloat.floatValue < 1)
                            retour = "<color=#" + ColorUtility.ToHtmlStringRGBA(ListeCouleurs.Defaut.couleurTexteBonus) + ">" +
                                "--<color=\"white\"> Production";
                        if (argumentFloat.floatValue == 0)
                        {
                            retour = "Production<color =#" +
                                ColorUtility.ToHtmlStringRGBA(ListeCouleurs.Defaut.couleurAlerteTexteInterface) + "> ANNULEE";
                        }

                        if (methode.stringValue.Contains("ToutesTribus"))
                        {
                            retour += " pour toutes les tribus";
                            UnityAction<float> callback = new UnityAction<float>(bonus.AssignerMultProdToutesTribus);
                            UnityEventTools.AddFloatPersistentListener(buff.antiEffets, callback, argumentInt.intValue * -1);
                        }
                        else
                        {
                            UnityAction<float> callback = new UnityAction<float>(bonus.AssignerMultProd);
                            UnityEventTools.AddFloatPersistentListener(buff.antiEffets, callback, argumentInt.intValue * -1);
                        }
                    }
                }

            }
            if (buff.compteurTour)
            {
                if (!buff.name.Contains("SpawnTroupeau"))
                {
                    char pluriel = '\0';
                    if (buff.nombreTour > 0) pluriel = 's';
                    retour += " pendant " + buff.nombreTour + " tour" + pluriel;
                }
                else retour = "";
            }
            else if (buff.tpsDuneTechno)
            {

            }
            else if(buff.tpsDunEvent)
            {
                retour += " jusqu'à la fin de l'évenement";
            }

            buff.listeEffetsRetours[i] = retour;
        }
    
    }
    private void InitialiserListeRetour(List<string> retours, int ettendue)
    {
        if(retours != null)
        {
            int difference = ettendue - retours.Count;
            if (difference > 0)
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
            else if (difference <= 0)
            {
                for (int i = 0; i < retours.Count; i++)
                {
                    retours[i] = "";
                }
            }
        }
    }

    private void Sauvegarder()
    {
        AssetDatabase.Refresh();
        EditorUtility.SetDirty(target);
        AssetDatabase.SaveAssets();
        EcrireEffets();
    }
}
