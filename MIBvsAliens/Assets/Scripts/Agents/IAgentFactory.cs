using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAgentFactory
{
    BaseAgent Create(Vector3 position);
}
