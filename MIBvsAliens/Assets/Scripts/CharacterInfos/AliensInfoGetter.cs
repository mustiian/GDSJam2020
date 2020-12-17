using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AliensInfoGetter : MonoBehaviour
{
    private Dictionary<AlienType, CharacterInfo> _infos;

    private void Start()
    {
        var charsInfo = GameManager.instance.charactersInfo.alienInfo;
        _infos = charsInfo.ToDictionary(info => info.alienType, info => info.characterInfo);
    }

    public CharacterInfo GetFor(AlienType alienType)
    {
        if (_infos == null)
            GetInfo();

        return _infos[alienType];
    }

    private void GetInfo()
    {
        var charsInfo = GameManager.instance.charactersInfo.alienInfo;
        _infos = charsInfo.ToDictionary(info => info.alienType, info => info.characterInfo);
    }
}