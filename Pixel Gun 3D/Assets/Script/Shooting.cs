using UnityEngine;
using Photon.Pun;

public class Shooting : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;

    private float timeElapced;
    private float shootingInterval = 0.5f;

    private void Update()
    {
        timeElapced += Time.deltaTime;

        if(Input.GetButton("Fire1") && timeElapced >= shootingInterval)
        {
            Shoot();
            timeElapced = 0;
        }
    }

    private void Shoot()
    {
        Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f));
        RaycastHit hitInfo;

        if(Physics.Raycast(ray, out hitInfo))
        {
            PhotonView photonView = hitInfo.collider.GetComponent<PhotonView>();
            if(photonView != null && !photonView.IsMine)
            {
                photonView.RPC("TakeDamage", RpcTarget.AllBuffered , 10f);
            }
        }
    }
}
