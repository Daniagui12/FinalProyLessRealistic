using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static DialogueObject1;
using UnityEngine.Events;
using System;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine.Video;

public class DialogueViewer1 : MonoBehaviour
{
    [SerializeField] Transform parentOfResponses;
    [SerializeField] Button prefab_btnResponse;
    [SerializeField] TextMeshProUGUI txtNodeDisplay;
    public DialogueController1 controller;

    public Node lastNode;
    public string lastNodeTitle;

    
    [DllImport("__Internal")]
    private static extern void openPage(string url);

    private void Start()
    {
        controller.onEnteredNode += OnNodeEntered;
        controller.InitializeDialogue();

        // Start the dialogue
        var curNode = controller.GetCurrentNode();
    }

    public static void KillAllChildren(UnityEngine.Transform parent)
    {
        UnityEngine.Assertions.Assert.IsNotNull(parent);
        for (int childIndex = parent.childCount - 1; childIndex >= 0; childIndex--)
        {
            UnityEngine.Object.Destroy(parent.GetChild(childIndex).gameObject);
        }
    }

    private void OnNodeSelected(int indexChosen)
    {
        Debug.Log("Chose: " + indexChosen);
        controller.ChooseResponse(indexChosen);
    }

    private void OnNodeEntered(Node newNode)
    {
        lastNodeTitle = newNode.title;
        Debug.Log("Entering node: " + newNode.title);
        lastNode = newNode;
        string texto = (newNode.userActions != null) ? (newNode.userActions.Count).ToString() : "0";
        Debug.Log("user actions: " + texto);
        texto = (newNode.simulatorActions != null) ? (newNode.simulatorActions.Count).ToString() : "0";
        Debug.Log("simulator actions: " + texto);
        txtNodeDisplay.text = newNode.text;

        foreach (var action in newNode.simulatorActions)
        {
            GameObject objectF = GameObject.Find(action.object2Action);
            objectF.GetComponent<ObjectController>().MethodAccess(action.actionName, action.actionParams);
        }

        KillAllChildren(parentOfResponses);

        if (newNode.userActions.Count> 0)
        {
            print("HAY ACCIONES DE USUARIO");
            List<bool> comprobaciones = new List<bool>();
            foreach (var action in newNode.userActions)
            {
                GameObject objectF = GameObject.Find(action.object2Action);
                comprobaciones.Add(objectF.GetComponent<ObjectController>().MethodAccessBool(action.actionName, action.actionParams));
            }

            if(comprobaciones.Contains(false))            
            {
                print("no se han cumplido las tareas");
            }
        }
        else
        {
            for (int i = lastNode.responses.Count - 1; i >= 0; i--)
            {
                int currentChoiceIndex = i;
                var response = lastNode.responses[i];
                var responceButton = Instantiate(prefab_btnResponse, parentOfResponses);
                responceButton.GetComponentInChildren<TextMeshProUGUI>().text = response.displayText;
                responceButton.onClick.AddListener(delegate { OnNodeSelected(currentChoiceIndex); });
            }
        }            
    }
            
    public void LoadResponses()
    {
        if (parentOfResponses.childCount == 0)
        {
            for (int i = lastNode.responses.Count - 1; i >= 0; i--)
            {
                int currentChoiceIndex = i;
                var response = lastNode.responses[i];
                var responceButton = Instantiate(prefab_btnResponse, parentOfResponses);
                responceButton.GetComponentInChildren<TextMeshProUGUI>().text = response.displayText;
                responceButton.onClick.AddListener(delegate { OnNodeSelected(currentChoiceIndex); });
            }
        }        
    }

    public void ChangeNode()
    {
        Debug.Log("CHANGE NODE + " + lastNode.title);
        if (parentOfResponses.childCount == 0 && !lastNode.tags.Contains("END"))
        {
            for (int i = lastNode.responses.Count - 1; i >= 0; i--)
            {
                int currentChoiceIndex = i;
                var response = lastNode.responses[i];
                var responceButton = Instantiate(prefab_btnResponse, parentOfResponses);
                responceButton.GetComponentInChildren<TextMeshProUGUI>().text = response.displayText;
                responceButton.onClick.AddListener(delegate { OnNodeSelected(currentChoiceIndex); });
                responceButton.onClick.Invoke();
            }
        }
    }

    public void ChangeNodeIndex(int index)
    {
        Debug.Log("CHANGE NODE index + " + lastNode.title);
        if (parentOfResponses.childCount == 0 && !lastNode.tags.Contains("END"))
        {
            for (int i = lastNode.responses.Count - 1; i >= index; i--)
            {
                int currentChoiceIndex = index;
                if (lastNode.responses != null && !lastNode.tags.Contains("END"))
                {
                    var response = new Response();
                    if (lastNode.responses.Count >= index) response = lastNode.responses[index];
                    else response = lastNode.responses[0];

                    var responceButton = Instantiate(prefab_btnResponse, parentOfResponses);
                    responceButton.GetComponentInChildren<TextMeshProUGUI>().text = response.displayText;
                    responceButton.onClick.AddListener(delegate { OnNodeSelected(currentChoiceIndex); });
                    responceButton.onClick.Invoke();
                }
            }
        }
    }
        
    

    public void DestroyResponses()
    {
        if(parentOfResponses.childCount!=0)
        {
            foreach (Transform child in parentOfResponses)
            {
                Destroy(child.gameObject);
            }
        }        
    }
    
}