using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.U2D;
using UnityEditor.U2D.SpriteShape;
using UnityEngine.U2D;

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

    Vector2 scrollPosition = Vector2.zero;

    #region VARIABLES DAMIER
    DamierGen damierGen;
    int colonnes = 1;
    int lignes = 1;

    bool modeAdditif = true;
    string modeAjout = "additif";
    char opperateurAjout = '+';
    #endregion

    #region VARIABLES FLEUVE
    DamierFleuveGen damierFleuve;
    Fleuve fleuveSelectionne;
    bool modeFleuve = false;
    bool modeAjoutFleuve = false;
    int indexSelectionFleuve = -1;
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
        damierGen = FindObjectOfType<DamierGen>();
        damierFleuve = FindObjectOfType<DamierFleuveGen>();
        colonnes = damierGen.colonnes;
        lignes = damierGen.lignes;

        listeTerrains = GameObject.FindGameObjectWithTag("ListeTerrains").GetComponents<TuileTerrain>();
    }

    private void Update()
    {
        Repaint();
    }

    private void OnGUI()
    {
        scrollPosition = GUILayout.BeginScrollView(scrollPosition, true, true);

        GUILayout.Space(10);

        DessinerGenerateurDamier();

        GUILayout.Space(10);

        DessinerModificateurDamier();

        GUILayout.Space(15);


        DessinerPalletteTerrain();

        Peindre();


        GUILayout.Space(15);

        DessinerInterfacePinceauFleuve();

        GUILayout.Space(15);

        DessinerInterfacesSauvegarde();

        GUILayout.EndScrollView();
    }

    #region GENERATEUR ET MODIFICATEUR DE DAMIER
    private void DessinerGenerateurDamier()
    {
        GUIStyle texteGauche = GUI.skin.GetStyle("Label");
        texteGauche.alignment = TextAnchor.MiddleLeft;

        GUILayout.Label("Générateur de Damier", texteGauche);

        GUILayout.Space(05);

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

        GUILayout.Space(-150);

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

                //mise à jour dimensions
                colonnes = damierGen.colonnes;
                lignes = damierGen.lignes;
            }
            else
            {
                //Retire 5 lignes
                damierGen.RetirerTuiles(0, 5);

                //mise à jour dimensions
                colonnes = damierGen.colonnes;
                lignes = damierGen.lignes;
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

                //mise à jour dimensions
                colonnes = damierGen.colonnes;
                lignes = damierGen.lignes;
            }
            else
            {
                //Retire 5 colonnes et 5 lignes
                damierGen.RetirerTuiles(5, 5);

                //mise à jour dimensions
                colonnes = damierGen.colonnes;
                lignes = damierGen.lignes;
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

                //mise à jour dimensions
                colonnes = damierGen.colonnes;
                lignes = damierGen.lignes;
            }
            else
            {
                //Retire 1 ligne
                damierGen.RetirerTuiles(0, 1);

                //mise à jour dimensions
                colonnes = damierGen.colonnes;
                lignes = damierGen.lignes;
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

                //mise à jour dimensions
                colonnes = damierGen.colonnes;
                lignes = damierGen.lignes;
            }
            else
            {
                //Retire 1 colonne et 1 ligne
                damierGen.RetirerTuiles(1, 1);

                //mise à jour dimensions
                colonnes = damierGen.colonnes;
                lignes = damierGen.lignes;
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
                ActiverModePeinture(false);
            }
            else
            {                
                ActiverModePeinture(true);
            }
        }

        GUILayout.Space(13);

        GUILayout.BeginHorizontal();

        GUILayout.BeginVertical();

        GUILayoutOption[] options = new GUILayoutOption[2] { GUILayout.MaxWidth(80), GUILayout.MinHeight(30) };
        DessinerBoutonsTerrain(options);

        GUILayout.EndVertical();

        GUILayout.Space(50);

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

    private void ActiverModePeinture(bool activer)
    {
        if(activer)
        {
            textBoutonActiverModePeinture = "désactiver mode peinture";
            modePeinture = true;
            ActiverModeFleuve(false);
            ActiverCanvas(false);
        }
        else
        {
            textBoutonActiverModePeinture = "activer mode peinture";
            modePeinture = false;
            ActiverCanvas(true);
        }
    }

    private void Peindre()
    {
        if (modePeinture)
        {
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
            Selection.objects = new Object[0];
        }
    }
    #endregion

    #region PINCEAU FLEUVE

    private void DessinerInterfacePinceauFleuve()
    {
        GUILayout.Label("Pinceau de Fleuve");


        DessinerBouttonsFleuve();

        DessinerFleuve();
    }

    private void DessinerBouttonsFleuve()
    {
        GameObject[] listeFleuves = GameObject.FindGameObjectsWithTag("Fleuve");
        

        GUILayout.BeginHorizontal();

        GUILayout.BeginVertical();

        //Dessine la liste des fleuves
        for (int i = 0; i < listeFleuves.Length; i++)
        {
            Fleuve fleuve = listeFleuves[i].GetComponent<Fleuve>();
            GUILayoutOption[] options = new GUILayoutOption[2] { GUILayout.Height(25), GUILayout.Width(80) };

            if (indexSelectionFleuve == i)
            {
                GUI.backgroundColor = Color.cyan;
            }
            else
            {
                GUI.backgroundColor = Color.white;
            }

            //Dessine la liste les boutons de selection de tous les fleuves
            if (GUILayout.Button(listeFleuves[i].name, options))
            {
                if(fleuveSelectionne == null)
                {
                    fleuveSelectionne = fleuve;
                    fleuveSelectionne.EstSelectionne(true);
                    fleuveSelectionne.GetComponent<SpriteShapeRenderer>().color = Color.blue;

                    indexSelectionFleuve = i;
                    ActiverModeFleuve(true);
                }
                else if(fleuve != fleuveSelectionne)
                {
                    fleuveSelectionne.GetComponent<SpriteShapeRenderer>().color = Color.white;
                    fleuve.GetComponent<SpriteShapeRenderer>().color = Color.blue;

                    fleuveSelectionne.EstSelectionne(false);
                    fleuveSelectionne = fleuve;
                    fleuveSelectionne.EstSelectionne(true);

                    indexSelectionFleuve = i;
                    ActiverModeFleuve(true);
                }
               else
                {
                    fleuveSelectionne.EstSelectionne(false);
                    fleuveSelectionne.GetComponent<SpriteShapeRenderer>().color = Color.white;
                    fleuveSelectionne = null;
                    indexSelectionFleuve = -1;

                    ActiverModeFleuve(false);
                }
            }
            if (!modeFleuve)
            {
                fleuve.GetComponent<SpriteShapeRenderer>().color = Color.white;
                fleuveSelectionne = null;
                indexSelectionFleuve = -1;
            }
        }
        GUI.backgroundColor = Color.white;
        GUILayout.EndVertical();

        GUILayout.Space(-80);

        // dessine les boutons de suppression qui vont avec
        GUILayout.BeginVertical();
        GUI.backgroundColor = Color.red;
        for (int i = 0; i < listeFleuves.Length; i++)
        {
            GUILayoutOption[] options = new GUILayoutOption[2] { GUILayout.Height(25), GUILayout.Width(100) };
            //Quand on clique sur un bouton, ça supprime le fleuve en question
            if (GUILayout.Button("SUPPRIMER", options))
            {
                if(i == indexSelectionFleuve)
                {
                    ActiverModeFleuve(false);
                    indexSelectionFleuve = -1;
                    fleuveSelectionne.GetComponent<Fleuve>().EstSelectionne(false);
                    fleuveSelectionne = null;
                }

                Fleuve fleuve = listeFleuves[i].GetComponent<Fleuve>();
                DestroyImmediate(fleuve.gameObject);
            }
        }
        GUI.backgroundColor = Color.white;
        GUILayout.EndVertical();

        GUILayout.EndHorizontal();

        GUILayout.Space(5);
        GUI.backgroundColor = Color.green;
        GUILayoutOption[] optionsAjout = new GUILayoutOption[2] { GUILayout.Height(30), GUILayout.Width(30) };
        //Quand on clique sur un bouton, ça supprime le fleuve en question
        if (GUILayout.Button(" + ", optionsAjout))
        {
            GameObject nvFleuve = Instantiate(damierFleuve.fleuvePrefab);
            nvFleuve.transform.position = new Vector3(0, 0, -200);
            nvFleuve.name = "Fleuve" + (listeFleuves.Length);
            nvFleuve.GetComponent<Fleuve>().Init();
        }
        GUI.backgroundColor = Color.white;
    }

    private void ActiverModeFleuve(bool activer)
    {
        DamierFleuveGen damierfleuve = FindObjectOfType<DamierFleuveGen>();

        if (activer)
        {
            ActiverModePeinture(false);
            modeFleuve = true;
            Vector3 position = new Vector3(0, 0, -4f);
            position.x = damierfleuve.transform.position.x;
            position.y = damierfleuve.transform.position.y;
            damierfleuve.transform.position = position;
            ActiverCanvas(false);
        }
        else
        {
            modeFleuve = false;
            Vector3 position = new Vector3(0, 0, 0.2f);
            position.x = damierfleuve.transform.position.x;
            position.y = damierfleuve.transform.position.y;
            damierfleuve.transform.position = position;
            ActiverCanvas(true);
        }
    }

    private void DessinerFleuve()
    {
        if(modeFleuve)
        {
            foreach (GameObject go in Selection.gameObjects)
            {
                if(go.CompareTag("NoeudFleuve"))
                {
                    NoeudFleuve nf = go.GetComponent<NoeudFleuve>();

                    if(fleuveSelectionne.grapheNoeuds.Contains(nf))
                    {
                        fleuveSelectionne.RetirerNoeud(nf);
                    }
                    else
                    {
                        fleuveSelectionne.AjouterNoeud(nf);
                    }
                }
                Selection.objects = new Object[0];
            }
            
        }
    }

    #endregion

    #region SAUVEGARDES

    private void DessinerInterfacesSauvegarde()
    {
        GUILayout.Label("Sauvegardes");

        GUILayout.Space(5);

        GUILayout.BeginHorizontal();
        nomMappeSauvegarde = GUILayout.TextField(nomMappeSauvegarde);
        if (GUILayout.Button("Sauvegarder Mappe"))
        {
            MappeSysteme.SauvergarderMappe(nomMappeSauvegarde);
            nomMappeSauvegarde = "NomSauvergarde";
        }
        GUILayout.EndHorizontal();

        GUILayout.Space(10);

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
            MappeSysteme.Mappe mappe;

            if(MappeSysteme.CheckerMappeExiste(nomMappe))
            {
                MappeSysteme.ChargerMappe(nomMappe);
                mappe = MappeSysteme.CreerMappe(nomMappe);

                colonnes = mappe.colonnes;
                lignes = mappe.lignes;
            }
            else
            {
                Debug.LogError("La mappe n'a pas été trouvée. Elle n'existe peut-être pas, ou le nom n'est pas le bon");
            }
        }

        GUI.backgroundColor = new Color(0.8f, 0.3f, 0.3f);
        if (GUILayout.Button("SUPPRIMER"))
        {
            if (MappeSysteme.CheckerMappeExiste(nomMappe))
            {
                MappeSysteme.SupprimerMappe(nomMappe);
            }
            else
            {
                Debug.LogError("La mappe n'a pas été trouvée. Elle n'existe peut-être pas, ou le nom n'est pas le bon");
            }
        }

        GUILayout.EndHorizontal();
    }
    #endregion


    private void ActiverCanvas(bool activer)
    {
        GameObject[] gOScene = Resources.FindObjectsOfTypeAll<GameObject>();

        foreach (GameObject go in gOScene)
        {
            if (go.TryGetComponent(out Canvas canvas))
            {
                canvas.gameObject.SetActive(activer);
            }
        }
    }

    private void Deselection()
    {
        Selection.objects = new GameObject[0];
    }
}
