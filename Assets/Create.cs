using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using zom = NPC.Enemy;
using villa = NPC.Ally;

public class Create : MonoBehaviour
{
    public TextMeshProUGUI numeroZombies;
    public TextMeshProUGUI numeroAldeanos;
    public int numZombies;
    public int numAldeanos;
    public GameObject[] Zom,Villa;
    void Start()
    {
        new CrearInstancias();

    }


    /// <summary>
    /// retroalimentacion del canvas para saber cuantos aldeanos y zombies hay en la scena
    /// </summary>
    private void Update()
    {
        Zom = GameObject.FindGameObjectsWithTag("Zombie");
        Villa = GameObject.FindGameObjectsWithTag("Villager");
        foreach (GameObject item in Zom)
        {
            numZombies = Zom.Length;
        }
        foreach (GameObject item in Villa)
        {
            numAldeanos = Villa.Length;
        }

        if(Villa.Length == 0)
        {
            numeroAldeanos.text = 0.ToString();
        }
        else
        {
            numeroAldeanos.text = numAldeanos.ToString();
        }

        numeroZombies.text = numZombies.ToString();
    }
}
/// <summary>
/// Creamos una cantidad al azar de instancias al azar y le agregamos un componente
/// </summary>
 class CrearInstancias 
{
    public GameObject cube;
    public readonly int minInstancias = Random.Range(5, 16);
    int selector = 0;
    const int MAX = 26;
    public CrearInstancias()
    {
        for (int i = 0; i < Random.Range(minInstancias,MAX); i++)
        {
            
            if (selector == 0)
            {
                cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.AddComponent<Camera>();
                cube.AddComponent<Heroe>();
                cube.AddComponent<Heroe.MirarH>();
                cube.AddComponent<Heroe.MoverH>();
                cube.transform.position = new Vector3(Random.Range(-20, 21), 0, Random.Range(-20, 21));
                selector += 1;
            }

            int selec = Random.Range(selector, 3);

            if (selec == 1)
            {
                cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.AddComponent<villa.Villager>();
                cube.transform.position = new Vector3(Random.Range(-20, 21), 0, Random.Range(-20, 21));
            }
            if (selec == 2)
            {
                cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.AddComponent<zom.Zombie>();
                cube.transform.position = new Vector3(Random.Range(-20, 21), 0, Random.Range(-20, 21));
            }
        }
    }
}