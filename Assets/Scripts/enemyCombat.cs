using UnityEngine;

public class enemyDamage : MonoBehaviour
{
    public int damage = 2;
    public Transform attackPoint;
    public float weaponRange;
    public float knockBackForce;
    public float stuntTime;
    public LayerMask playerLayer;

    public void Attack()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, weaponRange, playerLayer);

        if(hits.Length > 0)
        {
            hits[0].GetComponent<playerHealth>().changeHealth(-damage);
            hits[0].GetComponent<playerMovement>().Knockback(transform, knockBackForce, stuntTime);
        }
    }
}
