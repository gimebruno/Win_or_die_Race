using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public CarData player1Car;
    public CarData player2Car;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // No destruir este objeto entre escenas
        }
        else
        {
            Destroy(gameObject);  // Solo debe haber una instancia
        }
    }

    public void SetSelectedCars(CarData player1, CarData player2)
    {
        player1Car = player1;
        player2Car = player2;
    }
}
