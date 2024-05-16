using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private Image healthImage;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        RoomManager.instance.SwitchToPlayerCamera();
    }

    private void Update()
    {
        if(transform.position.y <= -10 && gameObject.activeSelf)
        {
            Die();
        }
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("Got hit!");

        currentHealth -= damage;
        healthImage.fillAmount = (float)currentHealth / 100;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        gameObject.SetActive(false);
        RoomManager.instance.SwitchToGeneralCamera();
        Invoke("Respawn", 2f);
    }
    private void Respawn()
    {
        currentHealth = 100;
        healthImage.fillAmount = (float)currentHealth / 100;

        gameObject.SetActive (true);
        gameObject.transform.position = RoomManager.instance.GetSpawnPoint().position;

        RoomManager.instance.SwitchToPlayerCamera();

    }
}
