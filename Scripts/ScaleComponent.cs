using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleComponent : MonoBehaviour {
  [SerializeField] float _scale;

  Transform _camera;

  void Awake() {
    _camera = Camera.main.transform;
  }

  void Update() {
    ChangeScale();
  }

  void ChangeScale() {
    var scale = _scale * (_camera.position - transform.position).magnitude;
    transform.localScale = new Vector3(scale, scale, scale);
  }
}
