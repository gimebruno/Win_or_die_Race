using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;

    void Start()
    {
        // Obtener los autos seleccionados del GameManager
        CarData player1Car = GameManager.Instance.player1Car;
        CarData player2Car = GameManager.Instance.player2Car;

        // Asignar los autos a los jugadores
        if (player1 != null && player1Car != null)
        {
            player1.GetComponent<SpriteRenderer>().sprite = player1Car.carSprite;
        }

        if (player2 != null && player2Car != null)
        {
            player2.GetComponent<SpriteRenderer>().sprite = player2Car.carSprite;
        }
    }
}
