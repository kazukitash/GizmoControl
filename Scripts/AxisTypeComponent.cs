using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public class AxisTypeComponent : MonoBehaviour {
  bool _isGlobal;
  public bool IsGlobal { get => _isGlobal; }
  public bool IsLocal { get => !_isGlobal; }
  public string AxisType { get => _isGlobal ? "Global" : "Local"; }
  public Action OnChangeAxisType;

  void Awake() {
    _isGlobal = false;
  }

  public void ChangeAxisType() {
    _isGlobal = !_isGlobal;
    OnChangeAxisType();
  }

  public void SetAxisType(string axisType) {
    _isGlobal = axisType == "Global";
    OnChangeAxisType();
  }
}
