using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance => s_Instance;

    [SerializeField]
    private float m_CurveStartTime;
    [SerializeField]
    private float m_CurveEndTime;
    [SerializeField]
    private AnimationCurve m_DifficultyCurve;

    private void Awake()
    {
        s_Instance = this;
        m_StartTime = Time.time;
    }

    /// <summary>
    /// Returns the current difficulty between zero and one inclusive.
    /// </summary>
    /// <remarks>This will be reimplemented later so that difficulty scales more slowly and is reduced when the character loses organs.</remarks>
    /// <returns>The current difficulty.</returns>
    public float GetDifficulty()
    {
        float evalPoint = Mathf.Clamp01(Mathf.InverseLerp(m_CurveStartTime, m_CurveEndTime, Time.time - m_StartTime));
        float difficulty = s_Instance.m_DifficultyCurve.Evaluate(evalPoint);
        return difficulty;
    }

    private static GameManager s_Instance;
    private static float m_StartTime;
}
