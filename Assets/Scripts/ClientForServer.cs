using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System;
using System.Text;

public class ClientForServer : MonoBehaviour {

    private const string TYPE_NAME = "IHA-SPG0";
    private HostData[] hostList;

    private string inmessages = "";

    private void RefreshHostList() {
        MasterServer.RequestHostList(TYPE_NAME);
    }

    void OnMasterServerEvent(MasterServerEvent msEvent) {
        if (msEvent == MasterServerEvent.HostListReceived) {
            hostList = MasterServer.PollHostList();
        }
    }

    private void JoinServer(HostData hostData) {
        Network.Connect(hostData);
    }

    void OnFailedToConnect(NetworkConnectionError error) {
        Debug.Log("Failed to connect -> " + error.ToString());
    }

    [RPC]
    void RPCOut(string info) {
        networkView.RPC("RPCIn", RPCMode.Server, "message");
    }

    [RPC]
    void RPCIn(string info) {
        Debug.Log("Received message -> " + info);
        inmessages += info + "\n";
    }

    void OnGUI() {
        if (GUI.Button(new Rect(100, 250, 250, 100), "Refresh Hosts")) { 
            RefreshHostList();
        }

        //message = GUI.TextField(new Rect(100, 100, 250, 40), message);
        //if (GUI.Button(new Rect(360, 100, 100, 100), "Ofusca, servidor!")) {
        //    //lala 
        //}

        if (hostList != null) {
            for (int i = 0; i < hostList.Length; i++) {
                if (GUI.Button(new Rect(400, 100 + (110 * i), 300, 100), hostList[i].gameName))
                    JoinServer(hostList[i]);
            }
        }
        if (GUILayout.Button("Make magic")) {
            RPCOut("outoutq");
        }
        GUILayout.Label(inmessages);
    }
}
