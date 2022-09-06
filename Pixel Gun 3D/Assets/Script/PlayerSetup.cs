using UnityEngine;
using Photon.Pun;
using TMPro;

public class PlayerSetup : MonoBehaviourPunCallbacks
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private MovementController movementController;
    [SerializeField] private TextMeshProUGUI playerNametext;

    private void Start()
    {
        if(photonView.IsMine)
        {
            mainCamera.enabled = true;
            movementController.enabled = true;
        }
        else
        {
            mainCamera.enabled = false;
            movementController.enabled = false;
        }

        PlayerNameSetUp();
    }

    private void PlayerNameSetUp()
    {
        if(photonView.Owner.NickName != null)
        {
            playerNametext.text = photonView.Owner.NickName;
        }
    }
}
