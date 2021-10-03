using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    private int numberOfEnemiesSpawned;
    public EnemyMovement enemy;

    public Text enemiesToKill;

    private List<EnemyMovement> enemyList = new List<EnemyMovement>();

    FMODUnity.StudioEventEmitter enemySpawnAudio;

    public static int enemiesKilled;
    public static int enemiesKillGoal;

    int xVector;
    int yVector;

    private bool readyToMove = false;
    public static bool goalReached = false;
    private bool goalSet = false;

    // Start is called before the first frame update
    void Awake()
    {
        enemiesKillGoal = 0;
        goalSet = false;
        goalReached = false;
        enemySpawnAudio = FindObjectOfType<FMODUnity.StudioEventEmitter>();
        MidiManager.OnNoteChange += OnNoteChange;
        MidiManagerBeat.OnBeatChange += OnBeatChange;
        StartGameCountdown.OnGameStart += OnGameStart;
        enemiesToKill.text = enemiesKillGoal.ToString();
    }

    private void OnGameStart()
    {
        InstantiateEnemies();
        SetEnemyKillGoal();
    }

    private void OnBeatChange(int currentNoteValue, string currentNoteName)
    {
        if (currentNoteName == "D" || currentNoteName == "E")
        {
            InstantiateEnemies();
        }

        if (currentNoteName == "E")
        {
            ShakeEnemy();
        }

        if (currentNoteName == "D" && readyToMove)
        {
            RandomlyMoveEnemies();
        }
    }

    private void OnNoteChange(int currentNoteValue, string currentNoteName)
    {
        ChooseRandomEnemyToFire();
    }

    void Update()
    {
        UpdateEnemiesList();
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
        MidiManager.OnNoteChange -= OnNoteChange;
        MidiManagerBeat.OnBeatChange -= OnBeatChange;
        StartGameCountdown.OnGameStart -= OnGameStart;
    }

    private void SetEnemyKillGoal()
    {
        if (enemiesKillGoal == 0 && !goalSet)
        {
            enemiesKillGoal = MusicManager.totalEventLength;
            enemiesToKill.text = enemiesKillGoal.ToString();
            goalSet = true;
        }
    }

    private void CalcuclateEnemiesLeft()
    {
        if (enemiesKillGoal > 0 && !goalReached)
        {
            enemiesKillGoal--;
        }
        else
        {
            goalReached = true;
            FMODUnity.RuntimeManager.PlayOneShot("event:/sfx/player_kill_count_achieved");
        }
        enemiesToKill.text = enemiesKillGoal.ToString();
    }

    private void ShakeEnemy()
    {
        float randomChance = Random.Range(0, 10);
        if (randomChance < 1 && enemyList.Count != 0)
        {
            readyToMove = true;

            for (int counter = 0; counter < enemyList.Count; counter++)
            {
                enemyList[counter].ShakeEnemyAnimation();
            }
            FMODUnity.RuntimeManager.PlayOneShot("event:/sfx/enemy_shake");

        }
    }

    private void DefineRandomNumbers()
    {
        numberOfEnemiesSpawned = Random.Range(4, 14);
        xVector = Random.Range(-13, 13);
        yVector = Random.Range(-1, 6);
    }

    private void RandomlyMoveEnemies()
    {
       for (int counter = 0; counter < enemyList.Count; counter++)
        {
            int newXVector = Random.Range(-13, 13);
            int newYVector = Random.Range(-1, 6);
            enemyList[counter].gameObject.transform.position = new Vector2(newXVector, newYVector);
            enemyList[counter].ShakeEnemyAnimation();
        }

        FMODUnity.RuntimeManager.PlayOneShot("event:/sfx/enemy_teleport");

        readyToMove = false;
    }

    private void InstantiateEnemies()
    {
        if (enemyList.Count == 0)
        {
            DefineRandomNumbers();

            for (int counter = 0; counter < numberOfEnemiesSpawned; counter++)
            {
                Instantiate(enemy, new Vector2(xVector, yVector), Quaternion.identity);
            }


            PopulateEnemiesList();
            enemyList[0].PlayEnemySpawnAudio();
        }
    }

    private void PopulateEnemiesList()
    {
        enemyList.AddRange(FindObjectsOfType<EnemyMovement>());
        //Debug.Log(enemyList.Count);
    }

    private void UpdateEnemiesList()
    {
        if (enemyList.Count > 0)
        {
            for (int counter = 0; counter < enemyList.Count; counter++)
            {
                if (enemyList[counter] == null)
                {
                    enemyList.Remove(enemyList[counter]);
                    if (!goalReached)
                    {
                        CalcuclateEnemiesLeft();
                    }
                }
            }
        }

        if (enemyList.Count == 0)
        {
            readyToMove = false;
        }
    }

    public void ChooseRandomEnemyToFire()
    {
        if (enemyList.Count != 0)
        {
            int randomShootProbability = Random.Range(0, 15);
            if (randomShootProbability - 4 <= enemyList.Count && enemyList.Count != 0)
            {
                int randomEnemy = Random.Range(0, enemyList.Count);
                enemyList[randomEnemy].EnemyShoot();
            }
        }
    }
}
