using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class MoveCharWithAnimation : MonoBehaviour
{
    public GameObject pathBase;
    public Vector3 offset;
    public int speed;
    public bool cicle;
    public List<Vector3> path;
    public bool walking = false;

    private Hashtable moveHash = new System.Collections.Hashtable();
    void Start()
    {
        moveHash.Add("easeType", iTween.EaseType.linear);
        moveHash.Add("speed", speed);
        moveHash.Add("onstart", "move");
        moveHash.Add("orienttopath", true);
        moveHash.Add("looktime", 1);
        path = new List<Vector3>();
        foreach (Transform child in pathBase.transform)
        {
            path.Add(child.transform.position + offset);
        }
        moveHash.Remove("path");
        moveHash.Add("path", path.ToArray());
    }
    public async Task<bool> StartMove()
    {
        print("entro en el startmove");
        walking = true;
        if (cicle)
        {
            moveHash.Remove("oncomplete");
            moveHash.Add("oncomplete", "idlecicle");
        }
        else
        {
            moveHash.Remove("oncomplete");
            moveHash.Add("oncomplete", "idle");
        }
        iTween.MoveTo(gameObject, moveHash);
        int sum = 0;
        foreach (Transform child in pathBase.transform)
        {
            sum += (int)(Vector3.Distance(child.transform.position, gameObject.transform.position) / speed * 1000);
        }
        await Task.Delay(sum);
        return true;
    }
    public void StopMove()
    {
        print("entre en STOP MOVE ");
        if(FindObjectOfType<ControllerTwine>()!=null && !FindObjectOfType<ControllerTwine>().impideMirarObjetivo) walking = false;
        if(FindObjectOfType<ControllerTwine>() == null) walking = false;
        iTween.Stop(gameObject);
        
    }
    public void StopCicle()
    {
        print("entre en STOP CYCLE");
        walking = true;
        iTween.Pause(gameObject);
    }
    void move()
    {
        gameObject.GetComponent<Animator>().SetTrigger("Caminar");
    }
    void idle()
    {
        walking = false;
        gameObject.GetComponent<Animator>().SetTrigger("Habla");
    }
    async void idlecicle()
    {
        await StartMove();
        //gameObject.GetComponent<Animator>().SetTrigger("Caminar");
    }
    public void setPathBase(GameObject go)
    {
        pathBase = go;
        path = new List<Vector3>();
        foreach (Transform child in pathBase.transform)
        {
            path.Add(child.transform.position + offset);
        }
        moveHash.Remove("path");
        moveHash.Add("path", path.ToArray());
    }

}