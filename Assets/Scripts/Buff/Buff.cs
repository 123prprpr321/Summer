
public class Buff
{
    public enum BuffType
    {
        Bleeding,//��Ѫ
        Healing,//�ظ�
        Slowing//����
    }

    public BuffType Type { get; set; }
    public float Duration { get; set; }
    public float Amount { get; set; }

    public Buff(BuffType type, float duration, float amount)
    {
        Type = type;
        Duration = duration;
        Amount = amount;
    }
}
