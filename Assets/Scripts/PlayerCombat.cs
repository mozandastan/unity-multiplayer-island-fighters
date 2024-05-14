using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private int damageAmount = 20;

    public void Attack()
    {

        Debug.Log("Player attacked for " + damageAmount + " damage!");

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            PlayerHealth targetHealth = hit.collider.GetComponent<PlayerHealth>();
            if (targetHealth != null)
            {
                targetHealth.TakeDamage(damageAmount);
            }
        }
    }
}
