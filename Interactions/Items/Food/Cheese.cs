using UnityEngine;

public class Cheese : Food
{
    public override float healthGained => 25;
    public override float staminaGained => 15;
    public override float badProbability => 0.2f;
}