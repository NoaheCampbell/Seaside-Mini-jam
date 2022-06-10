using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    private EnemyMaster bossMaster;
    private GameObject boss;
    private BossAttacks bossAttacks;
    private BossMovement bossMovement;
    public bool recentlyJumped;
    private float timer;
    private Vector3 bossPosition;
    private float timeSinceLastJump;
    public bool recentlyDashed;
    private float timeSinceLastDash;

    // Start is called before the first frame update
    void Start()
    {
        bossMaster = gameObject.GetComponent<EnemyMaster>();

        // Changes enemymaster fields to match boss
        bossMaster.EnemyType(gameObject.tag);

        bossMovement = gameObject.GetComponent<BossMovement>();

        boss = gameObject;
        recentlyJumped = false;
        timer = 0;
        timeSinceLastJump = 0;
        recentlyDashed = false;
        timeSinceLastDash = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (recentlyJumped)
        {
            timeSinceLastJump += Time.deltaTime;

            if (timeSinceLastJump > 10)
            {
            recentlyJumped = false;
            timeSinceLastJump = 0;
            }
        }

        if (recentlyDashed)
        {
            timeSinceLastDash += Time.deltaTime;

            if (timeSinceLastDash > 10)
            {
                recentlyDashed = false;
                timeSinceLastDash = 0;
            }
        }

    }

    public void Jump()
    {
        if (!recentlyJumped && bossMaster.isGrounded)
        {
            recentlyJumped = true;
            
            // Start the jump animation
            StartCoroutine(JumpAnimation());
        }
    }

    public void Dash()
    {
        if (!recentlyDashed && bossMaster.isGrounded)
        {
            recentlyJumped = true;

            // Start the jump animation
            StartCoroutine(DashAnimation());
        }
    }

    IEnumerator DashAnimation()
    {
        recentlyDashed = true;

        // Makes the boss dash towards the player
        var timerLeft = 50f;

        // Backs up a little bit before dashing forward
        boss.transform.position = Vector3.MoveTowards(boss.transform.position, boss.transform.position - boss.transform.forward, bossMaster.moveSpeed);

        bossMovement.canRotate = false;
        while (timerLeft > 0)
        {
            // Moves the boss forward in a dash, unless they get more than 30 units from the player
            boss.transform.position = Vector3.MoveTowards(boss.transform.position, boss.transform.position + boss.transform.forward, bossMaster.dashSpeed);
            yield return new WaitForSeconds(0.01f);

            if (bossMovement.targetDistance > 30)
            {
                bossMovement.canRotate = true;
                yield break;
            }

            timerLeft -= Time.deltaTime;
        }

        // Attacks as the boss passes by the player
        bossAttacks.MeleeAttack();

        bossMovement.canRotate = true;

    }


    IEnumerator JumpAnimation()
    {
        // Makes the boss jump up for a few seconds
        var timerLeft = 30f;

        while (timerLeft > 0)
        {
            boss.transform.position = Vector3.MoveTowards(boss.transform.position, bossPosition + new Vector3(0, 10, 0), 1f);
            yield return new WaitForSeconds(0.01f);
            // boss.transform.position = Vector3.MoveTowards(boss.transform.position, bossPosition, 0.5f);
            timerLeft -= 0.5f;
        }

    }
    
}
