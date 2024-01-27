using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance => s_Instance;

    [SerializeField]
    private float m_DifficultyStartTime;
    [SerializeField]
    private float m_DifficultyEndTime;
    [SerializeField]
    private AnimationCurve m_DifficultyCurve;

    [SerializeField]
    private float m_EasingDuration;
    [SerializeField]
    private AnimationCurve m_EasingCurve;

    private void Awake()
    {
        s_Instance = this;
        m_StartTime = Time.time;
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
        float difficultyEvalPoint = Mathf.Clamp01(Mathf.InverseLerp(m_DifficultyStartTime, m_DifficultyEndTime, Time.time - m_StartTime));
        float difficulty = m_DifficultyCurve.Evaluate(difficultyEvalPoint);
        float easingEvalPoint = Mathf.Clamp01(Mathf.InverseLerp(m_EasingStartTime, m_EasingStartTime + m_EasingDuration, Time.time));
        float easing = m_EasingCurve.Evaluate(easingEvalPoint);
        return difficulty - easing;
    }

    private static GameManager s_Instance;
    private static float m_EasingStartTime;
    private static float m_StartTime;
}
