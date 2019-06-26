using UnityEngine;

public class HallOfFameHandler : MonoBehaviour
{
    public CharacterStore characterStore;
    public Gallery gallery;

    void Start()
    {
        characterStore.GetCharacters();
    }
}
