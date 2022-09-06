using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class LaunchManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject enterNamePanel;
    [SerializeField] private GameObject networkStatusPanel;
    [SerializeField] private GameObject lobbyPanel;

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    private void Start()
    {
        enterNamePanel.SetActive(true);
        networkStatusPanel.SetActive(false);
        lobbyPanel.SetActive(false);
    }
    public void ConnectToPhotonServer()
    {
        if(!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
            enterNamePanel.SetActive(false);
            networkStatusPanel.SetActive(true);
        }
    }
    public void SetPlayerName(string playerName)
    {
        if (playerName.Length <= 0)
        {
            Debug.Log("Player Name not Entered");
            return;
        }

        PhotonNetwork.NickName = playerName;
        Debug.Log(PhotonNetwork.NickName);
    }

    public void JoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    #region Photon callBacks
    public override void OnConnected()
    {
        Debug.Log("Connected to the network");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log(PhotonNetwork.NickName + " Connected to the photon server");
        networkStatusPanel.SetActive(false);
        lobbyPanel.SetActive(true);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log(message);
        CreateAndJoinRoom();
    }

    public override void OnCreatedRoom()
    {
        Debug.Log(PhotonNetwork.NickName +" create room " + PhotonNetwork.CurrentRoom.Name);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log(PhotonNetwork.NickName + " Joined room " + PhotonNetwork.CurrentRoom.Name);

        PhotonNetwork.LoadLevel(1);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log(newPlayer.NickName + " Joined room" + PhotonNetwork.CurrentRoom.Name + " Count " + PhotonNetwork.CurrentRoom.PlayerCount);
    }

    #endregion

    private void CreateAndJoinRoom()
    {
        string roomName = "Room " + Random.Range(0, 1000);

        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsOpen = true;
        roomOptions.IsVisible = true;
        roomOptions.MaxPlayers = 20;

        PhotonNetwork.CreateRoom(roomName, roomOptions);
    }
}
