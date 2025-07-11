using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyModel : MonoBehaviour
{
    [field: SerializeField] public int MaxHP { get; set; }
    [field: SerializeField] public int Attack {  get; set; }
    [field: SerializeField] public float MoveSpd { get; set; }
    [field: SerializeField] public float AttackRange { get; set; }
    [field: SerializeField] public float DetectRange { get; set; }
    public ObservableProperty<int> CurHP { get; private set; } = new();
}
