﻿using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using System.IO;

public class Communication : MonoBehaviour {

    private const string TYPE_NAME = "IHA-SPG0";
    private HostData[] hostList;
    private string PID;

    public GameObject ship;

    private bool gameStarted = false;
    public bool connected = false;

    public bool vibractionActive = true;
    private string ipAddress = "0.0.0.0";

    void Awake() {
        DontDestroyOnLoad(transform.gameObject);
        if (File.Exists(Application.persistentDataPath + "/ipaddr.txt")) {
            string ip = File.ReadAllText(Application.persistentDataPath + "/ipaddr.txt");
            if (ip != null) {
                ipAddress = ip;
            }
        }
    }

    private void RefreshHostList() {
        MasterServer.ipAddress = ipAddress;
        MasterServer.port = 23466;
        MasterServer.RequestHostList(TYPE_NAME);
    }

    void OnMasterServerEvent(MasterServerEvent msEvent) {
        if (msEvent == MasterServerEvent.HostListReceived) {
            hostList = MasterServer.PollHostList();
        }
    }

    private void JoinServer(HostData hostData) {
        Network.Connect(hostData);
        connected = true;

        StreamWriter writer = File.CreateText(Application.persistentDataPath + "/ipaddr.txt");
        writer.Write(ipAddress);
        writer.Flush();
        writer.Close();
    }

    void OnFailedToConnect(NetworkConnectionError error) {
        Debug.Log("Failed to connect -> " + error.ToString());
    }

    #region RPC Out

    [RPC]
    public void SyncPosition(string _position) {
        string message = string.Format("{0}:{1}", PID, _position);
        networkView.RPC("SyncPosition", RPCMode.Server, message);
    }

    [RPC]
    public void SyncChangedItem(string _gun) {
        string message = string.Format("{0}:{1}", PID, _gun);
        networkView.RPC("SyncChangedItem", RPCMode.Server, message);
    }

    [RPC]
    public void MissedClicks(string msg) {
        string message = string.Format("{0}:{1}", PID, msg);
        networkView.RPC("MissedClicks", RPCMode.Server, message);
    }

    [RPC]
    public void LaunchBomb(string msg) {
        string message = string.Format("{0}", PID);
        networkView.RPC("LaunchBomb", RPCMode.Server, message);
    }

    #endregion

    #region RPC In

    [RPC]
    void RPCConnect(string info) {
        ProcessMessageIn(info);
    }

    [RPC]
    void SetBulletsGun2(string _message) {
        Match m = Regex.Match(_message, "\\d*:\\d*");
        if (m.Success) {
            string destPID = _message.Split(':')[0];
            if (destPID == PID) {
                int bullets;
                int.TryParse(_message.Split(':')[1], out bullets);
                GameObject bulletsGun2 = GameObject.FindGameObjectWithTag("gun2");
                bulletsGun2.GetComponent<ItemControl>().Amount = bullets;
            }
        }
    }

    [RPC]
    void SetBulletsGun3(string _message) {
        Match m = Regex.Match(_message, "\\d*:\\d*");
        if (m.Success) {
            string destPID = _message.Split(':')[0];
            if (destPID == PID) {
                int bullets;
                int.TryParse(_message.Split(':')[1], out bullets);
                GameObject bulletsGun3 = GameObject.FindGameObjectWithTag("gun3");
                bulletsGun3.GetComponent<ItemControl>().Amount = bullets;
            }
        }
    }

    [RPC]
    void SetBulletsSpecial(string _message) {
        Match m = Regex.Match(_message, "\\d*:\\d*");
        if (m.Success) {
            string destPID = _message.Split(':')[0];
            if (destPID == PID) {
                int bullets;
                int.TryParse(_message.Split(':')[1], out bullets);
                GameObject bulletsSpecial = GameObject.FindGameObjectWithTag("gunSpecial");
                bulletsSpecial.GetComponent<BombButton>().Amount = bullets;
            }
        }
    }

    [RPC]
    public void FreeBombMode(string message) {
        if (PID.Equals(message)) {
            GameObject bulletsSpecial = GameObject.FindGameObjectWithTag("gunSpecial");
            bulletsSpecial.GetComponent<BombButton>().freeBomb = true;
        }
    }

