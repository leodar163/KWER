using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StockRessource : MonoBehaviour
{
    [SerializeField] private Tribu tribu;

    public float capaciteNourriture;
    public float capacitePeau;
    public float capacitePierre;
    public float capacitePigment;
    public float capaciteOutil;

    public float nourriture;
    public float peau;
    public float pierre;
    public float pigment;
    public float outil;

    public static StockRessource operator +(StockRessource a, ProductionTuile.Production b)
    {
        a.nourriture += b.gainNourriture;
        a.peau += b.gainPeau;
        a.pierre += b.gainPierre;
        a.pigment += b.gainPigment;


        if (a.nourriture > a.capaciteNourriture) a.nourriture = a.capaciteNourriture;
        if (a.peau > a.capacitePeau) a.peau = a.capacitePeau;
        if (a.pierre > a.capacitePierre) a.pierre = a.capacitePierre;
        if (a.pigment > a.capacitePigment) a.pigment = a.capacitePigment;
        if (a.outil > a.capacitePigment) a.pigment = a.capacitePigment;

        return a;
    }

    public static StockRessource operator -(StockRessource a, ProductionTuile.Production b)
    {
        a.nourriture -= b.gainNourriture;
        a.peau -= b.gainPeau;
        a.pierre -= b.gainPierre;
        a.pigment -= b.gainPigment;

        if (a.nourriture < 0) a.nourriture = 0;
        if (a.peau < 0) a.peau = 0;
        if (a.pierre < 0) a.pierre = 0;
        if (a.pigment < 0) a.pigment = 0;
        if (a.outil < 0) a.pigment = 0;

        return a;
    }

    public static StockRessource operator +(StockRessource a, StockRessource b)
    {
        a.nourriture += b.nourriture;
        a.peau += b.peau;
        a.pierre += b.pierre;
        a.pigment += b.pigment;
        a.outil += b.outil;

        if (a.nourriture > a.capaciteNourriture) a.nourriture = a.capaciteNourriture;
        if (a.peau > a.capacitePeau) a.peau = a.capacitePeau;
        if (a.pierre > a.capacitePierre) a.pierre = a.capacitePierre;
        if (a.pigment > a.capacitePigment) a.pigment = a.capacitePigment;
        if (a.outil > a.capacitePigment) a.pigment = a.capacitePigment;
        
        return a;
    }

    public static StockRessource operator -(StockRessource a, StockRessource b)
    {
        a.nourriture -= b.nourriture;
        a.peau -= b.peau;
        a.pierre -= b.pierre;
        a.pigment -= b.pigment;
        a.outil -= b.outil;

        if (a.nourriture < 0) a.nourriture = 0;
        if (a.peau < 0) a.peau = 0;
        if (a.pierre < 0) a.pierre = 0;
        if (a.pigment < 0) a.pigment = 0;
        if (a.outil < 0) a.pigment = 0;

        return a;
    }

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
