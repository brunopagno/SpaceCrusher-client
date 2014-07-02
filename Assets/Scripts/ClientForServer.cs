using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System;
using System.Text;

public class ClientForServer : MonoBehaviour {

    private string ipAddress = "127.0.0.1";
    public int port = 3339;

    IPEndPoint remoteEndPoint;
    UdpClient client;

    string strMessage = "";

    void OnGUI() {
        Rect rectObj = new Rect(40, 380, 200, 400);
        GUIStyle style = new GUIStyle();
        style.alignment = TextAnchor.UpperLeft;
        GUI.Box(rectObj, "# UDPSend-Data\n127.0.0.1 " + port + " #\nshell> nc -lu 127.0.0.1  " + port + " \n", style);

        strMessage = GUI.TextField(new Rect(40, 420, 140, 20), strMessage);
        if (GUI.Button(new Rect(190, 420, 40, 20), "send")) {
            sendString(strMessage + "\n");
        }
    }

    void Start() {
        print("UDPSend.init()");

        remoteEndPoint = new IPEndPoint(IPAddress.Parse(ipAddress), port);
        client = new UdpClient();

        print("Sending to " + ipAddress + " : " + port);
        print("Testing: nc -lu " + ipAddress + " : " + port);
    }

    private void sendString(string message) {
        try {
            Debug.Log("OMG SENDING GODDAMNED DATA, CARALHO => " + message);
            byte[] data = Encoding.UTF8.GetBytes(message);

            client.Send(data, data.Length, remoteEndPoint);
        } catch (Exception err) {
            print(err.ToString());
        }
    }
}
