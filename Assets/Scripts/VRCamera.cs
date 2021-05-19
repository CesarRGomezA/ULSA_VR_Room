using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.NetworkVariable;
using MLAPI.Messaging;
using TMPro;

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
  UnityEngine.UI.Image loadingImage;
  [SerializeField]
  Vector3 initialScale;
  bool objectTouched;
  bool isCounting = false;
  float countdown = 0;
  VRControls vrcontrols;
  TargetButton target;
  Camera m_camera;

  public Player player;
  QuestionController question;
  public int id = 0;
  public bool win = false;
  void Awake()
  {
    m_camera = GetComponent<Camera>();
    vrcontrols = new VRControls();
    //playersCount = transform.Find("/PlayersCount").GetComponent<PlayersCount>();
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
      if(!IsServer)
      {
        if(IsOwner)
        {
          GetComponent<Rigidbody>().isKinematic = true;
        } 
      }
      else
      {
        GetComponent<Rigidbody>().isKinematic = true;
      }
    }
    else 
    {
      m_camera.enabled = false;
      GetComponent<AudioListener>().enabled = false;
    }
    GameManager.instance.AddPlayer(this);
  }

  void AddPlayer()
  {
    GameManager.instance.playersCount++;
  }

  public void StartGame()
  {
    if(IsServer) return;
    if(!IsOwner) return;
    if(id != 1)
      player = transform.Find("/Player2").GetComponent<Player>();
    else
      player = transform.Find("/Player1").GetComponent<Player>();
    player.VRPlayer = this;
    GameManager.instance.VRPlayer = this;
    player.StartPlayer();
    player.MoveToNextStep();
    GetComponent<Rigidbody>().isKinematic = false;
    Debug.Log("start game");
  }

  void ClickOverObject()
  {

  }

  void Update() 
  {
    #if UNITY_STANDALONE_WIN
    if(!IsLocalPlayer) return;
    transform.Translate(new Vector3(AxisDirection.x, 0f, AxisDirection.y) * Time.deltaTime * 3f);
    #endif

    //if(!GameManager.instance.gameStarted && GameManager.instance.playersCount.Value >= 2)
    /*if(!GameManager.instance.gameStarted && playersCount.count.Value >= 2)
    {
      if(!IsServer) player.MoveToNextStep();
      GameManager.instance.gameStarted = true;
    }*/

   
  }

  public IEnumerator Win(int id)
  {
    if(IsOwner)
    {
      yield return new WaitForSeconds(0.2f); 
      Transform message = transform.Find("Message");
      message.Find("Panel/Text").GetComponent<TMP_Text>().text = $"Player {id} Win";
      message.gameObject.SetActive(true);

      Time.timeScale = 0;
    }
    StartCoroutine(RestartGame());
  }

  IEnumerator RestartGame()
  {
    if(IsOwner)
    {
      yield return new WaitForSecondsRealtime(5f); 
      Transform message = transform.Find("Message");
      message.gameObject.SetActive(false);
      Time.timeScale = 1;
      if(!IsServer) player.StartPlayer();
    }
  }

  void FixedUpdate()
  {
    if(Physics.Raycast(transform.position, transform.forward, out hit, rayDistance, rayLayerDetection))
    {
      reticleTrs.position = hit.point;
      reticleTrs.localScale = initialScale * hit.distance;
      reticleTrs.Find("Reticle").GetComponent<SpriteRenderer>().color = Color.white;
      reticleTrs.Find("Reticle").GetComponent<Light>().range = 0.3f;
      reticleTrs.rotation = Quaternion.LookRotation(hit.normal);
      if(hit.transform.CompareTag("Button")){
        isCounting = true;
        target = hit.collider.GetComponent<TargetButton>();
        question = target.transform.parent.parent.GetComponent<QuestionController>();
        target.buttonImage.color = new Color(0.4f,0.4f,0.4f);
        loadingImage.fillAmount = 0;
      }else{
        isCounting = false;
        countdown = 0;
        if(target) target.buttonImage.color = Color.white;
        loadingImage.fillAmount = 0;
      } 
    }
    else
    {
      reticleTrs.localScale = initialScale;
      reticleTrs.localPosition = new Vector3(0, 0, 1);
      reticleTrs.localRotation = Quaternion.identity;
      reticleTrs.Find("Reticle").GetComponent<Light>().range = 0;

      isCounting = false;
      countdown = 0;
      if(target) target.buttonImage.color = Color.white;
      reticleTrs.Find("Reticle").GetComponent<SpriteRenderer>().color = new Color(0.7f, 0.7f, 0.7f);
      loadingImage.fillAmount = 0;
    }
    if(countdown >= 3) { 
      if(question) question.player = player;
      if(target) target.Action();
    }
    if(isCounting)
    {
      countdown += Time.deltaTime;
      loadingImage.fillAmount = countdown/3f;
      if(target)
      {
        float color = countdown/3f > 0.4f ? countdown/3f : 0.4f;
        target.buttonImage.color = new Color(color, color, color);
      }
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
