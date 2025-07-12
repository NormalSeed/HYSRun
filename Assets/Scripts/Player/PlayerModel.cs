using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : MonoBehaviour
{
    [field: SerializeField] public int MaxHP { get; set; }
    [field: SerializeField] public int Attack { get; set; }
    [field: SerializeField] public float MoveSpd { get; set; }
    [field: SerializeField] public float JumpPower { get; set; }
    public ObservableProperty<int> CurHP { get; private set; } = new();
}
