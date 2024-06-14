namespace ValueTypes;

[Serializable]
public readonly struct Email
{
    public static readonly Email Empty = new();
}
