using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

public class Communication : MonoBehaviour {

    private const string TYPE_NAME = "IHA-SPG0";
    private HostData[] hostList;
    public GUIText text;
    private string PID;

    public SpriteRenderer shipRenderer;
    public SpriteRenderer elipseRenderer;

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
        networkView.RPC("MovePlayer", RPCMode.Server, message);
    }

    [RPC]
    public void SendChangedGun(string _gun) {
        string message = string.Format("{0}:{1}", PID, _gun);
        networkView.RPC("ChangeGun", RPCMode.Server, message);
    }

    [RPC]
    void ChangeGun(string _message) {
    }

    [RPC]
    void MovePlayer(string _message) {
    }
    #endregion

    #region RPC In
    [RPC]
    void RPCIn(string info) {
        ProcessMessageIn(info);
        Debug.Log("Received message -> " + info);
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
    void RPCStart(string nothing) { }
    #endregion

    void OnGUI() {
        if (GUI.Button(new Rect(100, 250, 250, 100), "Refresh Hosts")) {
            RefreshHostList();
        }
        if (hostList != null) {
            for (int i = 0; i < hostList.Length; i++) {
                if (GUI.Button(new Rect(400, 100 + (110 * i), 300, 100), hostList[i].gameName))
                    JoinServer(hostList[i]);
            }
        }
        if (GUILayout.Button("StartGame")) {
            networkView.RPC("RPCStart", RPCMode.Server, string.Empty);
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
            shipRenderer.color = color;
            elipseRenderer.color = color;
        }
        m = Regex.Match(_message, "\\d:\\d*");
    }
}
