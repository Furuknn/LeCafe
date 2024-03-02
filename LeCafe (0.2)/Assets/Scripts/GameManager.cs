using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject upgradeButton;

    [SerializeField] private GameObject upgrade_1;
    [SerializeField] private GameObject upgrade_2;
    [SerializeField] private GameObject upgrade_3;

    [SerializeField] private TextMeshProUGUI upgradeCostText;

    private WaveSystem waveSystemscr;

    private int upgradeCost = 1000;
    private int upgradeMultiplier = 2;
    [SerializeField]private int currentMoney;
    private int upgradeAmount=0;

    private void Start()
    {
        waveSystemscr = GetComponent<WaveSystem>();

        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Resume();
        }

        if (!waveSystemscr.isWaveOngoing && waveSystemscr.waveData.text != "Day 0" && upgradeAmount<3)
        {
            upgradeButton.SetActive(true);
        }
        else
        {
            upgradeButton.SetActive(false);
        }
    }

    public void UpgradeCafe()
    {
        if (currentMoney>upgradeCost)
        {
            if (upgradeAmount==0)
            {
                upgrade_1.SetActive(true);
                upgradeAmount++;
                currentMoney -= upgradeCost;
                upgradeCost *= upgradeMultiplier;
            }
            else if (upgradeAmount == 1)
            {
                upgrade_2.SetActive(true);
                upgradeAmount++;
                currentMoney -= upgradeCost;
                upgradeCost *= upgradeMultiplier;
            }
            else if (upgradeAmount == 2)
            {
                upgrade_3.SetActive(true);
                upgradeAmount++;
                currentMoney -= upgradeCost;
                upgradeCost *= upgradeMultiplier;
                upgradeButton.SetActive(false);
            }

            upgradeCostText.text = upgradeCost + "$";

        }
    }
    
    public void Resume()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        if (pauseMenu.activeSelf)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Exit()
    {
        Application.Quit();
        Debug.Log("çýktý");
    }
}
