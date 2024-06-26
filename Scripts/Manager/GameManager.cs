using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [HideInInspector] public StageSystem StageSystem;

    [field: Header("Reference")]
    [field: SerializeField] public PlayerStat Data { get; private set; }

    public float MaxHP { get; set; }
    public float HP { get; set; }
    public float Attack { get; set; }
    public float AttackSpeed { get; set; }
    public float CriticalRate { get; set; }
    public float CriticalDamage { get; set; }
    public float Defence { get; set; }

    public float Damage { get; set; }
    public float AttackInterval { get; set; }

    public Currency currency;

    public Player Player;
    [HideInInspector] public DropSystem DropSystem;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        StageSystem = GetComponent<StageSystem>();
        DropSystem = GetComponent<DropSystem>();
        InitStat();
    }

    private void Start()
    {
        
    }

    public void InitStat()
    {
        MaxHP = Data.PlayerHP;
        Attack = Data.PlayerAttack;
        AttackSpeed = Data.PlayerSpeed;
        CriticalRate = Data.CriticalRate;
        CriticalDamage = Data.CriticalDamage;
        Defence = Data.PlayerDefense;
        HP = MaxHP;
    }
}
