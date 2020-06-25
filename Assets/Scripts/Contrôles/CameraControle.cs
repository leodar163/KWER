using System.Collections;
using UnityEngine;

public class CameraControle : MonoBehaviour
{
    private static CameraControle camControleActuel;

    public static CameraControle Actuel
    {
        get
        {
            if (camControleActuel == null)
            {
                camControleActuel = FindObjectOfType<CameraControle>();
            }
            return camControleActuel;
        }
    }

    float vitesse = 10f;
    Vector3 vecteurMove;
    Camera cam;

    public float maxZoom = 10f;
    public float minZoom = 4f;
    [SerializeField] float vitesseZoom = 0.2f;
    [HideInInspector]public bool sourisAccrochee = false;

    ControleSouris controleSouris;

    public bool controlesActives = true;


    private Vector3 positionMemoire;
    private float zoomMemoire;
    public bool camEnMouvmt = false;

    // Start is called before the first frame update
    void Start()
    {
        camControleActuel = this;

        controleSouris = FindObjectOfType<ControleSouris>();
        cam = GetComponent<Camera>();
        vecteurMove = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(controlesActives)
        {
            MoveCamera();
            ZoomCamera();

            BougerAvecSouris();
        }
    }

    #region CONTROLES
    private void MoveCamera()
    {
        vecteurMove = transform.position;

        if(Input.GetKey("q"))
        {
            vecteurMove.x -= 1 * vitesse * Time.deltaTime;  
        }

        if (Input.GetKey("d"))
        {
            vecteurMove.x += 1 * vitesse * Time.deltaTime;
        }

        if (Input.GetKey("z"))
        {
            vecteurMove.y += 1 * vitesse * Time.deltaTime;
        }

        if (Input.GetKey("s"))
        {
            vecteurMove.y -= 1 * vitesse * Time.deltaTime;
        }

        transform.position = vecteurMove;
    }

    private void ZoomCamera()
    {
        float molette = Input.GetAxis("Mouse ScrollWheel");
        
        if (molette < 0 && cam.orthographicSize < maxZoom )
        {
            cam.orthographicSize += vitesseZoom;   
        }

        if (molette > 0 && cam.orthographicSize > minZoom)
        {
            cam.orthographicSize -= vitesseZoom;
        }
    }


    public void BougerAvecSouris()
    { 
        if(sourisAccrochee)
        {
            Vector3 decalage =  Camera.main.ScreenToWorldPoint(Input.mousePosition) - controleSouris.pointAccrocheSouris;
            transform.position -= decalage;
        }
    }
    #endregion

    #region MOUVEMENTSCAMERA
    public void CentrerCamera(Vector3 point)
    {
        if(!camEnMouvmt)
        {
            EnregistrerPosition();

            point.z = transform.position.z;

            StartCoroutine(CentrageCamera(point, cam.orthographicSize));
        }
    }

    public void CentrerCamera(Vector3 point, float pointZoom)
    {
        if (!camEnMouvmt)
        {
            EnregistrerPosition();

            point.z = transform.position.z;

            StartCoroutine(CentrageCamera(point, pointZoom));
        }
    }

    public void CentrerCamera(Vector3 point, bool modeSelection)
    {
        if (!camEnMouvmt)
        {
            controlesActives = false;
            EnregistrerPosition();

            controlesActives = !modeSelection;
            point.z = transform.position.z;

            StartCoroutine(CentrageCamera(point, 1.5f));
        }
    }

    public void ReinitCamera()
    {
        if (!camEnMouvmt)
        {
            if (zoomMemoire != 0 && positionMemoire != Vector3.zero)
            {
                controlesActives = true;
                StartCoroutine(CentrageCamera(positionMemoire, zoomMemoire));
            }
        }
    }

    private void EnregistrerPosition()
    {
        positionMemoire = transform.position;
        zoomMemoire = cam.orthographicSize;
    }
    

    private IEnumerator CentrageCamera(Vector3 point, float pointZoom)
    {
        camEnMouvmt = true;
        float tempsKiPasse = 0;
        while(transform.position != point)
        {
            transform.position = Vector3.Lerp(transform.position, point, tempsKiPasse);
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, pointZoom, tempsKiPasse);

            tempsKiPasse += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        camEnMouvmt = false;
    }

    private IEnumerator CentrageCamera(Vector3 point, float pointZoom, float multiplicateurVitesse)
    {
        camEnMouvmt = true;
        float tempsKiPasse = 0;
        while (transform.position != point)
        {
            transform.position = Vector3.Lerp(transform.position, point, tempsKiPasse);
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, pointZoom, tempsKiPasse);

            tempsKiPasse += Time.deltaTime * multiplicateurVitesse;
            yield return new WaitForEndOfFrame();
        }
        camEnMouvmt = false;
    }
    #endregion
}
