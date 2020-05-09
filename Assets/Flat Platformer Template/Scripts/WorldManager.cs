using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldManager : MonoBehaviour
{
    float timer, questTimer = 20;
    int waitingTime = 4;
    public GameObject enemy;
    GameObject player;
    GameObject defendObject;
    public GameObject finishGameUI;
    public TextMeshProUGUI questTimerText;
    public TextMeshProUGUI finishText;
    public GameObject questTimerObject;

    public GameObject spawnPoint1, spawnPoint2;
    void Start()
    {
        Time.timeScale = 1f;
        finishGameUI.SetActive(false);
        defendObject = GameObject.FindGameObjectWithTag("DefendObject");
        player = GameObject.FindGameObjectWithTag("Player");
        Instantiate(enemy, spawnPoint1.transform.position, spawnPoint1.transform.rotation);
        Instantiate(enemy, spawnPoint2.transform.position, spawnPoint1.transform.rotation);
    }

    void Update()
    {
        questTimerText.text = "Defend Princess for: " + Convert.ToInt32(questTimer) + " seconds.";
        if ((player.GetComponent<HealthSystem>().GetCurrentHealth() <= 0) || (defendObject.GetComponent<HealthSystem>().GetCurrentHealth() <= 0))
        {
            questTimerText.text = "";
            finishText.text = "You failed!";
            finishGameUI.SetActive(true);
            Time.timeScale = 0f;
        }
        timer += Time.deltaTime;
        if (timer > waitingTime)
        {
            Instantiate(enemy, spawnPoint1.transform.position, spawnPoint1.transform.rotation);
            Instantiate(enemy, spawnPoint2.transform.position, spawnPoint1.transform.rotation);
            timer = 0;
        }
        if(!(questTimer <= 0))
            questTimer -= Time.deltaTime;

        if (questTimer <= 0)
        {
            questTimerText.text="";
            finishText.text = "You saved Princess!";
            finishGameUI.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
