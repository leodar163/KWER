using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

static public class MappeSysteme
{

    private static string cheminDefaut = "Assets/Mappes/";
    private static string extention = ".mappe";

    private const string baliseNom = "<n>";
    private const string baliseColonne = "<c>";
    private const string baliseLigne = "<l>";
    private const string baliseMappeTerrain = "<t>";
    private const string baliseFleuve = "<f>";
    private const string baliseNvFleuve = "<nf>";
    private const char separateurTerrain  = ':';
    private const char separateurFleuve = '|';

    private static TuileTerrain[] listeTerrains;

    public struct Mappe
    {
        public string nom;
        public TuileTerrain[,] mappeTerrains;
        public int colonnes;
        public int lignes;
        public List<string> listeFleuves;
        public char separateurNouedFleuve;

        public Mappe(string nomMappe, int nbrColonnes, int nbrLignes, TuileTerrain[,] listeTerrains, List<string> codeFleuve)
        {
            nom = nomMappe;
            colonnes = nbrColonnes;
            lignes = nbrLignes;
            mappeTerrains = listeTerrains;
            listeFleuves = codeFleuve;
            separateurNouedFleuve = separateurFleuve;
        }
    }

    #region SAUVEGARDE
    public static void SauvergarderMappe(string nomMappe, bool ecraserSave)
    {
        string cheminMappe = cheminDefaut + nomMappe + extention;

        listeTerrains = GameObject.FindGameObjectWithTag("ListeTerrains").GetComponents<TuileTerrain>();

        DamierGen damierGen = Object.FindObjectOfType<DamierGen>();
        TuileTerrain[,] damierTerrains = CreerDamierTerrain(damierGen.Damier);
        Fleuve[] listeFleuves = GameObject.FindObjectsOfType<Fleuve>();

        Mappe mappe = new Mappe(nomMappe, damierGen.colonnes, damierGen.lignes, damierTerrains, CreerListeFleuve(listeFleuves));

        if(ecraserSave)
        {    
                SupprimerMappe(nomMappe);

                CreerFichierMappe(cheminMappe, CreerCodeMappe(mappe));
            
        }
        else
        {
            CreerFichierMappe(cheminMappe, CreerCodeMappe(mappe));
        }
    }

    //Crée le fichier .mappe et y inscrit le code de la mappe
    private static void CreerFichierMappe(string cheminMappe, string codeMappe)
    {
        StreamWriter ecriteur = new StreamWriter(cheminMappe);

        ecriteur.Write(codeMappe);
        ecriteur.Close();
    }

    //Créer le code qui va être inscrit dans le fichier .mappe
    private static string CreerCodeMappe(Mappe mappe)
    {
        string codeMappe = baliseNom + mappe.nom 
            + baliseColonne + mappe.colonnes 
            + baliseLigne + mappe.lignes 
            + baliseMappeTerrain + CreerCodeMappeTerrain(mappe.mappeTerrains) 
            + baliseFleuve + CreerCodeFleuve(mappe.listeFleuves)
            ;


        return codeMappe;
    }

    //Créer la partie du code de la mappe qui concerne les types de terrain
    private static string CreerCodeMappeTerrain(TuileTerrain[,] mappeTerrain)
    {
        string codeMappeTerrain = "";

        for (int y = 0; y < mappeTerrain.GetLength(1); y++)
        {
            for (int x = 0; x < mappeTerrain.GetLength(0); x++)
            {
                codeMappeTerrain += separateurTerrain + mappeTerrain[x, y].nom;
            }
        }

        return codeMappeTerrain;
    }

    //Créer le code de la mappe qui va contenir les informations nécessaires à la sauvegarde des fleuves à partir de tous les fleuves présents sur la mappe
    private static List<string> CreerListeFleuve(Fleuve[] listeFleuves)
    {
        List<string> listeFleuvesCode = new List<string>();

        for (int i = 0; i < listeFleuves.Length; i++)
        {
            if(listeFleuves[i].grapheNoeuds.Count > 0)
            {
                listeFleuvesCode.Add("");
                for (int x = 0; x < listeFleuves[i].grapheNoeuds.Count; x++)
                {
                    listeFleuvesCode[i] += separateurFleuve;
                    listeFleuvesCode[i] += listeFleuves[i].grapheNoeuds[x].gameObject.name;
                }
            }
        }
        return listeFleuvesCode;
    }

    //Génère le code du fichier .mappe qui va contenir les info des fleuves
    private static string CreerCodeFleuve(List<string> listeFleuves)
    {
        string codeFleuve = "";

        for (int i = 0; i < listeFleuves.Count; i++)
        {
            codeFleuve += baliseNvFleuve;
            codeFleuve += listeFleuves[i];
        }

        return codeFleuve;
    }
    #endregion


    #region CHARGEMENT
    public static void ChargerMappe(string nomMappe)
    {
        DamierGen damierGen = Object.FindObjectOfType<DamierGen>();
        DamierFleuveGen damierFleuve = Object.FindObjectOfType<DamierFleuveGen>();
        listeTerrains = GameObject.FindGameObjectWithTag("ListeTerrains").GetComponents<TuileTerrain>();
        Mappe mappe = CreerMappe(nomMappe);

        damierGen.GenDamier(mappe);
    }

