using System;
using UnityEngine;

[RequireComponent(typeof(CharacterMovement), typeof(Animator), typeof(CharacterShooting))]
public  class CharacterClass: MonoBehaviour, ITakeDamage
{
    [SerializeField] protected float MaxHealth;
    [SerializeField] protected float Damage;
    [SerializeField] protected int InitiativeAmount;

    [SerializeField] protected int MaxMovementDistance;
    [SerializeField] protected int MaxAttackRange;

    [SerializeField] protected float MaxPrice;
    [SerializeField] protected float price;

    [SerializeField] protected FractionType Fraction;

    [SerializeField] protected TraitsPositive[] PositiveTraits = new TraitsPositive[3];
    [SerializeField] protected TraitsNegative[] NegativTraits = new TraitsNegative[3];

    private void Start()
    {
       // Price = SetPrice(MaxPrice);
    }

    protected enum FractionType
    {
        Prisoners,// Заключенный 
        Guardsman,// Гвардеец
        Volunteers// Волонтер
    }
    protected enum TraitsPositive
    {
        Null,// Отсутсвие Перка
        Fanning,//Позволяет несколько раз выстрелить из револьвера 
        Levering,//Позволяет выстрелить из винчистера несколько раз
        MetalJacketAmmo,// Повышает пробевные способности оружия, но понижает урон
        Salamandra,// Позволяет игнорировать урон от огня
        //IronRepiter,// Позволяет сделать второй выстрел со сниженым уроном 
        PhysicalTraining, //Увеличивает радиусь передвежения 
        WatchfulEye,// Шан нанести кретический урон врагу 
        Strongman,// Повышвет запс здоровья
    }
    protected enum TraitsNegative
    {
       Null,// Отсутсвие Перка
       FourFingerHand,// Имеете шанс не перезарядить оружие
       Myopia,// Повышает урон вблизи, но дает шанс промоха вдали 
       Farsightedness,// Понижает Урон вблизи, увеличивает вдали  
       Flimsy, // Понижает запас здоровья 
       Smoker, // Понижает дальность ходьбы 
       Pyrophobia,// Урон от огня увеличен 
       LowQualityWeapon,//Урон оружия уменьшен
       TornLigament,// Имеет шанс пропустить фазу боя
    } 

    protected void SetFraction(FractionType Fraction)
    {
        this.Fraction = Fraction;
    }

    protected void GetRandomPositiveTrait()
    {
        int RandomNumTrait = UnityEngine.Random.Range(0, Enum.GetValues(typeof(TraitsPositive)).Length);
        int RandomNumTraitPosition= UnityEngine.Random.Range(0, PositiveTraits.Length);
        PositiveTraits[RandomNumTraitPosition] = (TraitsPositive)RandomNumTrait;
    }
    protected void GetRandomNegativeTrait()
    {
        int RandomNumTrait = UnityEngine.Random.Range(0, Enum.GetValues(typeof(TraitsNegative)).Length);
        int RandomNumTraitNegative = UnityEngine.Random.Range(0, NegativTraits.Length);
        NegativTraits[RandomNumTraitNegative] = (TraitsNegative)RandomNumTrait;
    }

    protected void GetDefinitePositiveTrait(TraitsPositive Trait)
    {
        int RandomNumTraitPosition = UnityEngine.Random.Range(0, PositiveTraits.Length);
        PositiveTraits[RandomNumTraitPosition] = Trait;
    }
    protected void GetDefiniteNegativeTrait(TraitsNegative Trait)
    {
        int RandomNumTraitPosition = UnityEngine.Random.Range(0, NegativTraits.Length);
        NegativTraits[RandomNumTraitPosition] = Trait;
    }


    public int GetMaxMovementDistance()
    {
        return MaxMovementDistance;
    }

    public int GetMaxAttackRange()
    {
        return MaxAttackRange;
    }

    public int GetInitiativeAmount()
    {
        return InitiativeAmount;
    }
    public float GetDamage()
    {
        return Damage;
    }

    private void Die()
    {
        Debug.Log("Sdox");
        gameObject.SetActive(false);
        //GameObject.Destroy(gameObject);
    }

    public void TakeDamage(float Damage)
    {
        MaxHealth -= Damage;

        if (MaxHealth <= 0)
        {
            Die();
        }

        Debug.Log("Pisdanul");
    }
}

