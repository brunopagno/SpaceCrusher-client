using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System;
using System.Text;

public class Communication : MonoBehaviour {

    public Transform observedDot;

    private const string TYPE_NAME = "IHA-SPG0";
    private HostData[] hostList;
    private float time = 0;
    private int identifier = 0;
    
    private enum CommState {
        DISCONNECTED, CONNECTED, STARTED
    }
    CommState commState = CommState.DISCONNECTED;

    private string serializes = "asdasf";

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

    void Update() {
        time += Time.deltaTime;
        if (time > 0.25) {
            time = 0;
            OutMessage("" + identifier + ":pos:" + observedDot.position.x);
        }
    }

    void OnGUI() {
        if (commState == CommState.DISCONNECTED) {
            if (GUI.Button(new Rect(100, 250, 250, 100), "Refresh Hosts")) {
                RefreshHostList();
            }

            if (hostList != null) {
                for (int i = 0; i < hostList.Length; i++) {
                    if (GUI.Button(new Rect(400, 100 + (110 * i), 300, 100), hostList[i].gameName)) {
                        JoinServer(hostList[i]);
                    }
                }
            }
        } else if (commState == CommState.CONNECTED) {
            if (GUILayout.Button("START GAME")) {
                OutMessage("start");
            }
        } else {
            GUILayout.Label(serializes);
        }
    }
}
