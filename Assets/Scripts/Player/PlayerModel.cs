using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : MonoBehaviour
{
    [field: SerializeField] public int MaxHP { get; set; } = 5;
    [field: SerializeField] public int Attack { get; set; } = 1;
    [field: SerializeField] public float MoveSpd { get; set; } = 5;
    public ObservableProperty<int> CurHP { get; private set; } = new();
    public ObservableProperty<float> CurSpd { get; private set; } = new();
}
