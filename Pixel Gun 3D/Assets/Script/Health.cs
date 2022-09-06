using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Health : MonoBehaviourPunCallbacks
{
    [SerializeField] private Slider healthBarSlider;

    private float health = 100;

    private void Start()
    {
        healthBarSlider.maxValue = health;
    }

    [PunRPC]
    public void TakeDamage(float damage)
    {
        health -= damage;

        healthBarSlider.value = health;
        
        if(health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if(photonView.IsMine)
            GameManager.Instance.LeaveRoom();
    }
}
