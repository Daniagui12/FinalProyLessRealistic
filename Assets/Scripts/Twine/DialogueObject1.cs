using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueObject1 {
    private const string kStart = "START";
    private const string kEnd = "END";
    private const string kValidate = "VALIDACION";

    public struct Response {
        public string displayText;
        public string destinationNode;

        public Response( string display, string destination ) {
            displayText = display;
            destinationNode = destination;
        }
    }

    public struct Action
    {
        public string object2Action;
        public string actionName;
        public string actionParams;

        public Action(string objectN, string nameP, string paramsP)
        {
            object2Action = objectN;
            actionName = nameP;
            actionParams = paramsP;
        }
    }

    public class Node {
        public string title;
        public string text;
        public List<string> tags;
        public List<Action> userActions;
        public List<Action> simulatorActions;
        public List<Response> responses;
        public string audio;

        internal bool IsEndNode() {
            return tags.Contains( kEnd );
        }

        // TODO proper override
        public string Print() {
            return "";//string.Format( "Node {  Title: '%s',  Tag: '%s',  Text: '%s'}", title, tag, text );
        }

    }

    public class Dialogue {
        string title;
        Dictionary<string, Node> nodes;
        string titleOfStartNode;
        string titleLastNode;
        public Dialogue( TextAsset twineText ) {
            nodes = new Dictionary<string, Node>();
            ParseTwineText( twineText );
        }

        public Node GetNode( string nodeTitle ) {
            Debug.Log(nodeTitle);
            return nodes [ nodeTitle ];
        }

        public Node GetStartNode() {
            UnityEngine.Assertions.Assert.IsNotNull( titleOfStartNode );
            return nodes [ titleOfStartNode ];
        }

        //Ver el nodo actual
        public Node GetCurrentNode(string title)
        {
            UnityEngine.Assertions.Assert.IsNotNull(title);
            return nodes[title];
        }

        public void ParseTwineText( TextAsset twineText ) {
            string text = twineText.text;
            string[] nodeData = text.Split(new string[] { "::" }, StringSplitOptions.None);

            const int kIndexOfContentStart = 4;
            for ( int i = 0; i<nodeData.Length; i++ ) {
                if ( i < kIndexOfContentStart )
                    continue;

                // Note: tags are optional
                // Normal Format: "NodeTitle [Tags, comma, seperated] \r\n Message Text \r\n [[Response One]] \r\n [[Response Two]]"
                // No-Tag Format: "NodeTitle \r\n Message Text \r\n [[Response One]] \r\n [[Response Two]]"
                string currLineText = nodeData[i];
                bool tagsPresent = currLineText.IndexOf( "[" ) < currLineText.IndexOf( "\r\n" );
                int endOfFirstLine = currLineText.IndexOf( "\r\n" );

                int startOfResponses = -1;
                int startOfResponseDestinations = currLineText.IndexOf( "[[" );
                bool lastNode = (startOfResponseDestinations == -1);
                if ( lastNode )
                    startOfResponses = currLineText.Length;
                else {
                    // Last new line before "[["
                    startOfResponses = currLineText.Substring( 0, startOfResponseDestinations ).LastIndexOf( "\r\n" );
                }

                // Extract Title
                int titleStart = 0;
                int titleEnd = tagsPresent
                    ? currLineText.IndexOf( "[" )
                    : endOfFirstLine;
                string title = currLineText.Substring(titleStart, titleEnd).Trim();

                // Extract Tags (if any)
                string tags = tagsPresent
                    ? currLineText.Substring( titleEnd + 1, (endOfFirstLine - titleEnd)-2)
                    : "";

                // Extract Message Text & Responses
                string bodyNode = currLineText.Substring( endOfFirstLine, startOfResponses - endOfFirstLine).Trim();
                string responseText = currLineText.Substring( startOfResponses ).Trim();

                // Extract UserActions
                string userActionsText = bodyNode.Substring(bodyNode.IndexOf("{") + 1);
                userActionsText = userActionsText.Split('}')[0].Trim();            

                Debug.Log("USER ACTIONS  "+ userActionsText);
                
                    // Extract SimulatorActions
                string simulatorActionsText = bodyNode.Substring(bodyNode.IndexOf("<") + 1);
                simulatorActionsText = simulatorActionsText.Split('>')[0].Trim();
                
                Debug.Log("SIMULATOR ACTIONS  "+ simulatorActionsText);

                // Extract Message Text
                string message = "";
                if (bodyNode.Split('>').Length > 0) message = bodyNode.Split('>')[1];
                Debug.Log("MESSAGE  "+message);

                Node curNode = new Node();
                curNode.title = title;
                curNode.text = message;
                curNode.tags = new List<string>( tags.Split( new string [] { " " }, StringSplitOptions.None ) );

                if ( curNode.tags.Contains( kStart ) ) {
                    UnityEngine.Assertions.Assert.IsTrue( null == titleOfStartNode );
                    titleOfStartNode = curNode.title;
                }
                
                // user actions
                curNode.userActions = new List<Action>();
                if (userActionsText != null)
                {
                    string[] ActionData = userActionsText.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                    if (ActionData.Length > 0)
                    {
                        for (int k = 0; k < ActionData.Length; k++)
                        {
                            string curActionData = ActionData[k];
                            if (curActionData != "")
                            {
                                Action curAction = new Action();

                                string[] splitSepParams = curActionData.Split('(');
                                string[] splitObjectName = splitSepParams[0].Split('.');

                                curAction.object2Action = splitObjectName[0];
                                //Debug.Log("OBJECT = "+curAction.object2Action);

                                curAction.actionName = splitObjectName[1];
                                //Debug.Log("NAME = "+curAction.actionName);

                                int paramsEnd = splitSepParams[1].IndexOf(")");
                                curAction.actionParams = splitSepParams[1].Substring(0, paramsEnd).Replace("\"", "");
                                //Debug.Log("PARAMS = "+curAction.actionParams);

                                curNode.userActions.Add(curAction);
                            }
                        }
                    }
                }

                /**
                if (curNode.userActions.Count>0)
                {
                    UnityEngine.Assertions.Assert.IsTrue(null == titleLastNode);
                    titleLastNode = curNode.title;
                }
    **/

                // simulator actions
                curNode.simulatorActions = new List<Action>();
                if (simulatorActionsText != null)
                {
                    string[] ActionDataSim = simulatorActionsText.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                    if (ActionDataSim.Length > 0)
                    {
                        for (int k = 0; k < ActionDataSim.Length; k++)
                        {
                            string curActionDataSim = ActionDataSim[k];
                            if (curActionDataSim != "")
                            {
                                Action curAction = new Action();

                                string[] splitSepParams = curActionDataSim.Split('(');
                                string[] splitObjectName = splitSepParams[0].Split('.');

                                curAction.object2Action = splitObjectName[0];
                                //Debug.Log("OBJECT = "+curAction.object2Action);

                                curAction.actionName = splitObjectName[1];
                                //Debug.Log("NAME = "+curAction.actionName);

                                int paramsEnd = splitSepParams[1].IndexOf(")");
                                curAction.actionParams = splitSepParams[1].Substring(0, paramsEnd).Replace("\"", ""); ;
                                //Debug.Log("PARAMS = "+curAction.actionParams);

                                curNode.simulatorActions.Add(curAction);
                            }
                        }
                    }
                }

                // Note: response messages are optional (if no message then destination is the message)
                // With Message Format: "\r\n Message[[Response One]]"
                // Message-less Format: "\r\n [[Response One]]"
                curNode.responses = new List<Response>();
                if ( !lastNode ) {
                    List<string> responseData = new List<string>(responseText.Split( new string [] { "\r\n" }, StringSplitOptions.None ));
                    for ( int k = responseData.Count-1; k >= 0; k-- ) {
                        string curResponseData = responseData[k];

                        if ( string.IsNullOrEmpty( curResponseData ) ) {
                            responseData.RemoveAt( k );
                            continue;
                        }

                        // If message-less, then destination is the message
                        Response curResponse = new Response();
                        int destinationStart = curResponseData.IndexOf( "[[");
                        int destinationEnd = curResponseData.IndexOf( "]]");
                        string destination = curResponseData.Substring(destinationStart + 2, (destinationEnd - destinationStart)-2);
                        curResponse.destinationNode = destination;
                        if ( destinationStart == 0 )
                            curResponse.displayText = destination;
                        else
                            curResponse.displayText = curResponseData.Substring( 0, destinationStart );
                        curNode.responses.Add( curResponse );
                    }
                }

                nodes [ curNode.title ] = curNode;
            }
        }
    }
    
}
