using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    [SerializeField]
    private Customer m_Prefab;
    [SerializeField]
    private CustomerOrderPool m_OrderPool;

    [SerializeField]
    private float m_SecondsPerCustomerMin;
    [SerializeField]
    private float m_SecondsPerCustomerMax;

    private float m_CustomerTimeNext;
    private OrganManager m_OrganManager;
    
    private float GetNextSpawnTime()
    {
        return Time.time + Mathf.Lerp(m_SecondsPerCustomerMin, m_SecondsPerCustomerMax, 1.0f - GameManager.Instance.GetDifficulty());
    }
    private Customer TrySpawnCustomer()
    {
        Customer customer = Instantiate(m_Prefab, transform.position, Quaternion.identity, transform);
        customer.Initialize(m_OrderPool.GetRandomOrder(GameManager.Instance.GetDifficulty()).Organs);
        return customer;
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
