using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AIPathfinding : MonoBehaviour
{
    public AIPath path;
    public AIDestinationSetter destinationSetter;

    public enum AttackType
    {
        Melee, Ranged, Cobweb
    }
    public AttackType attackType;

    public float attackRange;
    public float attackDelay;

    bool playerInRange = false;
    bool isAttacking = false;

    public GameObject bulletPrefab;
    public float bulletSpeed;
    public Transform firepoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange && Vector3.Distance(transform.position, destinationSetter.target.position) < attackRange && !isAttacking)
        {
            switch (attackType)
            {
                case AttackType.Melee: StartCoroutine(MeleeAttack());
                    break;
                case AttackType.Ranged: StartCoroutine(RangedAttack(false));
                    break;
                case AttackType.Cobweb: StartCoroutine(RangedAttack(true));
                    break;
                default:
                    break;
            }
        }
    }

    IEnumerator RangedAttack(bool cobweb)
    {
        isAttacking = true;

        path.canMove = false;

        StartCoroutine(LookAtPlayer());

        yield return new WaitForSeconds(attackDelay);

        GameObject bullet = Instantiate(bulletPrefab, firepoint.position, Quaternion.identity);
        bullet.transform.up = destinationSetter.target.position - bullet.transform.position;
        bullet.GetComponent<Rigidbody2D>().AddForce((destinationSetter.target.position - bullet.transform.position) * bulletSpeed, ForceMode2D.Impulse);
        bullet.GetComponent<EnemyBullet>().isCobweb = cobweb;

        yield return new WaitForSeconds(attackDelay / 2);

        path.canMove = true;

        isAttacking = false;
    }

    IEnumerator LookAtPlayer()
    {
        float time = attackDelay + attackDelay / 2;

        float timer = 0;

        while (timer < time)
        {
            transform.up = destinationSetter.target.position - transform.position;

            timer += Time.deltaTime;

            yield return null;
        }
    }

    IEnumerator MeleeAttack()
    {
        isAttacking = true;

        path.canMove = false;

        yield return new WaitForSeconds(attackDelay);

        Collider2D[] hit = Physics2D.OverlapCircleAll(transform.position, attackRange);

        foreach (Collider2D col in hit)
        {
            if (col.CompareTag("Player"))
            {
                col.GetComponent<PlayerStats>().TakeDamage();
            }
        }

        yield return new WaitForSeconds(attackDelay / 2);

        path.canMove = true;

        isAttacking = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;

            destinationSetter.target = collision.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;

            destinationSetter.target = null;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
