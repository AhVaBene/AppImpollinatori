using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Game2Controller : MonoBehaviour
{
    public static bool isGameOver = true;
    public static bool cambioTurno = false;
    public static string sceltaString = "";
    Dictionary<int, string[]> proposte = new Dictionary<int, string[]>();
    Dictionary<int, string[]> risposteCorrette = new Dictionary<int, string[]>();
    static string[] colori = { "Colori non neutri: Blu, giallo, viola", "Spesso verde o bianco", "Brillante; spesso rosso", "Bianco o pallido", "Brunastro, violaceo", "Luminoso" };
    static string[] profumi = { "Fresco, intenso", "Vario, intenso", "Fresco, debole", "Dolce, intenso", "Sgradevole, intenso", "Debole" };
    static string[] periodi = { "Giorno", "Giorno o notte", "Notte o crepuscolo" };
    static string[] corolle = { "Piattaforma di atterraggio zigomorfa", "Racchiusa o aperta", "Piattaforma di atterraggio; talvolta con speroni nettariferi", 
        "Laciniata; talvolta con speroni nettariferi", "Attinomorfa, poco profonda" };
    static string[] ricompense = { "Nettare e/o polline", "Solo nettare", "Nessuna" };
    string[] turni = { "Colore", "Profumo", "Periodo di fioritura", "Corolla", "Ricompensa all'impollinatore", "" };

    int impollinatore;
    int turno;
    int scelte;

    public TextMeshProUGUI[] textButtons;
    public TextMeshProUGUI[] recaps;
    public TextMeshProUGUI turnoText;

    public GameObject TurnCanvas;
    public GameObject RecapCanvas;
    public GameObject GameOverCanvas;

    public TextMeshProUGUI score;
    public SpriteRenderer[] spriteRenderers;
    public Sprite[] spriteArray;
    Dictionary<int, int[]> fioriPerImpoll = new Dictionary<int, int[]>();
    static int[] fiori0 = { 0, 1, 2, 3, 7 }; //Bellis // Cardo // Cistus // Garofano // Papavero
    static int[] fiori1 = { 0, 1, 2, 3, 7 }; //Bellis // Cardo // Cistus // Garofano // Papavero
    static int[] fiori2 = { 0, 4, 6, 9, 11 }; //Bellis // Labiatae // Oxalis // Ranuncolo // Solanum
    static int[] fiori3 = { 0, 9, 11 }; //Bellis // // Ranuncolo // Solanum
    static int[] fiori4 = { 0, 2, 13, 14, 15 }; //Bellis // Cistus // Elleboro // Euphorbia // Narcissus
    static int[] fiori5 = { 8, 10, 12 }; //Primula // Rosa canina // Trifoglio
    static int[] fiori6 = { 6 }; //Oxalis
    static int[] fiori7 = { 5 }; //Orchidea
    public TextMeshProUGUI[] nomeFioriText;
    string[] nomeFiori = { "Bellis", "Cardo", "Cistus", "Garofano", "Labiatae", "Orchidea", "Oxalis", "Papavero", "Primula", "Ranuncolo", "Rosa Canina", "Solanum", "Trifoglio", "Elleboro", "Euphorbia", "Narcissus" };
    private void Start()
    {
        turno = 0;
        scelte = 0;
        fioriPerImpoll[0] = fiori0;
        fioriPerImpoll[1] = fiori1;
        fioriPerImpoll[2] = fiori2;
        fioriPerImpoll[3] = fiori3;
        fioriPerImpoll[4] = fiori4;
        fioriPerImpoll[5] = fiori5;
        fioriPerImpoll[6] = fiori6;
        fioriPerImpoll[7] = fiori7;
        proposte[0] = colori;
        proposte[1] = profumi;
        proposte[2] = periodi;
        proposte[3] = corolle;
        proposte[4] = ricompense;
        risposteCorrette[0] = new List<string>() { colori[2], profumi[2], periodi[0], corolle[2], ricompense[1] }.ToArray();
        risposteCorrette[1] = new List<string>() { colori[2], profumi[2], periodi[0], corolle[2], ricompense[1] }.ToArray();
        risposteCorrette[2] = new List<string>() { colori[0], profumi[0], periodi[0], corolle[0], ricompense[0] }.ToArray();
        risposteCorrette[3] = new List<string>() { colori[0], profumi[0], periodi[0], corolle[0], ricompense[0] }.ToArray();
        risposteCorrette[4] = new List<string>() { colori[1], profumi[1], periodi[1], corolle[1], ricompense[0] }.ToArray();
        risposteCorrette[5] = new List<string>() { colori[3], profumi[3], periodi[2], corolle[3], ricompense[1] }.ToArray();
        risposteCorrette[0] = new List<string>() { colori[5], profumi[5], periodi[0], corolle[4], ricompense[0] }.ToArray();
        risposteCorrette[0] = new List<string>() { colori[4], profumi[4], periodi[1], corolle[1], ricompense[2] }.ToArray();
    }

    void Update()
    {
        impollinatore = StartGame2.impollinatore;
        if (!isGameOver)
        {
            if(cambioTurno)
            {
                cambioTurno = false;
                //controllo la scelta fatta, cambio i button, aggiorno recap
                if (turno != 0)
                {
                    if (sceltaString == risposteCorrette[impollinatore][turno - 1])
                    {
                        scelte++;
                        Debug.Log("CORRETTO");
                    }
                }
                turnoText.text = turni[turno];
                turno++;
                if (turno == 6)
                {
                    OnGameOver();
                }
                else
                {
                    SetupScelte();//cambio i button delle risposte del turno scelto. Per 2,3,4,5 cambio il recap del turno appena fatto(-1)
                    SetupRecaps();
                }     
            }
        }
    }
    void SetupRecaps()
    {
        if (turno >= 2 && turno <= 5)
        {
            recaps[turno - 2].text = sceltaString;
        }
    }
    void SetupScelte()
    {
        int sceltaCorretta = Random.Range(0, 29) % 3;
        int secondaScelta = Random.Range(0, 29) % 3;
        string rispostaCorretta = risposteCorrette[impollinatore][turno - 1];
        Debug.Log(rispostaCorretta);
        string secondaString = "";
        string terzaString = "";
        while (secondaScelta == sceltaCorretta)
        {
            secondaScelta = Random.Range(0, 29) % 3;
        }
        int terzaScelta = 0;
        if (sceltaCorretta == 0 && secondaScelta == 1) terzaScelta = 2;
        if (sceltaCorretta == 1 && secondaScelta == 0) terzaScelta = 2;
        if (sceltaCorretta == 0 && secondaScelta == 2) terzaScelta = 1;
        if (sceltaCorretta == 2 && secondaScelta == 0) terzaScelta = 1;
        if (sceltaCorretta == 1 && secondaScelta == 2) terzaScelta = 0;
        if (sceltaCorretta == 2 && secondaScelta == 1) terzaScelta = 0;

        int length = proposte[turno - 1].Length;
        do
        {
            secondaString = proposte[turno - 1][Random.Range(0, 44) % length];
        } while (secondaString == rispostaCorretta);
        do
        {
            terzaString = proposte[turno - 1][Random.Range(0, 44) % length];
        } while (terzaString == rispostaCorretta || terzaString == secondaString);
        textButtons[sceltaCorretta].text = rispostaCorretta;
        textButtons[secondaScelta].text = secondaString;
        textButtons[terzaScelta].text = terzaString;
    }

    void OnGameOver()
    {
        isGameOver = true;
        TurnCanvas.SetActive(false);
        RecapCanvas.SetActive(false);
        GameOverCanvas.SetActive(true);
        score.text = scelte.ToString() + "/5 scelte corrette";
        int[] fiori;
        fioriPerImpoll.TryGetValue(impollinatore, out fiori);
        for(int i = 0; i< fiori.Length; i++)
        {
            spriteRenderers[i].sprite = spriteArray[fiori[i]];
            nomeFioriText[i].text = nomeFiori[fiori[i]];
        }
    }
}
