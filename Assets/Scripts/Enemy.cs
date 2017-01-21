using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int target = 0;
    public Transform exitPoint;
    public Transform[] waypoints;
    public float navigationUpdate;

    private Transform enemy;
    private float navigationTime = 0;
    private bool isDead = false;
    private GameObject gameObjectEnemy;

    public bool IsDead
    {
        get
        {
            return isDead;
        }

    }
    void Start()
    {
        enemy = GetComponent<Transform>();
        //anim = GetComponent<Animator>();
        //enemyCollider = GetComponent<Collider2D>();
        GameManager.Instance.RegisterEnemy(this);
        gameObjectEnemy = gameObject;
    }

    public void MarkGreen()
    {
        if(gameObjectEnemy != null)
            gameObjectEnemy.GetComponent<Renderer>().material.color = Color.green;
    }
    public void RemoveGreen()
    {
        if (gameObjectEnemy != null)
            gameObjectEnemy.GetComponent<Renderer>().material.color = Color.white;
    }

    void Update()
    {

        if (waypoints != null)
        {
            navigationTime += Time.deltaTime;
            if (navigationTime > navigationUpdate)
            {
                if (target < waypoints.Length)
                {
                    enemy.position = Vector2.MoveTowards(enemy.position, waypoints[target].position, navigationTime);
                }
                else
                {
                    enemy.position = Vector2.MoveTowards(enemy.position, exitPoint.position, navigationTime);
                }
                navigationTime = 0;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "checkpoint")
        {
            target += 1;
        }
        else if (other.tag == "Finish")
        {
            //GameManager.Instance.TotalEscaped += 1;
            //GameManager.Instance.RoundEscaped += 1;
            GameManager.Instance.UnRegister(this);
            //GameManager.Instance.isWaveOver();
        }
        else if (other.tag == "Projectile")
        {
            //Projectile newP = other.gameObject.GetComponent<Projectile>();
            //enemyHit(newP.AttackStrength);
            Destroy(other.gameObject);
        }
    }

}
