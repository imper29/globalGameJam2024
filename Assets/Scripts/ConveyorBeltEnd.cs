using UnityEngine;

public class ConveyorBeltEnd : MonoBehaviour
{
    private OrganManager m_OrganManager;

    private void Start()
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
        var customer = collision.gameObject.GetComponent<Customer>();
        if (customer)
        {
            m_OrganManager.AddPending(customer.WantedOrgans, -1);
            Destroy(collision.gameObject);
        }
    }
}
