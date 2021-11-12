using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibilityComponent : MonoBehaviour {
  public bool IsShowGizmo { get; protected set; }
  public bool IsShowMoveHundle { get; protected set; }
  public bool IsShowRotateHundle { get; protected set; }
  public bool IsShowScaleHundle { get; protected set; }

  Transform _camera;
  GameObject _moveHundle;
  GameObject _rotateHundle;
  GameObject _scaleHundle;

  Transform[,] _movePlane = new Transform[3, 4];

  void Awake() {
    _camera = Camera.main.transform;
    IsShowGizmo = false;
    IsShowMoveHundle = true;
    IsShowRotateHundle = false;
    IsShowScaleHundle = false;

    _moveHundle = transform.Find("MoveHundle").gameObject;
    _rotateHundle = transform.Find("RotateHundle").gameObject;
    _scaleHundle = transform.Find("ScaleHundle").gameObject;
    ChangeVisibility();

    _movePlane[0, 0] = transform.Find("MoveHundle/MoveXPlane1");
    _movePlane[0, 1] = transform.Find("MoveHundle/MoveXPlane2");
    _movePlane[0, 2] = transform.Find("MoveHundle/MoveXPlane3");
    _movePlane[0, 3] = transform.Find("MoveHundle/MoveXPlane4");
    _movePlane[1, 0] = transform.Find("MoveHundle/MoveYPlane1");
    _movePlane[1, 1] = transform.Find("MoveHundle/MoveYPlane2");
    _movePlane[1, 2] = transform.Find("MoveHundle/MoveYPlane3");
    _movePlane[1, 3] = transform.Find("MoveHundle/MoveYPlane4");
    _movePlane[2, 0] = transform.Find("MoveHundle/MoveZPlane1");
    _movePlane[2, 1] = transform.Find("MoveHundle/MoveZPlane2");
    _movePlane[2, 2] = transform.Find("MoveHundle/MoveZPlane3");
    _movePlane[2, 3] = transform.Find("MoveHundle/MoveZPlane4");
    ChangeMovePlaneVisibility();

    GetComponent<AxisTypeComponent>().OnChangeAxisType += CheckVisibility;
  }

  void CheckVisibility() {
    if (GetComponent<AxisTypeComponent>().IsGlobal && IsShowScaleHundle) {
      ShowMoveHundle();
    }
  }

  void Update() {
    ChangeMovePlaneVisibility();
  }

  void ChangeVisibility() {
    gameObject.SetActive(IsShowGizmo);
    _moveHundle.SetActive(IsShowMoveHundle);
    _rotateHundle.SetActive(IsShowRotateHundle);
    _scaleHundle.SetActive(IsShowScaleHundle);
  }

  void ChangeMovePlaneVisibility() {
    for (var iAxis = 0; iAxis < _movePlane.GetLength(0); iAxis++) {
      var minLength = 1f / 0f;
      var index = 0;
      for (var iPlane = 0; iPlane < _movePlane.GetLength(1); iPlane++) {
        _movePlane[iAxis, iPlane].gameObject.SetActive(false);
        var length = (_movePlane[iAxis, iPlane].position - _camera.position).magnitude;
        if (length < minLength) {
          _movePlane[iAxis, index].gameObject.SetActive(false);
          _movePlane[iAxis, iPlane].gameObject.SetActive(true);
          minLength = length;
          index = iPlane;
        }
      }
    }
  }

  public void ShowGizmo() {
    IsShowGizmo = true;
    ChangeVisibility();
  }

  public void HideGizmo() {
    IsShowGizmo = false;
    ChangeVisibility();
  }

  public void ShowMoveHundle() {
    IsShowMoveHundle = true;
    IsShowRotateHundle = false;
    IsShowScaleHundle = false;
    ChangeVisibility();
  }
  public void ShowRotateHundle() {
    IsShowMoveHundle = false;
    IsShowRotateHundle = true;
    IsShowScaleHundle = false;
    ChangeVisibility();
  }
  public void ShowScaleHundle() {
    IsShowMoveHundle = false;
    IsShowRotateHundle = false;
    IsShowScaleHundle = true;
    GetComponent<AxisTypeComponent>().SetAxisType("Local");
    ChangeVisibility();
  }
}
