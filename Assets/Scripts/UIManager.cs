using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreTxt;
    [SerializeField] private GameObject[] healthImages;
    [SerializeField] private GameObject[] bloodyPanels;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private Button gameOverButtonYes;
    [SerializeField] private Button gameOverButtonNo;
    [SerializeField] private VideoPlayer gameoverVideo;

    private int m_score;
    public int getScore => m_score;

    private void Start()
    {
        scoreTxt.text = m_score.ToString();
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

    public void UpdateScore(int newScore)
    {
        m_score = newScore;
        scoreTxt.text = m_score.ToString();
    }

    public void GameOver()
    {
        StartCoroutine(GameoverCoroutine());
    }

    IEnumerator GameoverCoroutine()
    {
        gameoverVideo.Play();
        yield return new WaitForSeconds(7.0f);
        gameoverVideo.enabled = false;
        gameOverPanel.SetActive(true);
    }
}