
public class Buff
{
    public enum BuffType
    {
        Bleeding,//Á÷Ñª
        Healing,//»Ø¸´
        Slowing//¼õËÙ
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
