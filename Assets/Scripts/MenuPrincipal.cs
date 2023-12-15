using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private GameObject painelMenuInicial;
    [SerializeField] private GameObject painelControls;

    public void Play(){
        SceneManager.LoadScene("MainScene");
    }

    public void OpenControls(){
        painelMenuInicial.SetActive(false);
        painelControls.SetActive(true);
    }

    public void CloseControls(){
        painelControls.SetActive(false);
        painelMenuInicial.SetActive(true);
    }

    public void Exit(){
        Debug.Log("Sair do Jogo");
        Application.Quit();
    }
}
