using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum Type { Sword };
    public Type type;
    public int damage;
    public float rate;
    public BoxCollider swordArea;
    public TrailRenderer trailEffect;

}
