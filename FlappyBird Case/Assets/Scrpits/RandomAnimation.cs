using UnityEngine;

public class RandomAnimation : MonoBehaviour
{
    [Range(0, 100)]
    [SerializeField] private float[] randomChance;

    [SerializeField] private RuntimeAnimatorController[] animatiorControllers;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();

        int animatiorControllersLength = animatiorControllers.Length;

        for (int i = 0; i < animatiorControllersLength; i++)
        {
            float rng = Random.Range(0f, 100f);
            if (rng < randomChance[i])
            {
                anim.runtimeAnimatorController = animatiorControllers[i];

                break;
            }
        }
    }
}