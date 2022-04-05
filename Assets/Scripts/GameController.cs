using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviourPunCallbacks
{
    //public GameObject player;
    //public GameObject panel;

    void Start()
    {
        // プレイヤー自身の名前を"Player"に設定する
        PhotonNetwork.NickName = "Player";

        PhotonNetwork.ConnectUsingSettings();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions(), TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        //Debug.Log("join");
        var players = PhotonNetwork.PlayerList;
        //Debug.Log(players);
        if (players.Length < 2)
        {
            //player = PhotonNetwork.Instantiate("Canvas", new Vector3(0, 0), Quaternion.identity);
            //panel = PhotonNetwork.Instantiate("PanelCanvas", new Vector3(0, 0), Quaternion.identity);
            PhotonNetwork.Instantiate("Params", new Vector3(0, 0), Quaternion.identity);
            PhotonNetwork.Instantiate("PanelCanvas", new Vector3(0, 1000), Quaternion.identity);
            PhotonNetwork.Instantiate("Canvas", new Vector3(0, 0), Quaternion.identity);
        }

        //GameObject wordle_panel = GameObject.Find("Canvas/WordlePanel");
        //player.transform.SetParent(wordle_panel.transform);
        //player.transform.GetChild(0).gameObject.GetComponent<RectTransform>().localPosition = new Vector3(200 , 0, 0);
        //player.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1); 
        //player.transform.Find("Score").gameObject.GetComponent<RectTransform>().localPosition = new Vector3(320 * RL, 190, 0);
    }
}
 