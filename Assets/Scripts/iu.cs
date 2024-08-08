using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boton : MonoBehaviour
{
    private int currentLevelIndex = 0;
    private List<string> levels = new List<string> { "Nivel_1", "Nivel_2", "Nivel_3", "Nivel_4" };

    void Start()
    {
        // Inicializar el índice del nivel actual basándonos en la escena activa
        if (levels.Contains(SceneManager.GetActiveScene().name))
        {
            currentLevelIndex = levels.IndexOf(SceneManager.GetActiveScene().name);
        }
    }

public GameObject pausaMenu;
    public void pausa()
    {
       pausaMenu.SetActive(true);
       Time.timeScale = 0; 
    }

    public void reanudar()
    {
       pausaMenu.SetActive(false);
       Time.timeScale = 1; 
    }
    
    // Función para salir del juego
    public void Exit()
    {
        Application.Quit();
    }

    // Función para cargar una escena específica
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // Función para cargar el siguiente nivel
    public void LoadNextLevel()
    {
        if (currentLevelIndex >= 0 && currentLevelIndex < levels.Count - 1)
        {
            currentLevelIndex++;
            SceneManager.LoadScene(levels[currentLevelIndex]);
        }
        else
        {
            Debug.Log("No hay más niveles para cargar. Volviendo al menú principal.");
            LoadScene("MenuPrincipal");
        }
    }

    // Función para cargar la pantalla de "Fin de Ronda"
    public void LoadEndRoundScreen()
    {
        SceneManager.LoadScene("FinRonda");
    }

    // Función para manejar la lógica de transición después de la pantalla de "Fin de Ronda"
    public void HandleEndRoundTransition()
    {
        // Actualizar el índice del nivel actual antes de decidir la siguiente acción
        currentLevelIndex = levels.IndexOf(SceneManager.GetActiveScene().name);

        if (currentLevelIndex < levels.Count - 1)
        {
            SceneManager.LoadScene(levels[currentLevelIndex + 1]);
        }
        else
        {
            Debug.Log("Juego completado. Volviendo al menú principal.");
            LoadScene("MenuPrincipal");
        }
    }
}
