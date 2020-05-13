using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEditor;
using UnityEditor.U2D;
using UnityEditor.U2D.SpriteShape;

[RequireComponent(typeof(SpriteShapeController))]
[RequireComponent(typeof(EdgeCollider2D))]
[RequireComponent(typeof(SpriteShapeRenderer))]

public class Fleuve : MonoBehaviour
{
    int index = 0;
    public List<NoeudFleuve> grapheNoeuds;
    Spline spline;
    SpriteShapeController ssController;

    const float profondeur = -2;

    

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(testSpline());
    }

    // Update is called once per frame
    void Update()
    {
        //ChangerSpline();
    }

    public void Init()
    {
        grapheNoeuds = new List<NoeudFleuve>();
        ssController = GetComponent<SpriteShapeController>();
        spline = ssController.spline;
    }

    public void AjouterNoeud(NoeudFleuve nf)
    {
        grapheNoeuds.Add(nf);
        nf.EstSelectionne(true);

        if (grapheNoeuds.Count == 2)
        {
            transform.position = Vector3.zero;
            for (int i = 0; i < grapheNoeuds.Count; i++)
            {
                Vector3 positionNoeud = grapheNoeuds[i].transform.position;
                positionNoeud.z = profondeur;
                
                    spline.InsertPointAt(i, positionNoeud);
                    spline.RemovePointAt(i + 1);
            }
        }
        else if(grapheNoeuds.Count > 2)
        {
            Vector3 positionNoeud = grapheNoeuds[grapheNoeuds.Count - 1].transform.position;
            positionNoeud.z = profondeur;
            spline.InsertPointAt(spline.GetPointCount(), positionNoeud);
        }
    }

    public void RetirerNoeud(NoeudFleuve nf)
    {
        if(grapheNoeuds.Contains(nf))
        {
            if(grapheNoeuds.Count > 2)
            {
                int index = RecupererIndexSpline(nf.transform.position);

                if(index >= 0)
                {
                    for (int i = index; i < grapheNoeuds.Count; i++)
                    {
                        grapheNoeuds[i].EstSelectionne(false);
                        grapheNoeuds.RemoveAt(i);

                        spline.RemovePointAt(i);
                    }
                }
            }
        }
    }

    private int RecupererIndexSpline(Vector3 position)
    {
        position.z = profondeur;
        for (int i = 0; i < grapheNoeuds.Count; i++)
        {
            if(spline.GetPosition(i) == position)
            {
                return i;
            }
        }

        return -1;
    }

    public void EstSelectionne(bool selectionne)
    {
        foreach(NoeudFleuve nf in grapheNoeuds)
        {
            nf.EstSelectionne(selectionne);
        }
    }

    public void ChangerSpline()
    {
        SpriteShapeController ssController = GetComponent<SpriteShapeController>();
        EdgeCollider2D edgeCollider = GetComponent<EdgeCollider2D>();

        Spline spline = ssController.spline;

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            position -= gameObject.transform.position;
            position.z = gameObject.transform.position.z;
            Debug.DrawLine(spline.GetPosition(1), position, Color.red, 0.01f);
            //spline.SetPosition(0, position);
            int tailleSpline = spline.GetPointCount();
            spline.InsertPointAt(tailleSpline , position);
        }
        
    }

    

}
