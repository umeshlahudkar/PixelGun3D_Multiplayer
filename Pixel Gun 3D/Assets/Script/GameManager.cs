using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourPunCallbacks
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    [SerializeField] private GameObject playerPrefab;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        if (PhotonNetwork.IsConnected)
        {
            int number = Random.Range(-20, 20);
            PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(number, 0, number), Quaternion.identity);
        }
    }

    public override void OnLeftRoom()
    {
        Debug.Log("Reload scene");
        SceneManager.LoadScene(0);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log(newPlayer.NickName + " Joined " + PhotonNetwork.CurrentRoom.Name + " " + PhotonNetwork.CurrentRoom.PlayerCount);
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }
}
