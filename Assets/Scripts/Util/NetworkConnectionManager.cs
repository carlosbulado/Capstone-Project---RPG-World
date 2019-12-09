using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NetworkConnectionManager : MonoBehaviourPunCallbacks
{
    public Button BtnConnectMaster;
    public Button BtnConnectRoom;
    public InputField PlayerName;

    public bool ConnectingToMaster;
    public bool ConnectingToRoom;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        ConnectingToMaster = false;
        ConnectingToRoom = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(BtnConnectMaster != null)
            BtnConnectMaster.gameObject.SetActive(!PhotonNetwork.IsConnected && !ConnectingToMaster);

        if(BtnConnectRoom != null)
            BtnConnectRoom.gameObject.SetActive(PhotonNetwork.IsConnected && !ConnectingToMaster && !ConnectingToRoom);
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        ConnectingToMaster = false;
        Debug.Log("Connected to Master!");
    }


    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
        ConnectingToMaster = false;
        ConnectingToRoom = false;
        Debug.LogWarningFormat("OnDisconnected() was called by PUN with reason {0}", cause);
    }

    public void OnClickConnectToMaster()
    {
        PhotonNetwork.OfflineMode = false;
        //PhotonNetwork.NickName = PlayerName.ToString();
        PhotonNetwork.NickName = "Player" + Random.Range(1, 100);

        ConnectingToMaster = true;
        PhotonNetwork.ConnectUsingSettings();
    }

    public void OnClickConnectToRoom()
    {
        if (!PhotonNetwork.IsConnected) return;

        ConnectingToRoom = true;
        //PhotonNetwork.CreateRoom("Test Room 1", new RoomOptions { MaxPlayers = 20 });
        //PhotonNetwork.JoinRoom("Test Room 1");
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        ConnectingToRoom = false;
        Debug.Log("Master: " + PhotonNetwork.IsMasterClient + " | Players in Room " + PhotonNetwork.CurrentRoom.Name + ": " + PhotonNetwork.CurrentRoom.PlayerCount);
        SceneManager.LoadScene("Main_Scene");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        PhotonNetwork.JoinRoom("Test Room 1");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 20 }); // first param is the name room. must be unique or null - that will generate a random name as well
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        Debug.Log(message);
        ConnectingToRoom = false;

    }


}
