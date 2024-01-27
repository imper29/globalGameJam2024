using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    [SerializeField]
    private Customer[] m_Prefabs;

    [SerializeField]
    private float m_SecondsPerCustomerMin;
    [SerializeField]
    private float m_SecondsPerCustomerMax;

    private float m_CustomerTimeNext;
    private OrganManager m_OrganManager;
    
    private float GetNextSpawnTime()
    {
        return Time.time + Mathf.Lerp(m_SecondsPerCustomerMin, m_SecondsPerCustomerMax, GameManager.Instance.GetDifficulty());
    }
    private Customer TrySpawnCustomer()
    {
        return Instantiate(m_Prefabs[Random.Range(0, m_Prefabs.Length)]);
    }

    private void Awake()
    {
        m_OrganManager = FindObjectOfType<OrganManager>();
    }
    private void Update()
    {
        if (m_CustomerTimeNext <= Time.time)
        {
            m_CustomerTimeNext = GetNextSpawnTime();
            var customer = TrySpawnCustomer();
            if (customer)
                m_OrganManager.AddPending(customer.WantedOrgans);
        }
    }
}
