using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    public void PlayAudio(string name)
    {
        FindObjectOfType<SFXPlayer>().PlayAudio(name);
    }
}
