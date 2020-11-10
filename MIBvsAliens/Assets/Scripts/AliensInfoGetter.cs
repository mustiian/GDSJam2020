using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AliensInfoGetter : MonoBehaviour
{
    private Dictionary<AlienType, CharacterInfo> _infos;

    private void Awake()
    {
        var charsInfo = GameManager.instance.charactersInfo.alienInfo;
        _infos = charsInfo.ToDictionary(info => info.alienType, info => info.characterInfo);
    }

    public CharacterInfo GetFor(AlienType alienType)
    {
        return _infos[alienType];
    }
}