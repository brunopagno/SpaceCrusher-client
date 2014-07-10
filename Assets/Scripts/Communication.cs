using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

public class Communication : MonoBehaviour {

    private const string TYPE_NAME = "IHA-SPG0";
    private HostData[] hostList;
    public GUIText text;
    private string PID;

    public GUITexture shipTexture;
    public GUITexture elipseTexture;

    private bool isActive;
    public bool IsActive {
        get { return isActive; }
    }

    private bool gameStarted = false;
    private bool connected = false;

    void Awake() {
        DontDestroyOnLoad(transform.gameObject);
    }

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
        connected = true;
        isActive = true;
    }

    void OnFailedToConnect(NetworkConnectionError error) {
        Debug.Log("Failed to connect -> " + error.ToString());
    }

    #region RPC Out
    [RPC]
    public void RPCOut(string info) {
        networkView.RPC("RPCIn", RPCMode.Server, info);
    }

    [RPC]
    public void SendPosition(string _position) {
        string message = string.Format("{0}:{1}", PID, _position);
        //networkView.RPC ("RPCIn", RPCMode.Server, message);
        networkView.RPC("MovePlayer", RPCMode.Server, message);
    }
    [RPC]
    void MovePlayer(string _message) { }

    [RPC]
    public void SendChangedGun(string _gun) {
        string message = string.Format("{0}:{1}", PID, _gun);
        //networkView.RPC ("RPCIn", RPCMode.Server, message);
        networkView.RPC("ChangeGun", RPCMode.Server, message);
    }
    [RPC]
    void ChangeGun(string _message) { }

    [RPC]
    public void SendRouletResult(string _result) {
        string message = string.Format("{0}:{1}", PID, _result);
        //networkView.RPC ("RPCIn", RPCMode.Server, message);
        networkView.RPC("RouletResult", RPCMode.Server, message);
    }
    [RPC]
    void RouletResult(string _message) { }

    #endregion

    #region RPC In
    [RPC]
    void RPCIn(string info) {
        ProcessMessageIn(info);
        //text.text = info;
        //GameObject life = GameObject.FindGameObjectWithTag("life");
        //life.GetComponent<Life>().setLife (5);
        //GameObject secondPlayer = GameObject.FindGameObjectWithTag("secondPlayer");
        //secondPlayer.GetComponent<SecondPlayer>().MoveSecondPlayer(info);
        Debug.Log("Received message -> " + info);
        //inmessages += info + "\n";
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
                lifeText.GetComponent<Life>().setLife(life);
            }
        }
    }

    [RPC]
    void SetBulletsGun2(string _message) {
        Match m = Regex.Match(_message, "\\d*:\\d*");
        if (m.Success) {
            string destPID = _message.Split(':')[0];
            if (destPID == PID) {
                int bullets;
                int.TryParse(_message.Split(':')[1], out bullets);
                GameObject bulletsGun2Text = GameObject.FindGameObjectWithTag("bulletsGun2");
                bulletsGun2Text.GetComponent<Bullets>().setBullets(bullets);
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
                GameObject bulletsGun3Text = GameObject.FindGameObjectWithTag("bulletsGun3");
                bulletsGun3Text.GetComponent<Bullets>().setBullets(bullets);
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
                GameObject bulletsSpecialText = GameObject.FindGameObjectWithTag("bulletsSpecial");
                bulletsSpecialText.GetComponent<Bullets>().setBullets(bullets);
            }
        }
    }

    [RPC]
    void RPCStart(string nothing) { }

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
    }

    void ProcessMessageIn(string _message) {
        Match m = Regex.Match(_message, "PID:\\d*");
        text.text = "passou aqui";
        if (m.Success) {
            text.text = "entrou aqui";
            PID = _message.Split(':')[1];
            text.text = PID;
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
            if (shipTexture != null) {
                shipTexture.color = color;
            }
            if (elipseTexture != null) {
                elipseTexture.color = color;
            }
        }
        m = Regex.Match(_message, "\\d:\\d*");
    }

    //fim comunicaçao

}
