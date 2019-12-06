using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine.UI;

public class NetworkConnectionManager : MonoBehaviourPunCallbacks
{
    public Button BtnConnectMaster;
    public Button BtnConnectRoom;
    public InputField PlayerName;

    public bool TriesToConnectToMaster;
    public bool TriesToConnectToRoom;

    // Start is called before the first frame update
    void Start()
    {
        TriesToConnectToMaster = false;
        TriesToConnectToRoom = false;
    }

    // Update is called once per frame
    void Update()
    {
        BtnConnectMaster.gameObject.SetActive(!PhotonNetwork.IsConnected && !TriesToConnectToMaster);
        BtnConnectRoom.gameObject.SetActive(PhotonNetwork.IsConnected && !TriesToConnectToMaster && !TriesToConnectToRoom);
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        TriesToConnectToMaster = false;
        Debug.Log("Connected to Master!");
    }


    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
        TriesToConnectToMaster = false;
        TriesToConnectToRoom = false;
        Debug.LogWarningFormat("OnDisconnected() was called by PUN with reason {0}", cause);
    }

    public void OnClickConnectToMaster()
    {
        PhotonNetwork.OfflineMode = false;
        PhotonNetwork.NickName = PlayerName.ToString();
        PhotonNetwork.AutomaticallySyncScene = true; // this will call the PhotonNetwork.LoadLevel()

        TriesToConnectToMaster = true;
        PhotonNetwork.ConnectUsingSettings();
    }

    public void OnClickConnectToRoom()
    {
        if (!PhotonNetwork.IsConnected) return;

        TriesToConnectToRoom = true;
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        TriesToConnectToRoom = false;
        Debug.Log("Master: " + PhotonNetwork.IsMasterClient + " | Players in Room " + PhotonNetwork.CurrentRoom.Name + ": " + PhotonNetwork.CurrentRoom.PlayerCount);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 4 }); // first param is the name room. must be unique or null - that will generate a random name as well
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        Debug.Log(message);
        base.OnCreateRoomFailed(returnCode, message);
        TriesToConnectToRoom = false;

    }


}
