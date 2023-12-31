using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HologramTEst : MonoBehaviour
{
    [SerializeField] Material _normalMat;
    [SerializeField] Material _phaseMat;

    [SerializeField] float _phaseValue = 0f; // 0 ~ 3.5����
    private float _phaseMaxValue = 3.5f;
    Renderer _rend;
    void Start()
    {
        _rend = GetComponent<Renderer>();
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(PhaseCo());
        }
    }
    IEnumerator PhaseCo()
    {
        _rend.material = _phaseMat;

        while(_phaseValue < _phaseMaxValue)
        {
            _rend.material.SetFloat("_SplitValue", _phaseValue);
            _phaseValue += 0.02f;
            yield return new WaitForEndOfFrame();
        }

        _rend.material = _rend.material = _normalMat;
    }
}
