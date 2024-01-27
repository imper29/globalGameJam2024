using UnityEngine;

[CreateAssetMenu]
public class Organ : MonoBehaviour
{
    public OrganType Type => m_Type;

    [SerializeField]
    private OrganType m_Type;
}
