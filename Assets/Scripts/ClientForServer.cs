using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System;
using System.Text;

public class ClientForServer : MonoBehaviour {

    public string serverIpAddress = "127.0.0.1";
    public int serverPort = 32154;

    public string errors = "";

    void ConnectToServer() {
        NetworkConnectionError netError = Network.Connect(serverIpAddress, serverPort);
        errors += netError.ToString() + "\n";
    }

    void DisconnectFromServer() {
        Network.Disconnect();
    }

    void OnGUI() {
        serverIpAddress = GUI.TextField(new Rect(40, 80, 240, 40), serverIpAddress);
        GUI.Label(new Rect(40, 150, 240, 80), errors);
        if (Network.peerType == NetworkPeerType.Disconnected) {
            GUILayout.Label("Disconnected");
            if (GUILayout.Button("Connect")) {
                ConnectToServer();
            }
        } else {
            if (Network.peerType == NetworkPeerType.Connecting) {
                GUILayout.Label("Connecting");
            } else {
                GUILayout.Label("Connected");
            }
            if (GUILayout.Button("Disconnect")) {
                DisconnectFromServer();
            }
        }
    }
}
