using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class ObjectController : MonoBehaviour
{
    public Object modelObject;

    public void MethodAccess(string methodName, string args)
    {
        var modelObjectScript = modelObject.GetType();
        print("TIPO!!!:: " + modelObjectScript);
        var loadingMethod = modelObjectScript.GetMethod(methodName);
        print("LOADING METHOD !!:: " + loadingMethod);
        if(loadingMethod!=null)
        {
            //var clase = new SphereControl();
            if(args=="")
            {
                loadingMethod.Invoke(modelObject, System.Type.EmptyTypes);
            }
            else
            {
                var splitArgs = args.Split(';');
                //var arguments = new object[] { splitArgs };
                loadingMethod.Invoke(modelObject, splitArgs);
            }
        }
        else
        {
            Debug.Log("No existe un método llamado "+ methodName + ", en el objeto ");
        }
    }

    public bool MethodAccessBool(string methodName, string args)
    {
        var modelObjectScript = modelObject.GetType();
        print("TIPO!!!:: " + modelObjectScript);
        var loadingMethod = modelObjectScript.GetMethod(methodName);
        print("LOADING METHOD !!:: " + loadingMethod);
        if (loadingMethod != null)
        {
            if (args == "")
            {
                return (bool)loadingMethod.Invoke(modelObject, System.Type.EmptyTypes);
            }
            else
            {
                var splitArgs = args.Split(';');
                //var arguments = new object[] { splitArgs };
                return (bool)loadingMethod.Invoke(modelObject, splitArgs);
            }
        }
        else
        {
            Debug.Log("No existe un método llamado " + methodName + ", en el objeto ");
            return true;
        }
    }

}

