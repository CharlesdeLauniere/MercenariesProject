
namespace MercenariesProject
{
    public enum Stats
    {
        Health,
        Mana,
        Strength,
        Endurance,
        Speed,
        Intelligence,
        MoveRange,
        AttackRange,
        CurrentHealth,
        CurrentMana
    }

    public enum Operation
    {
        Add,
        Minus,
        Multiply,
        Divide,
        AddByPercentage,
        MinusByPercentage,
        AddMana
    }

    public enum TileTypes
    {
        Traversable,
        NonTraversable,
        Effect
    }
}