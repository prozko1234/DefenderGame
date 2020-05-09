using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldManager : MonoBehaviour
{
    float timer;
    int waitingTime = 4;
    public GameObject enemy;
    GameObject player;

    public GameObject spawnPoint1, spawnPoint2;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Instantiate(enemy, spawnPoint1.transform.position, spawnPoint1.transform.rotation);
        Instantiate(enemy, spawnPoint2.transform.position, spawnPoint1.transform.rotation);
    }

    void Update()
    {
        if(player.GetComponent<HealthSystem>().GetCurrentHealth() <= 0)
        {
            LoadMainMenu();
        }
        timer += Time.deltaTime;
        if (timer > waitingTime)
        {
            Instantiate(enemy, spawnPoint1.transform.position, spawnPoint1.transform.rotation);
            Instantiate(enemy, spawnPoint2.transform.position, spawnPoint1.transform.rotation);
            timer = 0;
        }
    }

    void LoadMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
