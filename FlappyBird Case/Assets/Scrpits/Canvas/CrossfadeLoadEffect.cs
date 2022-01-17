using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrossfadeLoadEffect : MonoBehaviour
{
    private Animator crossfadeTransition;
    [SerializeField] private float tempoDeCrossfade = 1f;
    
    
    

    private void Start()
    {
        crossfadeTransition = GetComponent<Animator>();
        
    }

    

    public void ChamarCrossfade(string cena)
    {
        StartCoroutine(IniciarCrossfade(cena));
    }

    private IEnumerator IniciarCrossfade(string cena)
    {
        crossfadeTransition.SetTrigger("Start");
        yield return new WaitForSeconds(tempoDeCrossfade);

        //if(FindObjectOfType<SFXPlayer>() != null)
        //FindObjectOfType<SFXPlayer>().StopAll();

        
            

        SceneManager.LoadScene(cena);

        crossfadeTransition.SetTrigger("End");
    }
}