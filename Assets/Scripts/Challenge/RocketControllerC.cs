using UnityEngine;
using UnityEngine.InputSystem;

public class RocketControllerC : MonoBehaviour
{
    private EnergySystemC _energySystem;
    private RocketMovementC _rocketMovement;
    
    private bool _isMoving;
    private bool _isFirstPressed;//두번 입력 체크
    private float _movementDirection;

    private float Timer = 0.0f;//입력 공백 체크
    private readonly float ENERGY_TURN = 0.5f;
    private readonly float ENERGY_BURST = 2f;

    private void Awake()
    {
        _energySystem = GetComponent<EnergySystemC>();
        _rocketMovement = GetComponent<RocketMovementC>();
    }
    //입력이 장시간 없으면 입력 상황 초기화
    private void Update()
    {
        Timer += Time.deltaTime;
        if (Timer > 2f)
        {
            _isFirstPressed = false;
            Timer = 0;
        }
    }
    private void FixedUpdate()
    {
        if (!_isMoving) return;
        
        if(!_energySystem.UseEnergy(Time.fixedDeltaTime * ENERGY_TURN)) return;
        
        _rocketMovement.ApplyMovement(_movementDirection);
    }

    public void OnMove(InputValue val)
    {
        Vector2 dir = val.Get<Vector2>();
        _rocketMovement.ApplyMovement(dir.x);
    }

    public void OnBoost(InputValue val)
    {
        if (val.isPressed && !_isFirstPressed)
        {
            _isFirstPressed = true;
            return;
        }

        if (_isFirstPressed)
        {
            if (!_energySystem.UseEnergy(ENERGY_BURST)) return;
            _rocketMovement.ApplyBoost();
            _isFirstPressed = false;
        }
    }

    
}