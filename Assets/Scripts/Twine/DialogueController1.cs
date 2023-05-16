using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DialogueObject1;

public class DialogueController1 : MonoBehaviour {

    [SerializeField] public TextAsset twineText;
    Dialogue curDialogue;
    Node curNode;

    public delegate void NodeEnteredHandler( Node node );
    public event NodeEnteredHandler onEnteredNode;

    public Node GetCurrentNode() {
        return curNode;
    }

    public void InitializeDialogue() {
        curDialogue = new Dialogue( twineText );
        curNode = curDialogue.GetStartNode();
        onEnteredNode( curNode );
    }

    public void InitializeDialogueCurrentNode(string title)
    {
        curDialogue = new Dialogue(twineText);
        curNode = curDialogue.GetCurrentNode(title);
        onEnteredNode(curNode);
    }

    public List<Response> GetCurrentResponses() {
        return curNode.responses;
    }

    public void ChooseResponse( int responseIndex ) {
        string nextNodeID = curNode.responses[responseIndex].destinationNode;
        Node nextNode = curDialogue.GetNode(nextNodeID);
        curNode = nextNode;
        onEnteredNode( nextNode );
    }
}