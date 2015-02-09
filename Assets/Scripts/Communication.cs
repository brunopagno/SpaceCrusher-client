using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

public class Communication : MonoBehaviour {

    private const string TYPE_NAME = "IHA-SPG0";
    private HostData[] hostList;
    private string PID;

    public GameObject ship;

    private bool gameStarted = false;
    public bool connected = false;

    void Awake() {
        DontDestroyOnLoad(transform.gameObject);
    }

    private void RefreshHostList() {
        // /* Comente se não for usar master server local
        MasterServer.ipAddress = "143.54.13.238";
        MasterServer.port = 23466;
        // */
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
    public void LaunchBomb(string msg) {
        string message = string.Format("{0}", PID);
        networkView.RPC("LaunchBomb", RPCMode.Server, message);
    }

    #region APIcompleters

    // Métodos que só servem para API do Unity funcionar. Se eles não existirem acontece erro.

    [RPC]
    void MovePlayer(string _message) { }
    [RPC]
    void ChangeGun(string _message) { }
    [RPC]
    void PassarArminhaProAmiguinho(string _message) { }
    [RPC]
    void PassarVidaProAmiguinho(string _message) { }

    #endregion

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
    void RPCStart(string nothing) { }

    #endregion

    void OnGUI() {
        if (!gameStarted) {
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

}
