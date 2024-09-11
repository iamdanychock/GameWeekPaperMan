using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.IsartDigital.PaperMan;

public class Zipline : Interactable
{
    [SerializeField] float UP_DISTANCE;
    [SerializeField] float SEC_TO_GO_UP = 1;

    [SerializeField] bool _isUp = false;

    [SerializeField] AnimationCurve _curve;

    [SerializeField] Zipline ziplineFriend;

    int _direction => _isUp ? -1 : 1;
    float _posOnCurve;

    Vector3 _startPosition;

    protected override void Interact()
    {
        if (_posOnCurve != 0 && _posOnCurve != 1)
            return;

        Activate();
        ziplineFriend?.Activate();

        Player.Instance.SetModZipline(this);
    }

    public void Activate()
    {
        if (_posOnCurve != 0 && _posOnCurve != 1)
            return;

        if (_posOnCurve > .5)
            _posOnCurve -= Time.deltaTime;
        else
            _posOnCurve += Time.deltaTime;
    }

    protected override void Start()
    {
        base.Start();

        _posOnCurve = _isUp ? 1 : 0;

        _startPosition = transform.position;

        if (_isUp)
            transform.position = _startPosition + Vector3.up * UP_DISTANCE;
    }

    protected override void Update()
    {
        base.Update();

        if (_posOnCurve == 0 || _posOnCurve == 1)
            return;

        _posOnCurve += _direction * Time.deltaTime / SEC_TO_GO_UP;

        if (_posOnCurve < 0 || _posOnCurve > 1)
        {
            Player.Instance.SetModNormal();
            _posOnCurve = Mathf.RoundToInt(_posOnCurve);

            _isUp = !_isUp;
        }

        transform.position = Vector3.Lerp(_startPosition,_startPosition + Vector3.up * UP_DISTANCE, _curve.Evaluate(_posOnCurve));

    }
}
