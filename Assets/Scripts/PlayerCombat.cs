using Photon.Pun;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCombat : MonoBehaviourPun
{
    [SerializeField] private int damageAmount = 20;
    [SerializeField] private float pushForce = 3.0f;

    [SerializeField] private float attackCooldown = 1.5f;
    private float lastAttackTime = 0f;

    private Animator animator;
    [SerializeField] private ParticleSystem attackarticle;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(Time.time >= lastAttackTime + attackCooldown)
            {
                lastAttackTime = Time.time;
                //Attack();
                photonView.RPC("Attack", RpcTarget.All);
            }

        }
    }
    [PunRPC]
    public void Attack()
    {
        attackarticle.Play();
        animator.SetTrigger("AttackTrig");

        RaycastHit hit;
        //if (Physics.Raycast(transform.position, transform.forward, out hit,2f))
        Collider[] hitColliders = Physics.OverlapSphere(attackarticle.gameObject.transform.position, 3);
        foreach(Collider collider in hitColliders)
        {

            if (collider.gameObject == gameObject)
                continue;

            PlayerHealth targetHealth = collider.GetComponent<PlayerHealth>();
            if (targetHealth != null)
            {
                Debug.Log("Player attacked for " + damageAmount + " damage!");
                targetHealth.TakeDamage(damageAmount);
            }

            Rigidbody targetRigidbody = collider.GetComponent<Rigidbody>();
            if (targetRigidbody != null)
            {
                Vector3 pushDirection = collider.transform.position - attackarticle.gameObject.transform.position;
                pushDirection.y = 0;
                pushDirection.Normalize();

                targetRigidbody.AddForce(pushDirection * pushForce, ForceMode.Impulse);
            }
        }
    }
}
