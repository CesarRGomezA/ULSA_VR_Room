using UnityEngine;

[CreateAssetMenu(fileName="Alert Dialogue", menuName="Dialogs/Alert dialogue", order=2)]
public class AlertDialogueItem : DialogItem
{
  Color alertColor = Color.yellow;

  public Color AlertColor { get => alertColor;}
}