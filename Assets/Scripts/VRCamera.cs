using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;

public class VRCamera : NetworkBehaviour
{
  [SerializeField]
  Color rayColor = Color.green;
  [SerializeField, Range(0.1f, 100f)]
  float rayDistance = 5f;
  [SerializeField]
  LayerMask rayLayerDetection;
  RaycastHit hit;
  [SerializeField]
  Transform reticleTrs;

  [SerializeField]
  Vector3 initialScale;

  bool objectTouched;

  VRControls vrcontrols;

  Target target;
  
  Camera m_camera;

  void Awake()
  {
    m_camera = GetComponent<Camera>();
    vrcontrols = new VRControls();
  }

  void OnEnable()
  {
    vrcontrols.Enable();
  }

  void OnDisable()
  {
    vrcontrols.Disable();
  }

  void Start()
  {
    if(IsLocalPlayer)
    {
      reticleTrs.localScale = initialScale;
      vrcontrols.Gameplay.VRClick.performed += _=> ClickOverObject();
    }
    else 
    {
      m_camera.enabled = false;
    }
      
  }

  void ClickOverObject()
  {
    Debug.Log(target?.gameObject.layer);
    switch(target?.gameObject.layer)
      {
        case 8:
          target?.handleClick();
          //target.HandleColor();
          break;
        case 9:
          Debug.Log("click");
          target?.HandleTextInteraction();
          break;
      }
  }

  void Update() 
  {
    #if UNITY_STANDALONE_WIN
    if(!IsLocalPlayer) return;
    transform.Translate(new Vector3(AxisDirection.x, 0f, AxisDirection.y) * Time.deltaTime * 3f);
    #endif
  }
  void FixedUpdate()
  {
    if(Physics.Raycast(transform.position, transform.forward, out hit, rayDistance, rayLayerDetection))
    {
      target = hit.collider.GetComponent<Target>();
      reticleTrs.position = hit.point;
      reticleTrs.localScale = initialScale * hit.distance;
      reticleTrs.localRotation = Quaternion.LookRotation(hit.normal);
    }
    else
    {
      //target ??= null;
      if(target) target = null;
      reticleTrs.localScale = initialScale;
      reticleTrs.localPosition = new Vector3(0, 0, 1);
      reticleTrs.localRotation = Quaternion.identity;
    }
  }

  void OnDrawGizmosSelected()
  {
    Gizmos.color = rayColor;
    Gizmos.DrawRay(transform.position, transform.forward * rayDistance);
  }

  public override void NetworkStart()
  {
    base.NetworkStart();
  } 

  Vector2 AxisDirection => vrcontrols.Gameplay.Movement.ReadValue<Vector2>();
}
