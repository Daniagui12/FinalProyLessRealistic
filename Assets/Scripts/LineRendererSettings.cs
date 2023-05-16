using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineRendererSettings : MonoBehaviour
{
    //Declare a LineRenderer to store the component attached to the GameObject. 
    [SerializeField]
    LineRenderer rend;
    //Settings for the LineRenderer are stored as a Vector3 array of points. Set up a V3 array to 
    //initialize in Start. 
    Vector3[] points;
    public Image img;
    public Button btn;


    //Start is called before the first frame update
    void Start() {
        //get the LineRenderer attached to the gameobject. 
        rend = gameObject.GetComponent<LineRenderer>();
        //initialize the LineRenderer 
        points = new Vector3[2];
        //set the start point of the linerenderer to the position of the gameObject. 
        points[0] = Vector3.zero;
        //set the end point 20 units away from the GO on the Z axis (pointing forward) 
        points[1] = transform.position + new Vector3(0, 0, 20);
        //finally set the positions array on the LineRenderer to our new values 
        rend.SetPositions(points);
        rend.enabled = true;
        
    }

    private void Update()
    {
        AlignLineRenderer(rend);

        if(AlignLineRenderer(rend) && Input.GetAxis("Submit") > 0) 
        { 
        btn.onClick.Invoke(); 
        }
        
    }

    public LayerMask layerMask;
    public bool AlignLineRenderer(LineRenderer rend)
    {
        bool hitbtn = false;

        Ray ray;
        ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, layerMask))
        {
            hitbtn = true;
            rend.startColor = Color.red;
            rend.endColor = Color.red;
            points[1] = transform.forward + new Vector3(0, 0, hit.distance);
            btn = hit.collider.gameObject.GetComponent<Button>();
        }
        else
        {
            hitbtn = false;
            rend.startColor = Color.green;
            rend.endColor = Color.green;
            points[1] = transform.forward + new Vector3(0, 0, 20);
        }
        rend.SetPositions(points);
        rend.material.color = rend.startColor;
        return hitbtn;
    }

    public void ColorChangeOnClick()
    {
        if (btn != null)
        {
            if (btn.name == "Pregunta1")
            {
                print("se hizo clic en el primer boton");
            }
            else if (btn.name == "Button (1)")
            {
                print("se hizo clic en el segundo boton");
            }
            else if (btn.name == "Button (2)")
            {
                print("se hizo clic en el tercer boton");
            }
        }
    }
    
}
