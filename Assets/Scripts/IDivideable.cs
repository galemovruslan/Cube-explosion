using UnityEngine;

public interface IDivideable
{
    public float DivisionChance { get; }

    public void Initialize(Color color, float scale, float chance);
}