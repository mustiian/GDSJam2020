using UnityEngine;

public class AliensInfoGetter : MonoBehaviour
{
    public CharacterInfo GetFor(AlienType alienType)
    {
        return new CharacterInfo()
        {
            damage = 3,
            health = 20,
            speed = 2
        };
    }
}