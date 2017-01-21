using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {
    private Transform gun;
    private float speed = 1f;
    private Enemy target;
    [SerializeField]
    private float timeBetweenAttacks;
    [SerializeField]
    private float attackRadius = 3;
    [SerializeField]
// private Projectile projectile;
    private bool isAttack = false;
    private Enemy targetEnemy = null;
    private float attackCounter;
    private AudioSource audioSource;
    
    // Use this for initialization
    void Start()
    {
        gun = GetComponent<Transform>();
    }

    public enum FacingDirection
    {
        UP = 270,
        DOWN = 90,
        LEFT = 180,
        RIGHT = 0
    }

    public static Quaternion FaceObject(Vector2 startingPosition, Vector2 targetPosition, FacingDirection facing)
    {
        Vector2 direction = targetPosition - startingPosition;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle -= (float)facing;
        return Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void FixedUpdate()
    {
        Enemy targetEnemy = GetNearestEnemyInRange();
        if (targetEnemy != null) {
            transform.rotation = FaceObject(transform.localPosition, targetEnemy.transform.localPosition, FacingDirection.DOWN);

            targetEnemy.MarkGreen();
            //float distance = Mathf.Abs(Vector2.Distance(transform.localPosition, targetEnemy.transform.localPosition));
            //if (distance < 25) { 
            //    var dir = targetEnemy.transform.localPosition - transform.position;
            //    var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            //    transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            //}
            //else
            //{
            //    transform.localEulerAngles = new Vector3(0, 0, 45);
            //}
        }
        else
        {
           transform.localEulerAngles = new Vector3(0, 0, 0);
        }
    }

    private List<Enemy> GetEnemiesInRange()
    {
        List<Enemy> enemiesInRange = new List<Enemy>();
        foreach (Enemy enemy in GameManager.Instance.EnemyList)
        {
            if (Vector2.Distance(transform.localPosition, enemy.transform.localPosition) <= attackRadius && !enemy.IsDead)
            {
                enemiesInRange.Add(enemy);
            }
        }
        return enemiesInRange;
    }

    private Enemy GetNearestEnemyInRange()
    {
        Enemy nearestEnemy = null;
        float smallestDistance = float.PositiveInfinity;
        foreach (Enemy enemy in GetEnemiesInRange())
        {
            enemy.RemoveGreen();
            if (Vector2.Distance(transform.localPosition, enemy.transform.localPosition) < smallestDistance)
            {
                smallestDistance = Vector2.Distance(transform.localPosition, enemy.transform.localPosition);
                nearestEnemy = enemy;
            }
        }
        return nearestEnemy;
    }
}
