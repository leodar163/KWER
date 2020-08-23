using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.Tilemaps;

public class InfoTribus : MonoBehaviour
{
    private static InfoTribus cela;

    public static InfoTribus Defaut
    {
        get
        {
            if (cela == null) cela = FindObjectOfType<InfoTribus>();
            return cela;
        }
    }


    static public Tribu TribukiJoue
    {
        get
        {
            if (ListeOrdonneeDesTribus.Length > 0)
                return ListeOrdonneeDesTribus[TourParTour.Defaut.idTribu];
            else return null;
        }
    }

    static private Tribu[] listeOrdonneeTribus;
    static public Tribu[] ListeOrdonneeDesTribus
    {
        get
        {
            bool tribuMort = false;
            bool nbrTribuChange = false;
            Tribu[] tribus = FindObjectsOfType<Tribu>();
            if (listeOrdonneeTribus != null)
            {
                for (int i = 0; i < listeOrdonneeTribus.Length; i++)
                {
                    if (listeOrdonneeTribus[i] == null)
                        tribuMort = true;
                }
                if (tribus.Length != listeOrdonneeTribus.Length) nbrTribuChange = true;
            }
            if (listeOrdonneeTribus == null || tribuMort || nbrTribuChange)
            {
                listeOrdonneeTribus = new Tribu[tribus.Length];

                for (int i = 0; i < tribus.Length; i++)
                {
                    listeOrdonneeTribus[tribus[i].idTribu] = tribus[i];
                }
            }

            return listeOrdonneeTribus;
        }
    }
    public GameObject prefabTribu;
    [Space]
    public int nbrTribuMax = 4;
    [Space]
    [Tooltip("Sprites des boutons qui servent à différencier les tribus et à ouvrir leur interface campement" +
        "\nA ranger dans le même ordre que les sprites de pion")]
    public List<Sprite> bannieresTribus = new List<Sprite>();
    [Space]
    [Tooltip("Sprites des pions des tribus quand elles sont en déplacement" +
        "\nA ranger dans le même ordre que les sprites de bannière")]
    public List<Sprite> pionTribus = new List<Sprite>();

    // Start is called before the first frame update
    void Start()
    {
        cela = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void AjouterTribu(Vector3 position)
    {
        if(listeOrdonneeTribus.Length < cela.nbrTribuMax)
        {
            int nbrTribus = listeOrdonneeTribus.Length;
#if !UNITY_EDITOR
    GameObject nvlleTribu = Instantiate(cela.prefabTribu, cela.transform);
#else
            GameObject nvlleTribu = PrefabUtility.InstantiatePrefab(cela.prefabTribu, cela.transform) as GameObject;
#endif
            nvlleTribu.transform.localPosition = new Vector3(position.x, position.y, 0);

            Tribu trib = nvlleTribu.GetComponent<Tribu>();

            trib.idTribu = nbrTribus;
            nvlleTribu.name = "Tribu" + nbrTribus;
        }
    }

    public static void RetirerTribu(Tribu tribuARetirer)
    {
        int pallier = tribuARetirer.idTribu;

        for (int i = 0; i < listeOrdonneeTribus.Length; i++)
        {
            if(listeOrdonneeTribus[i].idTribu > pallier)
            {
                listeOrdonneeTribus[i].idTribu--;
                listeOrdonneeTribus[i].gameObject.name = "Tribu" + listeOrdonneeTribus[i].idTribu;
            }
        }
        if (Application.isPlaying) Destroy(tribuARetirer.gameObject);
        else DestroyImmediate(tribuARetirer.gameObject);

    }
}
