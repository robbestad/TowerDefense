using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public enum gameStatus
{
    next, play, gameover, win
}

public class GameManager : Singleton<GameManager> {
    const float waitingTime = 1f;

    [SerializeField]
    private int totalWaves = 10;
    [SerializeField]
    private GameObject spawnPoint;
    [SerializeField]
    private int totalEnemies = 3;
    [SerializeField]
    private int enemiesPerSpawn;
    [SerializeField]
    private Text totalMoneyLabel;
    [SerializeField]
    private Image GameStatusImage;
    [SerializeField]
    private Text nextWaveBtnLabel;
    [SerializeField]
    private Text escapedLabel;
    [SerializeField]
    private Text waveLabel;
    [SerializeField]
    private Text GameStatusLabel;
    [SerializeField]
    private int waveNumber = 0;
    private int totalMoney = 10;
    private int totalEscaped = 0;
    private int roundEscaped = 0;
    private int totalKilled = 0;
    private int enemiesToSpawn = 2;
    private gameStatus currentState = gameStatus.play;
    private AudioSource audioSource;

    public List<Enemy> EnemyList = new List<Enemy>();

    [SerializeField]
    private float spawnDelay = 1f;

    private int enemiesOnScreen = 0;

	void Start () {
        StartCoroutine(spawn());
    }

    void Update () {
		
	}

    IEnumerator spawn()
    {
        if (enemiesPerSpawn > 0 && EnemyList.Count < totalEnemies)
        {
            for (int i = 0; i < enemiesPerSpawn; i++)
            {
                if (EnemyList.Count < totalEnemies)
                {
                    Enemy newEnemy = Instantiate(EnemyList[Random.Range(0, enemiesToSpawn)]) as Enemy;
                    newEnemy.transform.position = spawnPoint.transform.position;
                }
            }
            yield return new WaitForSeconds(waitingTime);
            StartCoroutine(spawn());
        }
    }


    public void removeEnemyFromScreen()
    {
        if(enemiesOnScreen > 0)
        {
            enemiesOnScreen--;
        }
    }


    public void RegisterEnemy(Enemy enemy)
    {
        EnemyList.Add(enemy);
    }

    public void UnRegister(Enemy enemy)
    {
        EnemyList.Remove(enemy);
        Destroy(enemy.gameObject);
       // isWaveOver();
    }

    public void DestroyAllEnemies()
    {
        foreach (Enemy enemy in EnemyList)
        {
            Destroy(enemy.gameObject);
        }
        EnemyList.Clear();
    }


}