    [RPC]
    public void SyncScore(string _message) {
        Match m = Regex.Match(_message, "\\d*:\\d*");
        if (m.Success) {
            string destPID = _message.Split(':')[0];
            if (destPID == PID) {
                int score;
                int.TryParse(_message.Split(':')[1], out score);
                GameObject scoreText = GameObject.FindGameObjectWithTag("score");
                scoreText.GetComponent<Score>().SetScore(score);
            }
        }
    }

    [RPC]
    void SetLife(string _message) {
        Match m = Regex.Match(_message, "\\d*:\\d*");
        if (m.Success) {
            string destPID = _message.Split(':')[0];
            if (destPID == PID) {
                int life;
                int.TryParse(_message.Split(':')[1], out life);
                GameObject lifeText = GameObject.FindGameObjectWithTag("life");
                lifeText.GetComponent<Life>().Amount = life;
            }
        }
    }

    [RPC]
    public void SpeedReduction(string message) {
        string[] msg = message.Split(':');
        if (msg[0] == PID) {
            bool reduce = msg[1].Equals("yes");
            ship.GetComponent<Ship>().SpeedReduction(reduce);
        }
    }

    [RPC]
    public void InitialConfig(string message) {
        string[] msg = message.Split('|');
        foreach (string command in msg) {
            if (command.StartsWith("vibration")) {
                string param = command.Split(':')[1];
                vibractionActive = param.Equals("true", System.StringComparison.InvariantCultureIgnoreCase);
            } else if (command.StartsWith("button_size")) {
                string param = command.Split(':')[1];
                float bs = 1;
                float.TryParse(param, out bs);
                SetButtons(bs);
            }
        }
    }

    [RPC]
    void RPCStart(string nothing) { }

    #endregion

    void OnGUI() {
        if (!gameStarted) {
            ipAddress = GUI.TextField(new Rect(Screen.width - 120, 10, 110, 30), ipAddress);
            if (GUI.Button(new Rect(10, 10, 250, 100), "Refresh Hosts")) {
                RefreshHostList();
            }
            if (hostList != null) {
                for (int i = 0; i < hostList.Length; i++) {
                    if (GUI.Button(new Rect(10, 120 + (110 * i), 300, 100), hostList[i].gameName))
                        JoinServer(hostList[i]);
                }
            }
            if (connected) {
                if (GUI.Button(new Rect(270, 10, 250, 100), "StartGame")) {
                    networkView.RPC("RPCStart", RPCMode.Server, string.Empty);
                    gameStarted = true;
                }
            }
        }
        if (!string.IsNullOrEmpty(PID)) {
            GUILayout.Label("My id is: " + PID);
        }

        //foreach (TouchBehaviour tb in GameObject.FindObjectsOfType<TouchBehaviour>()) {
        //    GUILayout.Label(tb.name + "=>" + tb.counter);
        //}
    }

    void ProcessMessageIn(string _message) {
        Match m = Regex.Match(_message, "PID:\\d*");
        if (m.Success) {
            PID = _message.Split(':')[1];
            Color color = Color.white;
            switch (PID) {
                case "1":
                    color = Color.blue; break;
                case "2":
                    color = Color.red; break;
                case "3":
                    color = Color.green; break;
                case "4":
                    color = Color.yellow; break;
                case "5":
                    color = Color.magenta; break;
                case "6":
                    color = Color.cyan; break;
            }
            if (ship != null) {
                ship.GetComponent<Ship>().SetOriginalColor(color);
            }
        }
    }

    private void SetButtons(float size) {
        GameObject.Find("Weapon2").transform.localScale = new Vector3(size, size, 1);
        GameObject.Find("Weapon3").transform.localScale = new Vector3(size, size, 1);
        GameObject.Find("Lightning").transform.localScale = new Vector3(size, size, 1);
        GameObject.Find("ButtonRight").transform.localScale = new Vector3(size, size, 1);
        GameObject.Find("ButtonLeft").transform.localScale = new Vector3(size, size, 1);
        GameObject.Find("LaunchBomb").transform.localScale = new Vector3(size, size, 1);
    }
}
