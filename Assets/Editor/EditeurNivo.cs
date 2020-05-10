using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class EditeurNiveau : EditorWindow
{
    string textBoutonActiverModePeinture = "activer mode peinture";
    bool modePeinture = false;
    string nomMappeSauvegarde = "NomSauvegarde";

    TuileTerrain[] listeTerrains;
    TuileTerrain terrainSelectionne;

    string nomTerrainSelectionne;

    Rect rectangleAirePalette;

    Sprite spriteTerrain = null;

    Vector3 positionCameraMemoire;

    #region VARIABLES DAMIER
    DamierGen damierGen;
    int colonnes = 1;
    int lignes = 1;

    bool modeAdditif = true;
    string modeAjout = "additif";
    char opperateurAjout = '+';
    #endregion

    [MenuItem("Window/Editeur de Nivo")]
    public static void AfficherFenetre()
    {
        GetWindow<EditeurNiveau>("Editeur de Nivo");
    }

    private void OnFocus()
    {
        Init();
    }


    private void Init()
    {
        damierGen = GameObject.FindObjectOfType<DamierGen>();
        listeTerrains = GameObject.FindGameObjectWithTag("ListeTerrains").GetComponents<TuileTerrain>();
    }

    private void Update()
    {
        Repaint();
    }

    private void OnGUI()
    {
        GUILayout.Space(10);

        DessinerGenerateurDamier();

        GUILayout.Space(10);

        DessinerModificateurDamier();

        GUILayout.Space(15);


        DessinerPalletteTerrain();
        
        

        Peindre();

       


        GUILayout.Space(10);

        GUILayout.Label("Sauvegardes");

        GUILayout.Space(5);

        GUILayout.BeginHorizontal();
        nomMappeSauvegarde = GUILayout.TextField(nomMappeSauvegarde);
        if(GUILayout.Button("Sauvegarder Mappe"))
        {
            MappeSysteme.SauvergarderMappe(nomMappeSauvegarde);
            nomMappeSauvegarde = "NomSauvergarde";
        }
        GUILayout.EndHorizontal();

        GUILayout.Space(10);

        AfficherInterfacesSauvegarde();
    }

    #region GENERATEUR ET MODIFICATEUR DE DAMIER
    private void DessinerGenerateurDamier()
    {
        GUIStyle texteGauche = GUI.skin.GetStyle("Label");
        texteGauche.alignment = TextAnchor.MiddleLeft;

        GUILayout.Label("Générateur de Damier", texteGauche);

        GUILayout.Space(10);

        GUILayout.BeginHorizontal();

        GUILayout.BeginVertical();
        GUILayoutOption[] optionsIntField = new GUILayoutOption[2] { GUILayout.Height(20), GUILayout.Width(50)};

        GUILayout.Label("Colonnes");
        colonnes = EditorGUILayout.IntField(colonnes, optionsIntField);

        GUILayout.Label("Lignes");
        lignes = EditorGUILayout.IntField(lignes, optionsIntField);

        if(colonnes < 1)
        {
            colonnes = 1;
        }
        if(lignes < 1)
        {
            lignes = 1;
        }
        GUILayout.EndVertical();

        GUILayout.Space(-400);

        GUILayout.BeginVertical();

        GUILayout.Space(20);

        GUILayoutOption[] optionsBoutonGen = new GUILayoutOption[2] { GUILayout.Height(60), GUILayout.Width(100) };
        if (GUILayout.Button("Générer Damier",optionsBoutonGen))
        {
            damierGen.GenDamier(colonnes, lignes);
        }

        GUILayout.EndVertical();

        GUILayout.EndHorizontal();
    }

    private void DessinerModificateurDamier()
    {
        GUILayout.Label("Modficateur de Damier");

        GUILayout.Space(10);

        GUILayout.BeginHorizontal();

        GUILayoutOption[] optionsPlusCinqLi = new GUILayoutOption[2] { GUILayout.Height(25), GUILayout.Width(113) };

        //Bouton ajouter/retirer 5 lignes
        if (GUILayout.Button(opperateurAjout + "5", optionsPlusCinqLi))
        {
            if(modeAdditif)
            {
                //Ajoute 5 lignes
                damierGen.AjouterTuiles(0, 5);
            }
            else
            {
                //Retire 5 lignes
                damierGen.RetirerTuiles(0, 5);
            }
        }

        GUILayout.Space(5);

        GUILayoutOption[] optionsLiCol = new GUILayoutOption[2] { GUILayout.Height(25), GUILayout.Width(25) };

        //Bouton ajouter/retirer 5 lignes et 5 colonnes
        if (GUILayout.Button(opperateurAjout + "5", optionsLiCol))
        {
            if(modeAdditif)
            {
                //Ajoute 5 colonnes et 5 lignes
                damierGen.AjouterTuiles(5, 5);
            }
            else
            {
                //Retire 5 colonnes et 5 lignes
                damierGen.RetirerTuiles(5, 5);
            }

        }
        GUILayout.EndHorizontal();

        GUILayout.Space(5);

        GUILayout.BeginHorizontal();

        GUILayoutOption[] optionsPlusUnLi = new GUILayoutOption[2] { GUILayout.Height(25), GUILayout.Width(80) };

        //Bouton ajouter/retirer 1 ligne
        if (GUILayout.Button(opperateurAjout + "1", optionsPlusUnLi))
        {
            if(modeAdditif)
            {
                //Ajoute 1 ligne
                damierGen.AjouterTuiles(0, 1);
            }
            else
            {
                //Retire 1 ligne
                damierGen.RetirerTuiles(0, 1);
            }
        }

        GUILayout.Space(5);

        //Bouton ajouter/retirer 1 colonne et 1 ligne
        if (GUILayout.Button(opperateurAjout + "1", optionsLiCol))
        {
            if(modeAdditif)
            {
                //Ajoute 1 colonne et 1 ligne
                damierGen.AjouterTuiles(1, 1);
            }
            else
            {
                //Retire 1 colonne et 1 ligne
                damierGen.RetirerTuiles(1, 1);
            }
        }
        GUILayout.EndHorizontal();

        GUILayout.Space(5);

        GUILayout.BeginHorizontal();

        GUILayoutOption[] optionsAdditif = new GUILayoutOption[2] { GUILayout.Width(80), GUILayout.Height(80) };

        //Bouton changer de mode d'addition/soustraction
        if (GUILayout.Button(modeAjout, optionsAdditif))
        {
            modeAdditif = !modeAdditif;


            if (modeAdditif == true)
            {
                modeAjout = "additif";
                opperateurAjout = '+';
            }
            else
            {
                modeAjout = "soustractif";
                opperateurAjout = '-';
            }
        }

        GUILayout.Space(5);

        GUILayoutOption[] optionsPlusUnCol = new GUILayoutOption[2] { GUILayout.Height(80), GUILayout.Width(25)};

        //Bouton ajouter/retirer 1 colonne
        if (GUILayout.Button(opperateurAjout + "1", optionsPlusUnCol))
        {
            if(modeAdditif)
            {
                //Ajoute 1 colonne
                damierGen.AjouterTuiles(1, 0);
            }
            else
            {
                //Retire 1 colone
                damierGen.RetirerTuiles(1, 0);
            }
        }

        GUILayout.Space(5);

        GUILayout.BeginVertical();
        GUILayout.Space(-31);
        
        GUILayoutOption[] optionsPlusCinqCol = new GUILayoutOption[2] { GUILayout.Height(113), GUILayout.Width(25) };

        //Bouton ajouter/retirer 5 colonnes
        if (GUILayout.Button(opperateurAjout + "5", optionsPlusCinqCol))
        {
            if (modeAdditif)
            {
                //Ajoute 5 colonnes
                damierGen.AjouterTuiles(5, 0);
            }
            else
            {
                //Retire 5 colones
                damierGen.RetirerTuiles(5, 0);
            }
        }
        GUILayout.EndVertical();

        GUILayout.EndHorizontal();

    }

    #endregion

    #region PALETTE TERRAIN
    private void DessinerPalletteTerrain()
    {
        GUILayout.Label("Palette des terrains");

        GUILayout.Space(10);


        if (GUILayout.Button(textBoutonActiverModePeinture, GUILayout.MaxWidth(160)))
        {
            if (modePeinture)
            {
                textBoutonActiverModePeinture = "activer mode peinture";
                modePeinture = false;
            }
            else
            {
                textBoutonActiverModePeinture = "désactiver mode peinture";
                modePeinture = true;
            }
        }

        GUILayout.Space(13);

        GUILayout.BeginHorizontal();

        GUILayout.BeginVertical();

        GUILayoutOption[] options = new GUILayoutOption[2] { GUILayout.MaxWidth(80), GUILayout.MinHeight(30) };
        DessinerBoutonsTerrain(options);

        GUILayout.EndVertical();

        GUILayout.Space(150);

        GUILayout.BeginVertical();

        DessinerSpriteTuile();

        GUILayout.EndVertical();

        GUILayout.EndHorizontal();
       
    }

    private void DessinerSpriteTuile()
    {
        GUILayout.Label(nomTerrainSelectionne);

        GUIStyle styleTuileSprite = GUI.skin.GetStyle("Box");
        
        if(spriteTerrain)
        {
            GUILayoutOption[] options = new GUILayoutOption[2] { GUILayout.MaxWidth(150), GUILayout.MinHeight(150) };
            
            GUILayout.Box(spriteTerrain.texture,options);
     
        }
        
        //Graphics.DrawTexture(new Rect(23, 25, 100, 100), spriteTerrain.texture);
    }

    private void DessinerBoutonsTerrain(GUILayoutOption[] options)
    {
        for (int i = 0; i < listeTerrains.Length; i++)
        {
            if (GUILayout.Button(listeTerrains[i].nom, options))
            {
                Selection.objects = new GameObject[0];
                //Debug.Log(listeTerrains[i].nom);

                terrainSelectionne = listeTerrains[i];
                spriteTerrain = listeTerrains[i].sprite;
                nomTerrainSelectionne = listeTerrains[i].nom;

                //Debug.Log(terrainSelectionne);
            }
        }
    }
    #endregion

    #region SAUVEGARDES

    private void AfficherInterfacesSauvegarde()
    {
        List<string> listeMappes = MappeSysteme.RecuprererNomMappes();
        for (int i = 0; i < listeMappes.Count; i++)
        {
            CreerIntefaceSauvegarde(listeMappes[i]);
        }
    }

    private void CreerIntefaceSauvegarde(string nomMappe)
    {
        GUILayout.BeginHorizontal();

        GUILayout.Label(nomMappe);

        GUI.backgroundColor = new Color(0.3f, 0.8f, 0.3f);
        if (GUILayout.Button("CHARGER"))
        {
            MappeSysteme.ChargerMappe(nomMappe);
            
            MappeSysteme.Mappe mappe = MappeSysteme.CreerMappe(nomMappe);

            colonnes = mappe.colonnes;
            lignes = mappe.lignes;
        }

        GUI.backgroundColor = new Color(0.8f, 0.3f, 0.3f);
        if (GUILayout.Button("SUPPRIMER"))
        {
            MappeSysteme.SupprimerMappe(nomMappe);
        }

        GUILayout.EndHorizontal();
    }
    #endregion

    private void Peindre()
    {
        if (modePeinture)
        {
            GameObject[] gOScene = Resources.FindObjectsOfTypeAll<GameObject>();

            foreach (GameObject go in gOScene)
            {
                if(go.name == "Canvas")
                {
                    go.gameObject.SetActive(false);
                }
               
            }

            foreach (GameObject go in Selection.gameObjects)
            {
                if (go.GetComponent<TuileManager>())
                {
                    TuileManager tuile = go.GetComponent<TuileManager>();

                    //Debug.Log(terrainSelectionne.nom);
                    if(tuile.gameObject.name != "TuileHexa")
                    {
                    tuile.SetTerrain(terrainSelectionne);
                    }
                }
            }
        }
        else
        {
            GameObject[] gOScene = Resources.FindObjectsOfTypeAll<GameObject>();

            foreach (GameObject go in gOScene)
            {
                if (go.name == "Canvas")
                {
                    go.gameObject.SetActive(true);
                }
                    

            }

        }
    }

    private void Deselection()
    {
        Selection.objects = new GameObject[0];
    }
}
