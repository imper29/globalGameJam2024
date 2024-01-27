using UnityEngine;

public class CharacterHurtOverlay : MonoBehaviour
{
    [SerializeField]
    private float m_OpacityMax;
    [SerializeField]
    private float[] m_PlayerHurtIntensity;
    [SerializeField]
    private AnimationCurve m_EasingCurve;

    private void Awake()
    {
        m_Character = FindObjectOfType<Character>();
        m_OverlayRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        Color color = m_OverlayRenderer.color;
        color.a = m_EasingCurve.Evaluate(GameManager.Instance.GetEasing()) + m_PlayerHurtIntensity[0];//TODO: Use damage from character script.
        color.a *= m_OpacityMax;
        m_OverlayRenderer.color = color;
    }

    private Character m_Character;
    private SpriteRenderer m_OverlayRenderer;
}
