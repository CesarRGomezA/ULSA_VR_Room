using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
  [SerializeField]
  Color catchColor = Color.cyan;
  [SerializeField]
  Question dialog;

  Renderer render;
  [SerializeField]
  MessageController textInteraction;

  [SerializeField]
  List<Object> dialogsItems;
  int index;

  void Awake()
  {
    render = GetComponent<Renderer>();
  }

  public void HandleColor() => render.material.color = catchColor;

  public void HandleTextInteraction() =>  textInteraction.gameObject?.SetActive(!textInteraction.gameObject.activeSelf);

  public void handleClick()
  {
    if(!textInteraction.gameObject.activeSelf)
    {
      textInteraction.gameObject.SetActive(true);
    }

    if(dialogsItems[index] as Question)
    {
      Question item = dialogsItems[index] as Question;
      textInteraction.Message = item.Message;
    }


    //textInteraction.Message = dialogsItems[index].Message;
    //Debug.Log(dialogsItems[index].);
    //index = index + 1 < dialogsItems.Count ? index + 1 : 0;
    if(!(index + 1 < dialogsItems.Count)) 
    {
      index = 0;
      HandleTextInteraction();
    } 
    else 
    {
      index++;
    }
  }

}