    //Créer un structure Mappe à partir du nom d'un fichier .mappe
    public static Mappe CreerMappe(string nomMappe)
    {
        string code = RecupererCodeMappe(nomMappe);

        char[] tableauCode = code.ToCharArray();
        string balise = "";
        bool estBalise = false;

        string nom = "";
        string colonnesTxt ="";
        string lignesTxt = "";
        string codeTerrain = "";
        List<string> listeFleuves = new List<string>();
        int indexFleuve = -1;

        for (int i = 0; i < tableauCode.Length; i++)
        {
            //Détecte et stock les balises.
            if (estBalise)
            {
                balise += tableauCode[i];
            }
            if (tableauCode[i] == '<')
            {
                estBalise = true;
                balise = "<";
            }
            else if (tableauCode[i] == '>')
            {
                estBalise = false;
                if(balise == baliseNvFleuve)
                {
                    indexFleuve++;
                    listeFleuves.Add("");
                }
                continue;
            }


            //Stock le contenu entre les balises.
            switch (balise)
            {
                case baliseNom:
                    nom += tableauCode[i];
                    break;

                case baliseColonne:
                    colonnesTxt += tableauCode[i];
                    break;

                case baliseLigne:
                    lignesTxt += tableauCode[i];
                    break;

                case baliseMappeTerrain:
                    codeTerrain += tableauCode[i];
                    break;
                case baliseFleuve:
                    //Je crois qu'au final cette balise sert à rien
                    break;
                case baliseNvFleuve:
                    listeFleuves[indexFleuve] += tableauCode[i];
                    break;
            }
        }

        
        int colonnes = int.Parse(colonnesTxt);
        int lignes = int.Parse(lignesTxt);

        char[] separateurs = new char[] { separateurTerrain };
        string[] listeCodeTerrain = codeTerrain.Split(separateurs, System.StringSplitOptions.RemoveEmptyEntries);
       
        TuileTerrain[,] damierTerrains = CreerDamierTerrain(listeCodeTerrain, colonnes, lignes);

        Mappe mappe = new Mappe(nom, colonnes, lignes, damierTerrains, listeFleuves);

        return mappe;
    }

    //Extrait la carte des terrain de la carte des tuiles.
    private static TuileTerrain[,] CreerDamierTerrain(TuileManager[,] damier)
    {
        TuileTerrain[,] damierTerrains = new TuileTerrain[damier.GetLength(0),damier.GetLength(1)];

        //int index = 0;
        for (int y = 0; y < damier.GetLength(1); y++)
        {
            for (int x = 0; x < damier.GetLength(0); x++)
            {
                damierTerrains[x, y] = damier[x, y].terrainTuile;

                //Debug.Log("tuile"+index+" = "+damier[x, y].terrainTuile.nom);
                //index++;
            }
        }

        return damierTerrains;
    }

    private static TuileTerrain[,] CreerDamierTerrain(string[] codeTerrain, int colonnes, int lignes)
    {
        TuileTerrain[,] damierTerrains = new TuileTerrain[colonnes, lignes];
        int index = 0;

        for (int y = 0; y < lignes; y++)
        {
            for (int x = 0; x < colonnes; x++)
            {
                damierTerrains[x, y] = ConvertirCodeTerrain(codeTerrain[index]);
                //Debug.Log("index : " + index + " terrain : " + ConvertirCodeTerrain(codeTerrain[index]).nom);
                
                index++;
            }
        }
    
        return damierTerrains;
    }

    //Convertit le code du terrain en référence au terrain
    private static TuileTerrain ConvertirCodeTerrain(string code)
    {
        foreach(TuileTerrain terrain in listeTerrains)
        {
            if(terrain.nom == code)
            {
                return terrain;
            }
        }

        return null;
    }

    //Récupère le contenu/code d'un fichier .mappe en fonction du nom de la mappe
    private static string RecupererCodeMappe(string nomMappe)
    {
        string chemin = cheminDefaut + nomMappe + extention;
        string code = "";

        if(CheckerMappeExiste(nomMappe))
        {
            code = File.ReadAllText(chemin);
        }

        return code;
    }
    #endregion


    #region NAVIGATION
    //Vérifie si la mappe existe en fonction de son nom
    public static bool CheckerMappeExiste(string nomMappe)
    {
        string nomMappeFichier = nomMappe + extention;
        string[] mappes = Directory.GetFiles(cheminDefaut);

        for (int i = 0; i < mappes.Length; i++)
        {
            if(mappes[i] == cheminDefaut + nomMappeFichier)
            {
                return true;
            }
        }
        return false;
    }

    public static void SupprimerMappe(string nomMappe)
    {
        string chemin = cheminDefaut + nomMappe + extention;

        string cheminMeta = chemin + ".meta";

        File.Delete(chemin);
        File.Delete(cheminMeta);

    }

    //Récupère le nom de toutes les mappes
    public static List<string> RecuprererNomMappes()
    {
        string[] saves = Directory.GetFiles(cheminDefaut, "*.mappe");
        List<string> listeSauvegardes = new List<string>(saves);
        

        for (int i = 0; i < listeSauvegardes.Count; i++)
        {
                string nom = listeSauvegardes[i];

                int indexExtention = nom.IndexOf(extention);
                int indexChemin = nom.IndexOf(cheminDefaut);

                nom = nom.Remove(indexExtention);
                nom = nom.Remove(indexChemin, cheminDefaut.Length);

                //Debug.Log(nom);

                listeSauvegardes[i] = nom;

                //Debug.Log(nom);
        }

        return listeSauvegardes;
    }
    #endregion
}
