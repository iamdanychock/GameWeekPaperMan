using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zipline : MonoBehaviour
{
    [SerializeField] float UP_DISTANCE;
    [SerializeField] float SEC_TO_GO_UP = 1;

    [SerializeField] bool _isUp = false;

    [SerializeField] AnimationCurve _curve;

    int _direction => _isUp ? -1 : 1;
    float _posOnCurve;

    Vector3 _startPosition;

    public void Activate() 
    {
        Debug.Log(_posOnCurve);

        if (_posOnCurve > .5)
            _posOnCurve -= Time.deltaTime;
        else
            _posOnCurve += Time.deltaTime;

        Debug.Log(_posOnCurve);
    }

    private void Start()
    {
        _posOnCurve = _isUp ? 1 : 0;

        _startPosition = transform.position;
    }

    private void Update()
    {
        if (_posOnCurve == 0 || _posOnCurve == 1)
            return;

        _posOnCurve += _direction * Time.deltaTime / SEC_TO_GO_UP;

        Debug.Log(_posOnCurve + " " + _direction);

        if (_posOnCurve < 0 || _posOnCurve > 1)
        {
            Debug.Log("ff " + _posOnCurve);
            Player.Instance.SetModNormal();
            _posOnCurve = Mathf.RoundToInt(_posOnCurve);

            _isUp = !_isUp;
        }

        transform.position = Vector3.Lerp(_startPosition,_startPosition + Vector3.up * UP_DISTANCE, _curve.Evaluate(_posOnCurve));

    }
}
