using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MainInput
{
    [SerializeField]
    private Vector3 _cameraOffset = new Vector3(0, 10, -2.5f);
    [SerializeField]
    private Vector3 _targetOffset = new Vector3(0, 1, 0);

    private MainComponent _characterMainComponent;

    private TopDownController _topDownController;

    private Camera _cameraMain;
    private Transform _cameraTransform;

    private Vector3 _targetPosition;
    private Vector3 _inputAxis;
    private bool _isAttack;

    private RaycastHit _hit;

    private void Start()
    {
        _characterMainComponent = GetComponent<CharacterMain>();
        _cameraMain = Camera.main;
        _cameraTransform = _cameraMain.transform;
        _topDownController = new TopDownController(_cameraOffset, _targetOffset, transform);
    }

    private void LateUpdate()
    {
        _topDownController.CountInputAxis(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"), out _inputAxis);

        _isAttack = Input.GetMouseButton(0);

        Ray ray = _cameraMain.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out _hit, 100f, -1, QueryTriggerInteraction.Ignore))
        {
            _targetPosition = Vector3.Lerp(_targetPosition, _hit.point + _targetOffset, Time.deltaTime * 5f);
            if (Input.GetKeyUp("e"))
            {
                ItemOnGround item =_hit.transform.GetComponent<ItemOnGround>();
                if (item)
                {
                    GameObject weapon;
                    item.PickUp(out weapon);
                    _characterMainComponent.SetWeapon(weapon, Input.GetMouseButton(1));
                }
                else
                {
                    _characterMainComponent.RemoveWeapon();
                }
            }
        }

        _characterMainComponent.SetInputs(_inputAxis, _targetPosition, _isAttack);
        _topDownController.CountCameraMovement(ref _cameraTransform);
    }
}
