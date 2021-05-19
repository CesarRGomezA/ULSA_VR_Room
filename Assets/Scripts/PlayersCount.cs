using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI.NetworkVariable;
using MLAPI.Messaging;
using MLAPI;

public class PlayersCount : NetworkBehaviour
{
    public NetworkVariableInt count = new NetworkVariableInt(new NetworkVariableSettings {WritePermission = NetworkVariablePermission.Everyone}, 0);

}
