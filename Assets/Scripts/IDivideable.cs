using UnityEngine;

public interface IDivideable
{
    public float DivisionChance { get; }

    void SetColor(Color color);
    void SetScale(float scale);

    void SetChance(float chance);
}