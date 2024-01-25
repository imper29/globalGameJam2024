using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance => s_Instance;

    [SerializeField] private float m_DifficultyStartTime;
    [SerializeField] private float m_DifficultyEndTime;
    [SerializeField] private AnimationCurve m_DifficultyCurve;

    [SerializeField] private float m_EasingDuration;
    [SerializeField] private AnimationCurve m_EasingCurve;

    [SerializeField] private TMPro.TextMeshProUGUI m_ScoreRenderer;
    [SerializeField] private SpriteRenderer m_HealthRenderer;
    [SerializeField] private Sprite[] m_HealthSprites;

    [SerializeField] private Character player;
    [SerializeField] private UIManager uiManager;
    private bool m_IsGameOver;
    public bool isGameOver => m_IsGameOver;

    private void Awake()
    {
        s_Instance = this;
        m_StartTime = Time.time;
        m_EasingStartTime = Time.time - m_EasingDuration;
    }

    /// <summary>
    /// Starts a grace period.
    /// </summary>
    public void StartGracePeriod()
    {
        m_EasingStartTime = Time.time;
    }

    /// <summary>
    /// Returns the current difficulty between zero and one inclusive.
    /// </summary>
    /// <remarks>This will be reimplemented later so that difficulty scales more slowly and is reduced when the character loses organs.</remarks>
    /// <returns>The current difficulty.</returns>
    public float GetDifficulty()
    {
        float difficultyEvalPoint =
            Mathf.Clamp01(Mathf.InverseLerp(m_DifficultyStartTime, m_DifficultyEndTime, Time.time - m_StartTime));
        float difficulty = m_DifficultyCurve.Evaluate(difficultyEvalPoint);
        return difficulty - GetEasing();
    }

    /// <summary>
    /// Returns the current easing value between zero and one inclusive.
    /// </summary>
    /// <returns>The current easing value.</returns>
    public float GetEasing()
    {
        float easingEvalPoint =
            Mathf.Clamp01(Mathf.InverseLerp(m_EasingStartTime, m_EasingStartTime + m_EasingDuration, Time.time));
        return m_EasingCurve.Evaluate(easingEvalPoint);
    }

    private int m_PlayerMessedUpTimes;

    public void PlayerMessedUp()
    {
        if (m_IsGameOver)
            return;
        m_PlayerMessedUpTimes++;

        if (m_PlayerMessedUpTimes >= 3)
        {
            m_IsGameOver = true;
            uiManager.GameOver();
            return;
        }
        player.PlayerMessedUp(m_PlayerMessedUpTimes);
        uiManager.Damage(m_PlayerMessedUpTimes);
    }

    private static GameManager s_Instance;
    private static float m_EasingStartTime;
    private static float m_StartTime;
}