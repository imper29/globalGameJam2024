using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    [SerializeField]
    private float m_CustomersPerSecond;
    [SerializeField]
    private Customer[] m_Prefabs;
    [SerializeField]
    private OrganManager m_OrganManager;

    [SerializeField]
    private float m_SecondsPerCustomerMin;
    [SerializeField]
    private float m_SecondsPerCustomerMax;

    private float m_CustomerTimeNext;
    
    private float GetNextSpawnTime()
    {
        return Time.time + Mathf.Lerp(m_SecondsPerCustomerMin, m_SecondsPerCustomerMax, GameManager.GetDifficulty());
    }
    private Customer TrySpawnCustomer()
    {
        return Instantiate(m_Prefabs[Random.Range(0, m_Prefabs.Length)]);
    }

    private void Update()
    {
        if (m_CustomerTimeNext >= Time.time)
        {
            m_CustomerTimeNext = GetNextSpawnTime();
            TrySpawnCustomer();
        }
    }
}
