using System;
using UnityEngine;

public class OrganSurplus : MonoBehaviour
{
    [SerializeField]
    private Surplus[] m_SurplusOrgans;

    private void Start()
    {
        OrganManager organManager = FindObjectOfType<OrganManager>();
        for (int i = 0; i < m_SurplusOrgans.Length; ++i)
            for (int j = 0; j < m_SurplusOrgans[i].count; ++j)
                organManager.AddPending(m_SurplusOrgans[i].organ);
    }

    [Serializable]
    public struct Surplus
    {
        public OrganType organ;
        public int count;
    }
}
