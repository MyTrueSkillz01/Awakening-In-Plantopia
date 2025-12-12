using UnityEngine;

public class playerCombat : MonoBehaviour
{
    public Transform attackPoint;
    public float weaponRange = 1;
    public LayerMask enemyLayer;
    public int damage = 5;
    public Animator anim;
    public float Cooldown = 0.5f;
    private float timer;
    public bool isAttacking = false;
    private bool hasDealtDamage = false; // NEW FLAG

    private void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }
    }
    
    public void Attack()
    {
        if(timer <= 0 && !isAttacking)
        {
            isAttacking = true;
            hasDealtDamage = false; // RESET flag when starting new attack
            anim.SetBool("isAttacking", true);
            timer = Cooldown;
        }
    }

    public void dealDamage()
    {
        // Only deal damage if we haven't already
        if(hasDealtDamage) return;
        
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.position, weaponRange, enemyLayer);
        if(enemies.Length > 0)
        {
            enemies[0].GetComponent<enemyHealth>().ChangeHealth(-damage);
            hasDealtDamage = true; // MARK as dealt
        }
    }

    public void FinishAttack()
    {
        isAttacking = false;
        hasDealtDamage = false; // RESET for next attack
        anim.SetBool("isAttacking", false);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, weaponRange);
    }
}