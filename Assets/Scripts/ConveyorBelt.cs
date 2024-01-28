using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    [SerializeField]
    private float m_SpeedMin;
    [SerializeField]
    private float m_SpeedMax;
    [SerializeField]
    private Vector2 m_Direction;
    [SerializeField]
    private float m_BeltRendererWidth;
    [SerializeField]
    Transform m_BeltRenderer;

    private void OnTriggerStay2D(Collider2D collision)
    {
        var speed = GetSpeed();
        var body = collision.attachedRigidbody;
        if (body != null)
            body.position += speed * Time.fixedDeltaTime * m_Direction;
    }
    private void Update()
    {
        m_BeltRendererPos += Time.deltaTime * GetSpeed();
        m_BeltRenderer.transform.localPosition = m_Direction * (m_BeltRendererPos % m_BeltRendererWidth);
    }

    float GetSpeed()
    {
        return Mathf.Lerp(m_SpeedMin, m_SpeedMax, GameManager.Instance.GetDifficulty());
    }

    private float m_BeltRendererPos;
}
