using UnityEngine;

[CreateAssetMenu]
public class CustomerSpriteTable : ScriptableObject
{
    [SerializeField]
    private Sprite[] m_Organs;
    [SerializeField]
    private Sprite[] m_WantedExternal;
    [SerializeField]
    private Sprite[] m_WantedInternal;
    [SerializeField]
    private Sprite[] m_WantedInternalExternal;
    
    public Sprite GetSprite(OrganType organ)
    {
        if (organ == OrganType.None)
            return null;
        return m_Organs[(int)organ];
    }
    public Sprite GetSprite(OrganMask wantedCurrent, OrganMask wantedOriginal)
    {
        OrganType wantedInternal = GetInternalOrgan(wantedOriginal);
        OrganType wantsInternal = GetInternalOrgan(wantedCurrent);
        OrganType wantedExternal = GetExternalOrgan(wantedOriginal);
        OrganType wantsExternal = GetExternalOrgan(wantedCurrent);

        if (wantedInternal != OrganType.None)
        {
            if (wantedExternal != OrganType.None)
            {
                //Wanted internal and external.
                var externalOrganCount = GetExternalOrganSpriteCount();
                var internalOrganIndex = GetInternalOrganSpriteIndex(wantedInternal, wantsInternal != OrganType.None);
                var externalOrganIndex = GetExternalOrganSpriteIndex(wantedExternal, wantsExternal != OrganType.None);
                return m_WantedInternalExternal[internalOrganIndex * externalOrganCount + externalOrganIndex];
            }
            else
            {
                //Wanted internal.
                var internalOrganIndex = GetInternalOrganSpriteIndex(wantedInternal, wantsInternal != OrganType.None);
                return m_WantedInternal[internalOrganIndex];
            }
        }
        else
        {
            if (wantedExternal != OrganType.None)
            {
                //Wanted external.
                var externalOrganIndex = GetExternalOrganSpriteIndex(wantedExternal, wantsExternal != OrganType.None);
                return m_WantedExternal[externalOrganIndex];
            }
            else
            {
                return null;
            }
        }
    }

    public static OrganType GetInternalOrgan(OrganMask mask)
    {
        mask &= OrganMask.Internal;
        switch (mask)
        {
            case OrganMask.Liver:
                return OrganType.Liver;
            case OrganMask.Heart:
                return OrganType.Heart;
            case OrganMask.Kidney:
                return OrganType.Kidney;
            case OrganMask.Lungs:
                return OrganType.Lungs;
            case OrganMask.Stomach:
                return OrganType.Stomach;
            default:
                return OrganType.None;
        }
    }
    public static OrganType GetExternalOrgan(OrganMask mask)
    {
        mask &= OrganMask.External;
        switch (mask)
        {
            case OrganMask.Arm:
                return OrganType.Arm;
            case OrganMask.Foot:
                return OrganType.Foot;
            case OrganMask.Brain:
                return OrganType.Brain;
            case OrganMask.Eye:
                return OrganType.Eye;
            case OrganMask.Ear:
                return OrganType.Ear;
            default:
                return OrganType.None;
        }
    }

    public static int GetInternalOrganSpriteIndex(OrganType wanted, bool wants)
    {
        return wants ? 0 : 1;
    }
    public static int GetInternalOrganSpriteCount()
    {
        return 2;
    }
    public static int GetExternalOrganSpriteIndex(OrganType wanted, bool wants)
    {
        int offset = wants ? 0 : 1;
        switch (wanted)
        {
            case OrganType.Arm:
                return 0 + offset;
            case OrganType.Brain:
                return 2 + offset;
            case OrganType.Ear:
                return 4 + offset;
            case OrganType.Eye:
                return 6 + offset;
            case OrganType.Foot:
            default:
                return 8 + offset;
        }
    }
    public static int GetExternalOrganSpriteCount()
    {
        return 10;
    }
}
