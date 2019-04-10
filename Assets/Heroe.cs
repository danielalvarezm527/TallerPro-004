using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using zom = NPC.Enemy;
using villa = NPC.Ally;

/// <summary>
/// calculamos las distancias con respecto a las demas entidades
/// </summary>
public class Heroe : MonoBehaviour
{
    float distancia;
    float distanciaZ;
    public float time;
    public TextMeshProUGUI textoZombie;
    public TextMeshProUGUI textoAldeano;
    GameObject[] aldeanos, zombie;

    DatosVillager datosAldeano = new DatosVillager();
    DatosZombie datosZombie = new DatosZombie();

    /// <summary>
    /// asignamos datos basicos del heroe
    /// </summary>
    void Start()
    {
        Rigidbody hero = this.gameObject.AddComponent<Rigidbody>();
        this.gameObject.tag = "Hero";
        this.gameObject.name = "Hero";
        hero.constraints = RigidbodyConstraints.FreezeAll;
        hero.useGravity = false;
        StartCoroutine(BuscaEntidades());
        textoZombie = GameObject.FindGameObjectWithTag("TextZombie").GetComponent<TextMeshProUGUI>();
        textoAldeano = GameObject.FindGameObjectWithTag("TextAldeano").GetComponent<TextMeshProUGUI>();
    }

    /// <summary>
    /// contamos un tiempo
    /// </summary>
    public void Update()
    {
        time += Time.fixedDeltaTime;
    }

    /// <summary>
    /// calculamos la distancia de los objetos con respecto al heroe para hacer la retroalimentacon por medio del canvas 
    /// </summary>
    /// <returns></returns>
    IEnumerator BuscaEntidades()
    {
        zombie = GameObject.FindGameObjectsWithTag("Zombie");
        aldeanos = GameObject.FindGameObjectsWithTag("Villager");

        // retroalimentacion para el aldeano
        foreach (GameObject item in aldeanos)
        {
            yield return new WaitForEndOfFrame();
            villa.Villager componenteAldeano = item.GetComponent<villa.Villager>();
            if (componenteAldeano != null)
            {              
                distancia = Mathf.Sqrt(Mathf.Pow((item.transform.position.x - transform.position.x), 2) + Mathf.Pow((item.transform.position.y - transform.position.y), 2) + Mathf.Pow((item.transform.position.z - transform.position.z), 2));
                if (distancia < 5f)
                {
                    time = 0;
                    datosAldeano = item.GetComponent<villa.Villager>().datosAldeano;
                    textoAldeano.text = "Hola, soy " + datosAldeano.nombre + " y tengo " + datosAldeano.edad.ToString() + " años";
                }
                if (time > 3)
                {
                    textoAldeano.text = " ";
                }
            }
        }

        // retroalimentacion para el zombie
        foreach (GameObject itemZ in zombie)
        {
            yield return new WaitForEndOfFrame();
            zom.Zombie componenteZombie = itemZ.GetComponent<zom.Zombie>();
            if (componenteZombie != null)
            {              
                distanciaZ = Mathf.Sqrt(Mathf.Pow((itemZ.transform.position.x - transform.position.x), 2) + Mathf.Pow((itemZ.transform.position.y - transform.position.y), 2) + Mathf.Pow((itemZ.transform.position.z - transform.position.z), 2));
                if (distanciaZ < 5f)
                {
                    time = 0;
                    datosZombie = itemZ.GetComponent<zom.Zombie>().datosZombie;
                    textoZombie.text = "Waaaarrrr quiero comer " + datosZombie.gusto;
                }
                if (time > 3)
                {
                    textoZombie.text = " ";
                }
            }
        }
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(BuscaEntidades());
    }

    /// <summary>
    /// calse encargada de el movimiento del heroe
    /// </summary>
    public class MoverH : MonoBehaviour
    {

        Velocidad velocidad;

        private void Start()
        {
            velocidad  = new Velocidad(Random.Range(0.25f, 0.5f));
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.W))
            {
                this.gameObject.transform.Translate(0, 0, velocidad.velo);
            }
            if (Input.GetKey(KeyCode.S))
            {
                this.gameObject.transform.Translate(0, 0, -velocidad.velo);
            }
        }
    }

    /// <summary>
    /// calse encargada de la rotacion del heroe
    /// </summary>
    public class MirarH : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKey(KeyCode.A))
            {
                this.gameObject.transform.Rotate(0, -3, 0);
            }
            if (Input.GetKey(KeyCode.D))
            {
                this.gameObject.transform.Rotate(0, 3, 0);
            }
        }
    }
}

/// <summary>
/// calse para la velocidad read only del heroe
/// </summary>
public class Velocidad
{
    public readonly float velo;
    public Velocidad(float vel)
    {
        velo = vel;
    }
}
