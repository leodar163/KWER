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

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(testSpline());
    }

    // Update is called once per frame
    void Update()
    {
        ChangerSpline();
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

    public void MiseAJourCollider()
    {
        
    }

    IEnumerator testSpline()
    {
        SpriteShapeController ssController = GetComponent<SpriteShapeController>();
        EdgeCollider2D edgeCollider = GetComponent<EdgeCollider2D>();

        

        Spline spline = ssController.spline;
        
        

        int tailleSpline = spline.GetPointCount();

        for (int i = 0; i < 200; i++)
        {
            Vector3 position = spline.GetPosition(0);
            position.y += 0.01f;
            spline.SetPosition(0, position);

            yield return new WaitForSeconds(0.01f);
        }
    }

}
