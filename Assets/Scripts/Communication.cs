using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System;
using System.Text;

public class Communication : MonoBehaviour {

    public GameObject ship;

    private const string TYPE_NAME = "IHA-SPG0";
    private HostData[] hostList;

    private enum CommState {
        DISCONNECTED, CONNECTED, STARTED
    }
    CommState commState = CommState.DISCONNECTED;

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
        commState = CommState.CONNECTED;
    }

    void OnFailedToConnect(NetworkConnectionError error) {
        Debug.Log("Failed to connect -> " + error.ToString());
    }

    [RPC]
    void OutMessage(string msg) {
        networkView.RPC("InMessage", RPCMode.Server, msg);
    }

    [RPC]
    void InMessage(string msg) {
        Debug.Log("Received message -> " + msg);
        if (msg.Equals("start")) {
            commState = CommState.STARTED;
        }
    }

    void OnGUI() {
        if (commState == CommState.DISCONNECTED) {
            if (GUI.Button(new Rect(100, 250, 250, 100), "Refresh Hosts")) {
                RefreshHostList();
            }

            if (hostList != null) {
                for (int i = 0; i < hostList.Length; i++) {
                    if (GUI.Button(new Rect(400, 100 + (110 * i), 300, 100), hostList[i].gameName))
                        JoinServer(hostList[i]);
                }
            }
        } else if (commState == CommState.CONNECTED) {
            if (GUILayout.Button("START GAME")) {
                OutMessage("start");
            }
        } else {
            //lala =D
        }
    }
}
