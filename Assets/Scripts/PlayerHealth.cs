using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviourPun
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private Image healthImage;
    private int currentHealth;

    private Animator animator;

    private PlayerManager playerManager;
    private PlayerManager attackerManager;

    [SerializeField] private AudioClip hitSound;
    [SerializeField] private AudioClip respawnSound;
    [SerializeField] private AudioSource audioSource;

    void Start()
    {
        PlayRespawnSound();
        currentHealth = maxHealth;
        playerManager = GetComponent<PlayerManager>();
        animator = GetComponent<Animator>();
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
    public void TakeDamage(int damage, int attackerViewID)
    {
        animator.SetTrigger("HitTrig");
        PlayHitSound();

        if (photonView.IsMine)
        {
            Debug.Log("Got hit!");

            currentHealth -= damage;

            attackerManager = PhotonView.Find(attackerViewID).GetComponent<PlayerManager>();

            if (currentHealth <= 0)
            {
                //Die();
                photonView.RPC("Die", RpcTarget.All);

                //attackerManager = PhotonView.Find(attackerViewID).GetComponent<PlayerManager>();
                //if (attackerManager != null)
                //{
                //    // Öldüren oyuncunun PlayerManager'ýndaki AddKill fonksiyonunu çaðýr
                //    //attackerManager.AddKill();
                //    attackerManager.photonView.RPC("AddKill", RpcTarget.AllBuffered);
                //}
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
        if (attackerManager != null)
        {
            // Öldüren oyuncunun PlayerManager'ýndaki AddKill fonksiyonunu çaðýr
            //attackerManager.AddKill();
            attackerManager.photonView.RPC("AddKill", RpcTarget.AllBuffered);
        }
        playerManager.AddDeath();

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

        PlayRespawnSound();

        if (photonView.IsMine)
            RoomManager.instance.SwitchToPlayerCamera();

    }
    private void PlayHitSound()
    {
        if (hitSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(hitSound);
        }
    }
    private void PlayRespawnSound()
    {
        if (respawnSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(respawnSound);

        }
    }
}
