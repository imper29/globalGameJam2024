using UnityEngine;

public class Organ : MonoBehaviour
{
    public OrganType Type => m_Type;
    public bool isInPlayerHands;
    [SerializeField]
    private OrganType m_Type;
}
