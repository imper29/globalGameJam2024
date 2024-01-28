using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu]
public class CustomerOrderPool : ScriptableObject
{
    /// <summary>
    /// The customer orders.
    /// </summary>
    public CustomerOrder[] Orders => m_Orders;

    /// <summary>
    /// Returns a random customer order.
    /// </summary>
    /// <param name="difficulty">The difficulty.</param>
    /// <returns>A random customer order.</returns>
    public CustomerOrder GetRandomOrder(float difficulty)
    {
        var orders = GetOptions(m_Orders, difficulty).OrderByDescending(o => o.Weight).ToArray();
        var weight = orders.Sum(o => o.Weight);
        var rand = Random.Range(0.0f, weight);
        for (int i = 0; i < orders.Length; ++i)
        {
            if (rand <= orders[i].Weight)
                return orders[i];
            rand -= orders[i].Weight;
        }
        return orders[orders.Length - 1];
        
        static IEnumerable<CustomerOrder> GetOptions(CustomerOrder[] orders, float difficulty, float weight = 1.0f)
        {
            IEnumerable<CustomerOrder> results = Enumerable.Empty<CustomerOrder>();
            foreach (var order in orders)
            {
                if (order.DifficultyMin <= difficulty && order.DifficultyMax >= difficulty)
                {
                    if (order.Pool)
                        results = results.Concat(GetOptions(order.Pool.Orders, difficulty, weight * order.Weight));
                    else
                    {
                        var o = order;
                        o.Weight *= weight;
                        results = results.Append(o);
                    }
                }
            }
            return results;
        }
    }

    [SerializeField]
    private CustomerOrder[] m_Orders;

    [System.Serializable]
    public struct CustomerOrder
    {
        /// <summary>
        /// The weight.
        /// </summary>
        public float Weight
        {
            get => m_Weight;
            set => m_Weight = value;
        }
        /// <summary>
        /// The minimum difficulty.
        /// </summary>
        public readonly float DifficultyMin => m_DifficultyMin;
        /// <summary>
        /// The maximum difficulty.
        /// </summary>
        public readonly float DifficultyMax => m_DifficultyMax;
        /// <summary>
        /// The organs ordered.
        /// </summary>
        public readonly OrganMask Organs => m_Organs;
        /// <summary>
        /// The subpool to query if not null.
        /// </summary>
        public readonly CustomerOrderPool Pool => m_Pool;

        [SerializeField]
        private float m_Weight, m_DifficultyMin, m_DifficultyMax;
        [SerializeField]
        private OrganMask m_Organs;
        [SerializeField]
        private CustomerOrderPool m_Pool;
    }
}
