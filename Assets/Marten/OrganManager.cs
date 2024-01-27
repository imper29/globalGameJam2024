using System.Collections.Generic;
using UnityEngine;

public class OrganManager : MonoBehaviour
{
    [SerializeField]
    private Organ[] m_Prefabs;
    [SerializeField]
    private float m_OrganSpawnTime = 1.0f;
    [SerializeField]
    private float m_OrganSpawnTimeCooldownMin = 0.3f;
    [SerializeField]
    private float m_OrganSpawnTimeCooldownMax = 1.0f;

    private int[] m_OrgansPending = new int[(int)OrganType.COUNT];
    
    public void AddPending(OrganType wantedOrgan)
    {
        m_OrgansPending[(int)wantedOrgan]++;
    }
    public void AddPending(OrganMask wantedOrgans)
    {
        for (int i = 0; i < m_OrgansPending.Length; ++i)
            if ((wantedOrgans & (OrganMask)(1 << i)) != 0)
                m_OrgansPending[i]++;
    }
    private float GetNextSpawnTime()
    {
        return Time.time + Random.Range(m_OrganSpawnTimeCooldownMin, m_OrganSpawnTimeCooldownMax) * (1.0f - GameManager.GetDifficulty());
    }
    private Organ TrySpawnOrgan()
    {
        var options = new List<Organ>();
        for (int i = 0; i < m_Prefabs.Length; ++i)
        {
            var type = (int)m_Prefabs[i].Type;
            if (m_OrgansPending[type] != 0)
                options.Add(m_Prefabs[i]);
        }
        if (options.Count == 0)
            return null;

        var prefab = options[Random.Range(0, options.Count)];
        m_OrgansPending[(int)prefab.Type]--;
        return Instantiate(prefab, transform.position, Quaternion.identity, transform);
    }

    private void Update()
    {
        if (m_OrganSpawnTime <= Time.time)
        {
            m_OrganSpawnTime = GetNextSpawnTime();
            TrySpawnOrgan();
        }
    }
}
