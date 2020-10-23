using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bola : MonoBehaviour
{
    public float velocidad = 30.0f;

    AudioSource fuenteDeAudio;
    public AudioClip audioGol, audioRaqueta, audioRebote, audioStart, audioFinish;


    public int golesIzquierda = 0;
    public int golesDerecha = 0;

    public Text contadorIzquierda;
    public Text contadorDerecha;
    public Text FinJuego;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.right * velocidad;

        fuenteDeAudio = GetComponent<AudioSource>();

        contadorIzquierda.text = golesIzquierda.ToString();
        contadorDerecha.text = golesDerecha.ToString();

        fuenteDeAudio.clip = audioStart;
        fuenteDeAudio.Play();
    }

    void OnCollisionEnter2D(Collision2D micolision)
    {

        if (micolision.gameObject.name == "RaquetaIzquierda")
        {
            int x = 1;
            int y = direccionY(transform.position, micolision.transform.position);

            Vector2 direccion = new Vector2(x, y);

            GetComponent<Rigidbody2D>().velocity = direccion * velocidad;

            fuenteDeAudio.clip = audioRaqueta;
            fuenteDeAudio.Play();

        }
        else if (micolision.gameObject.name == "RaquetaDerecha")
        {
            int x = -1;
            int y = direccionY(transform.position, micolision.transform.position);

            Vector2 direccion = new Vector2(x, y);

            GetComponent<Rigidbody2D>().velocity = direccion * velocidad;

            fuenteDeAudio.clip = audioRaqueta;
            fuenteDeAudio.Play();
        }

        if (micolision.gameObject.name == "Arriba" || micolision.gameObject.name == "Abajo")
        {
            fuenteDeAudio.clip = audioRebote;
            fuenteDeAudio.Play();
        }
    }

    int direccionY(Vector2 posicionBola, Vector2 posicionRaqueta)
    {
        if (posicionBola.y > posicionRaqueta.y)
        {
            return 1;
        }
        else if (posicionBola.y < posicionRaqueta.y)
        {
            return -1;
        }
        else
        {
            return 0;
        }
    }

    public void reiniciarBola(string direccion)
    {
        transform.position = Vector2.zero;

        //Que la velocidad de la bola sea en función del número de goles.
        velocidad += (golesDerecha + golesIzquierda);

        if (direccion == "Derecha")
        {
            golesDerecha++;
            contadorDerecha.text = golesDerecha.ToString();
            GetComponent<Rigidbody2D>().velocity = Vector2.right * velocidad;

            if (golesDerecha >= 5)
            {
                FinJuego.text = "¡VICTORIA A JUGADOR DERECHA!";

                fuenteDeAudio.clip = audioFinish;
                fuenteDeAudio.Play();

                transform.position = Vector2.zero;
                GetComponent<Rigidbody2D>().velocity = Vector2.left * 0;

                return;

            }
        }
        else if (direccion == "Izquierda")
        {
            golesIzquierda++;
            contadorIzquierda.text = golesIzquierda.ToString();
            GetComponent<Rigidbody2D>().velocity = Vector2.left * velocidad;

            if (golesIzquierda >= 5)
            {
                FinJuego.text = "¡VICTORIA A JUGADOR IZQUIERDA!";

                    fuenteDeAudio.clip = audioFinish;
                    fuenteDeAudio.Play();

                    transform.position = Vector2.zero;
                    GetComponent<Rigidbody2D>().velocity = Vector2.left * 0;

                    return;
            }
        }

        fuenteDeAudio.clip = audioGol;
        fuenteDeAudio.Play();

      
    }
}