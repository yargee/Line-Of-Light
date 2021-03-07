using UnityEngine;
using UnityEngine.Events;

public class Zombie : MonoBehaviour
{
    private const string Run = "Run";
    private const string Attack = "Attack";

    [SerializeField] private Health _health;
    [SerializeField] private Animator _animator;
    [SerializeField] private ZombieMovement _mover;
    [SerializeField] private ZombieAttack _attacker;
    [SerializeField] private BoxCollider _collider;
    [SerializeField] private Rigidbody _rigidbody;

    private readonly string[] _death = { "Death_1", "Death_2", "Death_3", "Death_4" };

    public event UnityAction<Zombie> Died;

    public Health Health => _health;
    public Animator Animator => _animator;

    private void OnEnable()
    {
        _health.StartDying += OnStartDying;
    }

    private void FixedUpdate()
    {
        _mover.Move();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Soldier soldier) || collision.gameObject.TryGetComponent(out Obstacle obstacle))
        {
            _mover.Stop();
            _animator.SetBool(Attack, true);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Soldier target))
        {
            StartCoroutine(target.Dying());
            StartCoroutine(_attacker.Delay());
        }

        if (collision.gameObject.TryGetComponent(out Obstacle obstacle) && _attacker.AttackAvailable)
        {
            obstacle.TryBreak();
            StartCoroutine(_attacker.Delay());
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        _mover.Move();
        _animator.SetBool(Attack, false);
    }

    public void Enrage()
    {
        _animator.SetBool(Run, true);
        _mover.Boost();
    }

    private void OnStartDying()
    {
        _mover.Stop();
        _health.StartDying -= OnStartDying;
        _animator.SetBool(_death[Random.Range(0, 4)], true);
        _rigidbody.isKinematic = true;
        _collider.enabled = false;
        Died?.Invoke(this);
    }
}
