using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System;
using System.Text;

public class ClientForServer : MonoBehaviour {

    public string ipAddress = "127.0.0.1";
    public int port = 3339;

    IPEndPoint remoteEndPoint;
    UdpClient client;

    string strMessage = "";
    string textii = "";
    string debugue = "";

    void OnGUI() {
        Rect rectObj = new Rect(40, 40, 200, 400);
        GUIStyle style = new GUIStyle();
        style.alignment = TextAnchor.UpperLeft;
        GUI.Box(rectObj, "# UDPSend-Data\n127.0.0.1 " + port + " #\nshell> nc -lu 127.0.0.1  " + port + " \n", style);

        strMessage = GUI.TextField(new Rect(40, 80, 240, 40), strMessage);
        if (GUI.Button(new Rect(290, 80, 100, 40), "send")) {
            sendString(strMessage + "\n");
        }
        ipAddress = GUI.TextField(new Rect(40, 120, 240, 40), ipAddress);
        GUI.Label(new Rect(40, 185, 200, 40), "current ip-> " + textii);
        if (GUI.Button(new Rect(290, 120, 100, 40), "set ip")) {
            remoteEndPoint = new IPEndPoint(IPAddress.Parse(ipAddress), port);
            client = new UdpClient();
            textii = ipAddress;
        }

        GUI.Label(new Rect(40, 220, 300, 200), debugue);
    }

    private void sendString(string message) {
        try {
            Debug.Log("OMG SENDING GODDAMNED DATA => " + message);
            byte[] data = Encoding.UTF8.GetBytes(message);

            client.Send(data, data.Length, remoteEndPoint);
            debugue = "SentData -> " + message + "\n" +
                "Remote -> " + remoteEndPoint.Address + " port ->" + remoteEndPoint.Port;
        } catch (Exception err) {
            Debug.LogError(err.ToString());
        }
    }
}
