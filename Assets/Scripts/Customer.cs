using System;
using UnityEngine;

public class Customer : MonoBehaviour
{
    public OrganMask WantedOrgans => m_WantedOrgans;
    public OrganMask WantedOrgansOriginal => m_WantedOrgansOriginal;

    public void Initialize(OrganMask order)
    {
        m_WantedOrgans = order;
        m_WantedOrgansOriginal = order;
        RefreshSprites();
        
    }
    public void ImplantOrgan(Organ organ)
    {
        OrganMask mask = (OrganMask)(1 << (int)organ.Type);
        if ((m_WantedOrgans & mask) == 0)
        {
            //Gave organ customer did not want.
            FindObjectOfType<OrganManager>().AddPending(mask & (~m_WantedOrgans));
            GameManager.Instance.PlayerMessedUp();
            Debug.Log("Customer angry!");
        }
        else
        {
            //Gave organ customer did want.
            GameManager.Instance.AddScore();
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
        RefreshSprites();
    }
    private void RefreshSprites()
    {
        m_BodyRenderer.sprite = m_SpriteTable.GetSprite(m_WantedOrgans, m_WantedOrgansOriginal);

        OrganType internalRequired = CustomerSpriteTable.GetInternalOrgan(m_WantedOrgans);
        OrganType externalRequired = CustomerSpriteTable.GetExternalOrgan(m_WantedOrgans);
        if (internalRequired == OrganType.None && externalRequired == OrganType.None)
        {
            m_BubbleRenderer.enabled = false;
            m_BubbleRendererOrganC.enabled = false;
            m_BubbleRendererOrganL.enabled = false;
            m_BubbleRendererOrganR.enabled = false;
        }
        else if (internalRequired == OrganType.None || externalRequired == OrganType.None)
        {
            //Requires one.
            m_BubbleRenderer.size = new(1.208333f, 1.208333f);
            if (internalRequired == OrganType.None)
            {
                m_BubbleRendererOrganC.sprite = m_SpriteTable.GetSprite(externalRequired);
                m_BubbleRendererOrganC.enabled = true;
                m_BubbleRendererOrganL.enabled = false;
                m_BubbleRendererOrganR.enabled = false;
            }
            else
            {
                m_BubbleRendererOrganC.sprite = m_SpriteTable.GetSprite(internalRequired);
                m_BubbleRendererOrganC.enabled = true;
                m_BubbleRendererOrganL.enabled = false;
                m_BubbleRendererOrganR.enabled = false;
            }
        }
        else
        {
            //Requires two.
            m_BubbleRenderer.size = new(2.0f, 1.208333f);
            m_BubbleRendererOrganR.sprite = m_SpriteTable.GetSprite(externalRequired);
            m_BubbleRendererOrganL.sprite = m_SpriteTable.GetSprite(internalRequired);
            m_BubbleRendererOrganC.enabled = false;
            m_BubbleRendererOrganL.enabled = true;
            m_BubbleRendererOrganR.enabled = true;
        }

    }

    private void OnDestroy()
    {
        if (m_WantedOrgans != 0)
        {
            GameManager.Instance.PlayerMessedUp();
        }
    }

    [SerializeField]
    private SpriteRenderer m_BodyRenderer;
    [SerializeField]
    private SpriteRenderer m_BubbleRenderer;
    [SerializeField]
    private SpriteRenderer m_BubbleRendererOrganL;
    [SerializeField]
    private SpriteRenderer m_BubbleRendererOrganR;
    [SerializeField]
    private SpriteRenderer m_BubbleRendererOrganC;
    [SerializeField]
    private CustomerSpriteTable m_SpriteTable;

    private OrganMask m_WantedOrgans;
    private OrganMask m_WantedOrgansOriginal;

    private static OrganMask[] s_Orders =
    {
        OrganMask.Brain,
        OrganMask.Foot,
        OrganMask.Arm,
        OrganMask.Eye,

        OrganMask.Liver,
        OrganMask.Heart,
        OrganMask.Kidney,
        OrganMask.Lungs,
        OrganMask.Stomach,

        OrganMask.Liver | OrganMask.Brain,
        OrganMask.Heart | OrganMask.Brain,
        OrganMask.Kidney | OrganMask.Brain,
        OrganMask.Lungs | OrganMask.Brain,
        OrganMask.Stomach | OrganMask.Brain,

        OrganMask.Liver | OrganMask.Foot,
        OrganMask.Heart | OrganMask.Foot,
        OrganMask.Kidney | OrganMask.Foot,
        OrganMask.Lungs | OrganMask.Foot,
        OrganMask.Stomach | OrganMask.Foot,

        OrganMask.Liver | OrganMask.Arm,
        OrganMask.Heart | OrganMask.Arm,
        OrganMask.Kidney | OrganMask.Arm,
        OrganMask.Lungs | OrganMask.Arm,
        OrganMask.Stomach | OrganMask.Arm,

        OrganMask.Liver | OrganMask.Eye,
        OrganMask.Heart | OrganMask.Eye,
        OrganMask.Kidney | OrganMask.Eye,
        OrganMask.Lungs | OrganMask.Eye,
        OrganMask.Stomach | OrganMask.Eye,
    };
}
