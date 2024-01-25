using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreTxt;
    [SerializeField] private GameObject[] healthImages;
    [SerializeField] private GameObject[] bloodyPanels;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private Button gameOverButtonYes;
    [SerializeField] private Button gameOverButtonNo;

    private int m_Score;
    public int Score => m_Score;

    private void Start()
    {
        scoreTxt.text = m_Score.ToString();
        foreach (var image in healthImages)
            image.SetActive(false);
        foreach (var panel in bloodyPanels)
            panel.gameObject.SetActive(false);
        gameOverPanel.SetActive(false);

        gameOverButtonYes.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        });

        gameOverButtonNo.onClick.AddListener(() => { SceneManager.LoadScene(0); });
    }

    public void Damage(int messedUpTimes)
    {
        healthImages[messedUpTimes - 1].SetActive(true);
        bloodyPanels[messedUpTimes - 1].SetActive(true);
    }

    public void UpdateScore(int addedScore)
    {
        m_Score += addedScore;
        scoreTxt.text = m_Score.ToString();
    }

    public void GameOver() => gameOverPanel.SetActive(true);
}