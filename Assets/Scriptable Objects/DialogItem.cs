using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="Dialog Item", menuName="Dialogs/Empty dialog", order=1)]
public class DialogItem : ScriptableObject
{
    [SerializeField, TextArea(5, 20)]
    string message;

    public string Message {get => message; set => message = value;}
}
