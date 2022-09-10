using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Algoritmo : MonoBehaviour
{
    public Color32[] colorTallo = new Color32[4];
    public Color32[] colorFlor = new Color32[3];

    public GameObject labelAltura;
    public GameObject labelColor;
    public GameObject labelTamanio;
    public PoolingManager pm;
    public Transform conjuntoFlores;

    private List<float> adaptacionFlor = new List<float>();
    private int parienteA;
    private int parienteB;
    private bool terminarAlgoritmo = false;
    public List<int[]> nuevosCromosomas = new List<int[]>();
    public Text generacion;
    public int numeroGeneracion = 0;

    void Start()
    {
        InvokeRepeating("HacerAlgoritmo", 2.0f, 1.0f);
    }

    public void HacerAlgoritmo(){

        if(terminarAlgoritmo){
            generacion.text = "Finalizado: " + numeroGeneracion;
            return;
        }

        Adaptacion();

        if(terminarAlgoritmo){
            generacion.text = "Finalizado: " + numeroGeneracion;
            return;
        }

        ChooseParents();
        Crossover();
        Reemplazo();
        pm.cromosomas.Clear();
        for (int i = 0; i < 9; i++)
        {
            pm.cromosomas.Add(nuevosCromosomas[i]);
        }
        adaptacionFlor.Clear();
        nuevosCromosomas.Clear();

        numeroGeneracion++;
        generacion.text = "Generación: " + numeroGeneracion;
    }

    public void Adaptacion(){

        string altura = labelAltura.GetComponent<Text>().text;
        string color = labelColor.GetComponent<Text>().text;
        string tamanio = labelTamanio.GetComponent<Text>().text;

        float heightCurrent = 0;
        float colorCurrent = 0;
        float sizeCurrent = 0;

        int valorTotal = 0;

        for (int n = 0; n < 9; n++){
            switch (altura){
                case "Alto":
                    heightCurrent = pm.cromosomas[n][0]/300;
                    if(pm.cromosomas[n][0] > 200 && pm.cromosomas[n][0] <= 300){
                        valorTotal++;
                    }
                    break;
                case "Medio":
                    heightCurrent = pm.cromosomas[n][0]/200;
                    if(pm.cromosomas[n][0] > 100 && pm.cromosomas[n][0] <= 200){
                        valorTotal++;
                    }
                    break;
                case "Bajo":
                    heightCurrent = pm.cromosomas[n][0]/100;
                    if(pm.cromosomas[n][0] <= 100){
                        valorTotal++;
                    }
                    break;
                default:
                    break;
            }

            if (heightCurrent > 1){
                heightCurrent = 1 / heightCurrent;
            }
            
            int colorRAA = 0;
            switch (color){
                case "Rojo":
                    colorRAA = 0;
                    if(pm.cromosomas[n][1] == 0){
                        valorTotal++;
                    }
                    break;
                case "Azul":
                    colorRAA = 1;
                    if(pm.cromosomas[n][1] == 1){
                        valorTotal++;
                    }
                    break;
                case "Amarillo":
                    colorRAA = 2;
                    if(pm.cromosomas[n][1] == 2){
                        valorTotal++;
                    }
                    break;
                default:
                    break;
            }
            if (colorRAA == pm.cromosomas[n][1]){
                colorCurrent = 1;
            }

            switch (tamanio){
                case "Grande":
                    sizeCurrent = pm.cromosomas[n][4]/80;
                    if(pm.cromosomas[n][4] > 60 && pm.cromosomas[n][4] <= 80){
                        valorTotal++;
                    }
                    break;
                case "Normal":
                    sizeCurrent = pm.cromosomas[n][4]/60;
                    if(pm.cromosomas[n][4] > 40 && pm.cromosomas[n][4] <= 60){
                        valorTotal++;
                    }
                    break;
                case "Pequeño":
                    sizeCurrent = pm.cromosomas[n][4]/40;
                    if(pm.cromosomas[n][4] <= 40){
                        valorTotal++;
                    }
                    break;
                default:
                    break;
            }

            if (sizeCurrent > 1){
                sizeCurrent = 1 / sizeCurrent;
            }

            if(valorTotal >= 27){
                terminarAlgoritmo = true;
            }
            adaptacionFlor.Add((heightCurrent+colorCurrent+sizeCurrent)/3);
        }
    }

    public void ChooseParents(){

        int iParentA = 0;
        int iParentB = 0;

        for (int n = 0; n < 9; n++) {
            if (adaptacionFlor[n] > adaptacionFlor[iParentA]){
                iParentA = n;
            }
        }
        parienteA = iParentA;

        for (int n = 0; n < 9; n++) {
            if ((adaptacionFlor[n] > adaptacionFlor[iParentB]) && iParentA != n) {
                iParentB = n;
            }
        }
        parienteB = iParentB;
    }

    public void Crossover(){

        int[] oldChromosomeA = pm.cromosomas[parienteA];
        int[] oldChromosomeB = pm.cromosomas[parienteB];

        //int from = Random.Range(0,6);
        //int to = Random.Range(from,7);

        for (int n = 0; n < 9; n++) {
            int[] newChromosome = pm.cromosomas[n];
            for (int a = 0; a < 6; a++) {
                if (Random.Range(0, 2) == 0) {
                    newChromosome[a] = oldChromosomeA[a];
                } else {
                    newChromosome[a] = oldChromosomeB[a];
                }
                if (Random.Range(0, 101) > 95) {
                    newChromosome[a] = getAlleleValue(a);
                }
            }
            nuevosCromosomas.Add(newChromosome);
        }
    }

    public int getAlleleValue(int a) {
        int[] values = new int[]{Random.Range(50,301), Random.Range(0,3), Random.Range(0,4), Random.Range(5,16), Random.Range(20,81), Random.Range(5,16)};

        return values[a];
    }

    public void Reemplazo(){
        int x = 0;
        foreach (Transform obj in conjuntoFlores)
        {
            obj.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().size = new Vector2(nuevosCromosomas[x][5], nuevosCromosomas[x][5]);
            obj.GetChild(0).GetComponent<SpriteRenderer>().size = new Vector2(nuevosCromosomas[x][4], nuevosCromosomas[x][4]);
            obj.GetChild(0).GetComponent<SpriteRenderer>().color = colorFlor[nuevosCromosomas[x][1]];
            obj.GetChild(1).GetComponent<SpriteRenderer>().size = new Vector2(nuevosCromosomas[x][3], nuevosCromosomas[x][0]);
            obj.GetChild(1).GetComponent<SpriteRenderer>().color = colorTallo[nuevosCromosomas[x][2]];

            x++;
        }
    }
}
