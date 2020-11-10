using System;
using UnityEngine;

[Serializable]
public struct AgentTypeInfo
{
    public AgentType agentType;
    public CharacterInfo characterInfo;
}

[Serializable]
public struct AlienTypeInfo
{
    public AlienType alienType;
    public CharacterInfo characterInfo;
}

[Serializable]
public struct CharacterInfo
{
    public int health;
    public int damage;
    public int speed;
    public int cost;
}

public class CharactersInfo : MonoBehaviour
{
    public AgentTypeInfo[] agentInfo;
    public AlienTypeInfo[] alienInfo;
}
