using UnityEngine;

public class Customer : MonoBehaviour
{
    public OrganMask WantedOrgans => m_WantedOrgans;

    public void ImplantOrgan(Organ organ)
    {
        OrganMask mask = (OrganMask)(1 << (int)organ.Type);
        if ((m_WantedOrgans & mask) == 0)
        {
            //Gave organ customer did not want.
            Debug.Log("Customer angry!");
        }
        else
        {
            //Gave organ customer did want.
            m_WantedOrgans &= ~mask;
            if (m_WantedOrgans == 0)
            {
                //Customer is fully satisfied.
                Debug.Log("Customer satisifed!");
            }
            else
            {
                //Customer is partially satisified.
                Debug.Log("Customer happy!");
            }
        }
        Destroy(organ.gameObject);
    }
    
    [SerializeField]
    private OrganMask m_WantedOrgans;
}
