using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviourPun
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private Image healthImage;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;

    }

    private void Update()
    {
        if (transform.position.y <= -10 && gameObject.activeSelf)
        {
            //Die();
            photonView.RPC("Die", RpcTarget.All);
        }
    }
    //public void TakeDamage(int damage)
    //{

    //        Debug.Log("Got hit!");

    //        currentHealth -= damage;
    //        UpdateHealthUI();
    //        if (currentHealth <= 0)
    //        {
    //            Die();
    //        }
    //}
    //private void UpdateHealthUI()
    //{
    //    healthImage.fillAmount = (float)currentHealth / maxHealth;
    //}
    //private void Die()
    //{
    //    gameObject.SetActive(false);
    //    Invoke("Respawn", 2f);

    //    if (photonView.IsMine)
    //        RoomManager.instance.SwitchToGeneralCamera();
    //    }
    [PunRPC]
    public void TakeDamage(int damage)
    {
        if (photonView.IsMine)
        {
            Debug.Log("Got hit!");

            currentHealth -= damage;
            
            if (currentHealth <= 0)
            {
                //Die();
                photonView.RPC("Die", RpcTarget.All);
            }

            photonView.RPC("UpdateHealthUI", RpcTarget.AllBuffered, currentHealth);
        }
    }
    [PunRPC]
    private void UpdateHealthUI(int currentHealth)
    {
        // Her oyuncu kendi ve diðer oyuncularýn hasar alan oyuncunun can çubuðunu güncelliyor
        healthImage.fillAmount = (float)currentHealth / maxHealth;
    }
    [PunRPC]
    private void Die()
    {
        gameObject.SetActive(false);
        Invoke("Respawn", 2f);

        if (photonView.IsMine)
            RoomManager.instance.SwitchToGeneralCamera();
    }

    private void Respawn()
    {
        currentHealth = maxHealth;
        //UpdateHealthUI();
        photonView.RPC("UpdateHealthUI", RpcTarget.All, currentHealth);

        gameObject.transform.position = RoomManager.instance.GetSpawnPoint().position;
        gameObject.SetActive(true);

        if (photonView.IsMine)
            RoomManager.instance.SwitchToPlayerCamera();

    }
}
