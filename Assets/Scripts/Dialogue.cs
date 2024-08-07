using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public int nodeId { get; set; }
    public string name { get; set; }
    public string text { get; set; }
    public int? nextNodeId { get; set; }

}
