using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CarSelectionManager : MonoBehaviour
{
    public CarData[] availableCars;
    public Image player1CarImage;
    public Image player2CarImage;

    private int player1Index = 0;
    private int player2Index = 0;

    void Start()
    {
        UpdateCarSelection();
    }

    public void NextCarPlayer1()
    {
        player1Index = (player1Index + 1) % availableCars.Length;
        UpdateCarSelection();
    }

    public void PreviousCarPlayer1()
    {
        player1Index = (player1Index - 1 + availableCars.Length) % availableCars.Length;
        UpdateCarSelection();
    }

    public void NextCarPlayer2()
    {
        player2Index = (player2Index + 1) % availableCars.Length;
        UpdateCarSelection();
    }

    public void PreviousCarPlayer2()
    {
        player2Index = (player2Index - 1 + availableCars.Length) % availableCars.Length;
        UpdateCarSelection();
    }

    void UpdateCarSelection()
    {
        player1CarImage.sprite = availableCars[player1Index].carSprite;
        player2CarImage.sprite = availableCars[player2Index].carSprite;
    }

    public void StartGame()
    {
        // Asignar autos seleccionados al GameManager
        GameManager.Instance.SetSelectedCars(availableCars[player1Index], availableCars[player2Index]);

        // Cargar el nivel 1
        SceneManager.LoadScene("Nivel_1");
    }
}