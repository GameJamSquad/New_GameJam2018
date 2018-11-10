using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool isGamePaused = false;
    bool isGameOver = false;
    public bool lobbyOpen = true;

    public GameObject lobbyObject;
    public Text winText;
    int winningPlayer;

    bool hasFirstPlayer = false, hasSecondPlayer = false, hasThirdPlayer = false, hasFourthPlayer = false;
    public int firstPlayerTank, secondPlayerTank, thirdPlayerTank, fourthPlayerTank;
    public bool[] playerLockIns;

    public List<GameObject> playerInPanels;
    public List<GameObject> playersLockedinPanels;
    public GameObject startGamePanel;

    public List<GameObject> player1Buttons;
    public List<GameObject> player2Buttons;
    public List<GameObject> player3Buttons;
    public List<GameObject> player4Buttons;

    public GameObject scorePanel;
    public List<Text> playerScores;

    public int numOfPlayers = 1;
    public List<TankManager> tanks;

    public List<GameObject> tankTypes;

    public int scoreToWin = 10;

    public float respawnTimer = 5f;
    public List<Transform> respawnPoints;

    public float timeSpeed = 1;
    public bool slowdownOn, speedupOn;
    public GameObject slowDownPanel, speedUpPanel;

    public void AdjustTimeSpeed(float amount)
    {
        timeSpeed += amount;
        Time.timeScale = timeSpeed;
        StartCoroutine(ResetTimeSpeedCooldown());
    }

    IEnumerator ResetTimeSpeedCooldown()
    {
        yield return new WaitForSeconds(5f * timeSpeed);
        timeSpeed = 1;
        slowdownOn = false;
        speedupOn = false;
        Time.timeScale = timeSpeed;
    }

    private void Update()
    {
        lobbyObject.SetActive(lobbyOpen);

        CheckPlayerInput();
        PanelManager();

        if (lobbyOpen)
        {
            isGamePaused = true;

            ChangePlayerTanks();
            UpdatePlayerButtons();
        }
        if (isGameOver)
        {
            if (Input.GetButtonDown("AButton_P1") || Input.GetButtonDown("AButton_P2") || Input.GetButtonDown("AButton_P3") || Input.GetButtonDown("AButton_P4"))
            {
                int level = Random.Range(1, 3);
                SceneManager.LoadScene(level);
            }
        }

        if (playerLockIns[0] && playerLockIns[1] && playerLockIns[2] && playerLockIns[3])
        {
            if (lobbyOpen)
            {
                if (Input.GetButtonDown("AButton_P1") || Input.GetButtonDown("AButton_P2") || Input.GetButtonDown("AButton_P3") || Input.GetButtonDown("AButton_P4"))
                {
                    StartGame();
                }
            }
        }
    }

    void PanelManager()
    {
        if (hasFirstPlayer)
        {
            playerInPanels[0].SetActive(false);
        }
        else
        {
            playerInPanels[0].SetActive(true);
        }

        if (hasSecondPlayer)
        {
            playerInPanels[1].SetActive(false);
        }
        else
        {
            playerInPanels[1].SetActive(true);
        }

        if (hasThirdPlayer)
        {
            playerInPanels[2].SetActive(false);
        }
        else
        {
            playerInPanels[2].SetActive(true);
        }

        if (hasFourthPlayer)
        {
            playerInPanels[3].SetActive(false);
        }
        else
        {
            playerInPanels[3].SetActive(true);
        }

        if (playerLockIns[0])
        {
            playersLockedinPanels[0].SetActive(true);
        }
        if (playerLockIns[1])
        {
            playersLockedinPanels[1].SetActive(true);
        }
        if (playerLockIns[2])
        {
            playersLockedinPanels[2].SetActive(true);
        }
        if (playerLockIns[3])
        {
            playersLockedinPanels[3].SetActive(true);
        }

        if (!lobbyOpen)
        {
            scorePanel.SetActive(true);
        }
        if (speedupOn)
        {
            speedUpPanel.SetActive(true);
        }
        else
        {
            speedUpPanel.SetActive(false);
        }
        if (slowdownOn)
        {
            slowDownPanel.SetActive(true);
        }
        else
        {
            slowDownPanel.SetActive(false);
        }

        if(playerLockIns[0] && playerLockIns[1] && playerLockIns[2] && playerLockIns[3])
        {
            startGamePanel.SetActive(true);
        }
    }

    void UpdatePlayerButtons()
    {
        if(firstPlayerTank == 0)
        {
            player1Buttons[0].SetActive(false);
            player1Buttons[1].SetActive(true);

            player1Buttons[2].SetActive(true);
            player1Buttons[3].SetActive(false);

            player1Buttons[4].SetActive(true);
            player1Buttons[5].SetActive(false);

            player1Buttons[6].SetActive(true);
            player1Buttons[7].SetActive(false);
        }
        else if (firstPlayerTank == 1)
        {
            player1Buttons[0].SetActive(true);
            player1Buttons[1].SetActive(false);

            player1Buttons[2].SetActive(false);
            player1Buttons[3].SetActive(true);

            player1Buttons[4].SetActive(true);
            player1Buttons[5].SetActive(false);

            player1Buttons[6].SetActive(true);
            player1Buttons[7].SetActive(false);
        }
        else if (firstPlayerTank == 2)
        {
            player1Buttons[0].SetActive(true);
            player1Buttons[1].SetActive(false);

            player1Buttons[2].SetActive(true);
            player1Buttons[3].SetActive(false);

            player1Buttons[4].SetActive(false);
            player1Buttons[5].SetActive(true);

            player1Buttons[6].SetActive(true);
            player1Buttons[7].SetActive(false);
        }
        else if (firstPlayerTank == 3)
        {
            player1Buttons[0].SetActive(true);
            player1Buttons[1].SetActive(false);

            player1Buttons[2].SetActive(true);
            player1Buttons[3].SetActive(false);

            player1Buttons[4].SetActive(true);
            player1Buttons[5].SetActive(false);

            player1Buttons[6].SetActive(false);
            player1Buttons[7].SetActive(true);
        }

        if (secondPlayerTank == 0)
        {
            player2Buttons[0].SetActive(false);
            player2Buttons[1].SetActive(true);

            player2Buttons[2].SetActive(true);
            player2Buttons[3].SetActive(false);

            player2Buttons[4].SetActive(true);
            player2Buttons[5].SetActive(false);

            player2Buttons[6].SetActive(true);
            player2Buttons[7].SetActive(false);
        }
        else if (secondPlayerTank == 1)
        {
            player2Buttons[0].SetActive(true);
            player2Buttons[1].SetActive(false);

            player2Buttons[2].SetActive(false);
            player2Buttons[3].SetActive(true);

            player2Buttons[4].SetActive(true);
            player2Buttons[5].SetActive(false);

            player2Buttons[6].SetActive(true);
            player2Buttons[7].SetActive(false);
        }
        else if (secondPlayerTank == 2)
        {
            player2Buttons[0].SetActive(true);
            player2Buttons[1].SetActive(false);

            player2Buttons[2].SetActive(true);
            player2Buttons[3].SetActive(false);

            player2Buttons[4].SetActive(false);
            player2Buttons[5].SetActive(true);

            player2Buttons[6].SetActive(true);
            player2Buttons[7].SetActive(false);
        }
        else if (secondPlayerTank == 3)
        {
            player2Buttons[0].SetActive(true);
            player2Buttons[1].SetActive(false);

            player2Buttons[2].SetActive(true);
            player2Buttons[3].SetActive(false);

            player2Buttons[4].SetActive(true);
            player2Buttons[5].SetActive(false);

            player2Buttons[6].SetActive(false);
            player2Buttons[7].SetActive(true);
        }

        if (thirdPlayerTank == 0)
        {
            player3Buttons[0].SetActive(false);
            player3Buttons[1].SetActive(true);

            player3Buttons[2].SetActive(true);
            player3Buttons[3].SetActive(false);

            player3Buttons[4].SetActive(true);
            player3Buttons[5].SetActive(false);

            player3Buttons[6].SetActive(true);
            player3Buttons[7].SetActive(false);
        }
        else if (thirdPlayerTank == 1)
        {
            player3Buttons[0].SetActive(true);
            player3Buttons[1].SetActive(false);

            player3Buttons[2].SetActive(false);
            player3Buttons[3].SetActive(true);
        
            player3Buttons[4].SetActive(true);
            player3Buttons[5].SetActive(false);

            player3Buttons[6].SetActive(true);
            player3Buttons[7].SetActive(false);
        }
        else if (thirdPlayerTank == 2)
        {
            player3Buttons[0].SetActive(true);
            player3Buttons[1].SetActive(false);

            player3Buttons[2].SetActive(true);
            player3Buttons[3].SetActive(false);

            player3Buttons[4].SetActive(false);
            player3Buttons[5].SetActive(true);

            player3Buttons[6].SetActive(true);
            player3Buttons[7].SetActive(false);
        }
        else if (thirdPlayerTank == 3)
        {
            player3Buttons[0].SetActive(true);
            player3Buttons[1].SetActive(false);

            player3Buttons[2].SetActive(true);
            player3Buttons[3].SetActive(false);

            player3Buttons[4].SetActive(true);
            player3Buttons[5].SetActive(false);

            player3Buttons[6].SetActive(false);
            player3Buttons[7].SetActive(true);
        }


        if (fourthPlayerTank == 0)
        {
            player4Buttons[0].SetActive(false);
            player4Buttons[1].SetActive(true);

            player4Buttons[2].SetActive(true);
            player4Buttons[3].SetActive(false);

            player4Buttons[4].SetActive(true);
            player4Buttons[5].SetActive(false);

            player4Buttons[6].SetActive(true);
            player4Buttons[7].SetActive(false);
        }
        else if (fourthPlayerTank == 1)
        {
            player4Buttons[0].SetActive(true);
            player4Buttons[1].SetActive(false);

            player4Buttons[2].SetActive(false);
            player4Buttons[3].SetActive(true);

            player4Buttons[4].SetActive(true);
            player4Buttons[5].SetActive(false);

            player4Buttons[6].SetActive(true);
            player4Buttons[7].SetActive(false);
        }
        else if (fourthPlayerTank == 2)
        {
            player4Buttons[0].SetActive(true);
            player4Buttons[1].SetActive(false);

            player4Buttons[2].SetActive(true);
            player4Buttons[3].SetActive(false);

            player4Buttons[4].SetActive(false);
            player4Buttons[5].SetActive(true);

            player4Buttons[6].SetActive(true);
            player4Buttons[7].SetActive(false);
        }
        else if (fourthPlayerTank == 3)
        {
            player4Buttons[0].SetActive(true);
            player4Buttons[1].SetActive(false);

            player4Buttons[2].SetActive(true);
            player4Buttons[3].SetActive(false);

            player4Buttons[4].SetActive(true);
            player4Buttons[5].SetActive(false);

            player4Buttons[6].SetActive(false);
            player4Buttons[7].SetActive(true);
        }
    }

    public void AddFirstPlayer()
    {
        hasFirstPlayer = true;
        numOfPlayers++;
    }
    public void AddSecondPlayer()
    {
        hasSecondPlayer = true;
        numOfPlayers++;
    }
    public void AddThirdPlayer()
    {
        hasThirdPlayer = true;
        numOfPlayers++;
    }
    public void AddFourthPlayer()
    {
        hasFourthPlayer = true;
        numOfPlayers++;
    }

    public void CheckScores()
    {
        for(int i = 0; i < tanks.Count; i++)
        {
            playerScores[i].text = tanks[i].score.ToString();

            if(tanks[i].score >= scoreToWin)
            {
                winningPlayer = i + 1;
                EndGame();
            }
        }
    }

    void EndGame()
    {
        winText.text = "Player " + winningPlayer + " wins the game! \nPress A to start again!";
        winText.gameObject.SetActive(true);
        isGameOver = true;
        isGamePaused = true;
    }

    public void StartGame()
    {
        for(int i = 0; i < numOfPlayers; i++)
        {
            if (i == 0)
            {
                GameObject curTank = Instantiate(tankTypes[firstPlayerTank], respawnPoints[0].position, Quaternion.identity);
                tanks.Add(curTank.GetComponentInChildren<TankManager>());
                tanks[i].playerNumber = i + 1;
            }
            if (i == 1)
            {
                GameObject curTank = Instantiate(tankTypes[secondPlayerTank], respawnPoints[1].position, Quaternion.identity);
                tanks.Add(curTank.GetComponentInChildren<TankManager>());
                tanks[i].playerNumber = i + 1;
            }
            if (i == 2)
            {
                GameObject curTank = Instantiate(tankTypes[thirdPlayerTank], respawnPoints[2].position, Quaternion.identity);
                tanks.Add(curTank.GetComponentInChildren<TankManager>());
                tanks[i].playerNumber = i + 1;
            }
            if (i == 3)
            {
                GameObject curTank = Instantiate(tankTypes[fourthPlayerTank], respawnPoints[3].position, Quaternion.identity);
                tanks.Add(curTank.GetComponentInChildren<TankManager>());
                tanks[i].playerNumber = i + 1;
            }
        }

        lobbyOpen = false;
        isGamePaused = false;
    }
    

    void CheckPlayerInput()
    {
        if (Input.GetButtonDown("AButton_P1"))
        {
            AddFirstPlayer();
        }
        if (Input.GetButtonDown("AButton_P2"))
        {
            AddSecondPlayer();
        }
        if (Input.GetButtonDown("AButton_P3"))
        {
            AddThirdPlayer();
        }
        if (Input.GetButtonDown("AButton_P4"))
        {
            AddFourthPlayer();
        }
    }

    void ChangePlayerTanks()
    {
        if (hasFirstPlayer)
        {
            if (!playerLockIns[0])
            {
                if (Input.GetButtonDown("AButton_P1"))
                {
                    firstPlayerTank = 0;
                }
                if (Input.GetButtonDown("BButton_P1"))
                {
                    firstPlayerTank = 1;
                }
                if (Input.GetButtonDown("XButton_P1"))
                {
                    firstPlayerTank = 2;
                }
                if (Input.GetButtonDown("YButton_P1"))
                {
                    firstPlayerTank = 3;
                }

                if (firstPlayerTank != -1)
                {
                    if (Input.GetButtonDown("Start_P1"))
                    {
                        playerLockIns[0] = true;
                    }
                }
            }

            if (hasSecondPlayer)
            {
                if (!playerLockIns[1])
                {
                    if (Input.GetButtonDown("AButton_P2"))
                    {
                        secondPlayerTank = 0;
                    }
                    if (Input.GetButtonDown("BButton_P2"))
                    {
                        secondPlayerTank = 1;
                    }
                    if (Input.GetButtonDown("XButton_P2"))
                    {
                        secondPlayerTank = 2;
                    }
                    if (Input.GetButtonDown("YButton_P2"))
                    {
                        secondPlayerTank = 3;
                    }

                    if (secondPlayerTank != -1)
                    {
                        if (Input.GetButtonDown("Start_P2"))
                        {
                            playerLockIns[1] = true;
                        }
                    }
                }
            }

            if (hasThirdPlayer)
            {
                if (!playerLockIns[2])
                {
                    if (Input.GetButtonDown("AButton_P3"))
                    {
                        thirdPlayerTank = 0;
                    }
                    if (Input.GetButtonDown("BButton_P3"))
                    {
                        thirdPlayerTank = 1;
                    }
                    if (Input.GetButtonDown("XButton_P3"))
                    {
                        thirdPlayerTank = 2;
                    }
                    if (Input.GetButtonDown("YButton_P3"))
                    {
                        thirdPlayerTank = 3;
                    }

                    if (thirdPlayerTank != -1)
                    {
                        if (Input.GetButtonDown("Start_P3"))
                        {
                            playerLockIns[2] = true;
                        }
                    }
                }
            }

            if (hasFourthPlayer)
            {
                if (!playerLockIns[3])
                {
                    if (Input.GetButtonDown("AButton_P4"))
                    {
                        fourthPlayerTank = 0;
                    }
                    if (Input.GetButtonDown("BButton_P4"))
                    {
                        fourthPlayerTank = 1;
                    }
                    if (Input.GetButtonDown("XButton_P4"))
                    {
                        fourthPlayerTank = 2;
                    }
                    if (Input.GetButtonDown("YButton_P4"))
                    {
                        fourthPlayerTank = 3;
                    }

                    if (fourthPlayerTank != -1)
                    {
                        if (Input.GetButtonDown("Start_P4"))
                        {
                            playerLockIns[3] = true;
                        }
                    }
                }
            }
        }
    }
}

