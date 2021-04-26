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
    bool playerInLOF = false;
    bool isAttacking = false;

    public GameObject bulletPrefab;
    public float bulletSpeed;
    public Transform firepoint;

    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange && playerInLOF && Vector3.Distance(transform.position, player.position) < attackRange && !isAttacking)
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

    private void FixedUpdate()
    {
        if (playerInRange && !playerInLOF)
        {
            playerInLOF = LineOfSight();

            if (playerInLOF == true)
            {
                destinationSetter.target = player;
            }
        }
    }

    IEnumerator RangedAttack(bool cobweb)
    {
        isAttacking = true;

        path.canMove = false;

        StartCoroutine(LookAtPlayer());

        yield return new WaitForSeconds(attackDelay);

        if (!LineOfSight())
        {
            yield return new WaitUntil(() => LineOfSight());
        }

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

            if (LineOfSight())
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

    private bool LineOfSight()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, player.position - transform.position);

        if (hit)
        {
            if (hit.transform.CompareTag("Player"))
            {
                return true;
            }
        }

        return false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
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
