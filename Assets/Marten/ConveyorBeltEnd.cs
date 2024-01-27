using UnityEngine;

public class ConveyorBeltEnd : MonoBehaviour
{
    private OrganManager m_OrganManager;

    private void Awake()
    {
        m_OrganManager = FindObjectOfType<OrganManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var organ = collision.gameObject.GetComponent<Organ>();
        if (organ)
        {
            m_OrganManager.AddPending(organ.Type);
            Destroy(collision.gameObject);
        }
    }
}