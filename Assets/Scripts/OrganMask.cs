[System.Flags]
public enum OrganMask
{
    Brain = 1 << 0,
    Foot = 1 << 1,
    Arm = 1 << 2,
    Liver = 1 << 3,
    Heart = 1 << 4,
    Eye = 1 << 5,
    Kidney = 1 << 6,
    Lungs = 1 << 7,
    Stomach = 1 << 8,
    Ear = 1 << 9,
    Internal = Liver | Heart | Kidney | Lungs | Stomach,
    External = Brain | Arm | Foot | Eye
}
public enum OrganType
{
    None = -1,
    Brain = 0,
    Foot = 1,
    Arm = 2,
    Liver = 3,
    Heart = 4,
    Eye = 5,
    Kidney = 6,
    Lungs = 7,
    Stomach = 8,
    Ear = 9,
    COUNT = 10
}
