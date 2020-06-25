using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(UnityEngine.U2D.SpriteShapeController))]
[RequireComponent(typeof(EdgeCollider2D))]
[RequireComponent(typeof(UnityEngine.U2D.SpriteShapeRenderer))]

public class Fleuve : MonoBehaviour
{
    int index = 0;
    public List<NoeudFleuve> grapheNoeuds;
    UnityEngine.U2D.Spline spline;
    UnityEngine.U2D.SpriteShapeController ssController;

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
        ssController = GetComponent<UnityEngine.U2D.SpriteShapeController>();
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
            //print(spline.GetPointCount());
        }
    }

    public void RetirerNoeud(NoeudFleuve nf)
    {
        if(grapheNoeuds.Contains(nf))
        {
            if(grapheNoeuds.Count > 2)
            {
                int index = RecupererIndexSpline(nf.transform.position);
                List<NoeudFleuve> noeudsADeselectionner = grapheNoeuds.GetRange(index, grapheNoeuds.Count-index);
                int portee = grapheNoeuds.Count;
                grapheNoeuds.RemoveRange(index, grapheNoeuds.Count-index);

                if (index > 1)
                {
                    for (int i = index; i < portee; i++)
                    {                           
                        spline.RemovePointAt(index);
                    }
                    
                    foreach(NoeudFleuve noefl in noeudsADeselectionner)
                    {
                        noefl.EstSelectionne(false);
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
}
