using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GlossarioController : MonoBehaviour
{
    public TextMeshProUGUI TitoloImp;
    public TextMeshProUGUI colore;
    public TextMeshProUGUI profumo;
    public TextMeshProUGUI periodo;
    public TextMeshProUGUI corolla;
    public TextMeshProUGUI ricompensa;

    private Impollinatore[] impollinatori = {
        new Impollinatore("Ape", "Colori non neutri: Blu, giallo, viola", "Fresco, intenso", "Giorno", "Piattaforma di atterraggio zigomorfa", "Nettare e/o polline"),
        new Impollinatore("Coleottero", "Spesso verde o bianco", "Vario, intenso", "Giorno o notte", "Racchiusa o aperta", "Nettare e/o polline"),
        new Impollinatore("Farfalla diurna", "Brillante; spesso rosso", "Fresco, debole", "Giorno", "Piattaforma di atterraggio; talvolta con speroni nettariferi", "Solo nettare"),
        new Impollinatore("Farfalla notturna", "Bianco o pallido", "Dolce, intenso", "Notte o crepuscolo", "Laciniata; talvolta con speroni nettariferi", "Solo nettare"),
        new Impollinatore("Mosca (Ricompensa)", "Luminoso", "Debole", "Giorno", "Attinomorfa, poco profonda", "Nettare e/o polline"),
        new Impollinatore("Mosca (Inganno)", "Brunastro, violaceo", "Sgradevole, intenso", "Giorno o notte", "Racchiusa o aperta", "Nessuna")
    };
    Dictionary<int, int[]> fioriPerImpoll = new Dictionary<int, int[]>();
    static int[] fiori0 = { 0, 1, 2, 3, 7 }; //Bellis // Cardo // Cistus // Garofano // Papavero (farf)
    static int[] fiori2 = { 0, 4, 6, 9, 11 }; //Bellis // Labiatae // Oxalis // Ranuncolo // Solanum (ape)
    static int[] fiori4 = { 0, 2, 13, 14, 15 }; //Bellis // Cistus // Elleboro // Euphorbia // Narcissus (col)
    static int[] fiori5 = { 8, 10, 12 }; //Primula // Rosa canina // Trifoglio (farfN)
    static int[] fiori6 = { 6 }; //Oxalis (moscaR)
    static int[] fiori7 = { 5 }; //Orchidea (moscaI)
    public SpriteRenderer[] spriteRenderersImp;
    public SpriteRenderer[] spriteRenderersFiori;
    public Sprite[] spriteArrayImp;
    public Sprite[] spriteArrayFiori;
    public TextMeshProUGUI[] NomeImp;
    public TextMeshProUGUI[] NomeFiori;
    string[] nomeFiori = { "Bellis", "Cardo", "Cistus", "Garofano", "Labiatae", "Orchidea", "Oxalis", "Papavero", "Primula", "Ranuncolo", "Rosa Canina", "Solanum", "Trifoglio", "Elleboro", "Euphorbia", "Narcissus" };
    string[] nomeImp = { "Aphis melliphera", "Bombus terrestris", "Cetornia aurata", "Oxythyrea funesta", "Aglais urticae", "Zerynthia", "Autographa gamma", "Syrphus ribesii", "Bombylella atra" };

    public static int indice = 0; //0...5


    public TextMeshProUGUI TitoloInfo;
    public TextMeshProUGUI SottotTitoloInfo1;
    public TextMeshProUGUI SottotTitoloInfo2;
    public TextMeshProUGUI infoText1;
    public TextMeshProUGUI infoText2;

    public SpriteRenderer[] spriteCorolla;
    public SpriteRenderer[] spriteBottom;
    public Sprite[] spriteArrayInfo;
    public GameObject scaleSprite;

    public static int indiceInfo = 0; //0...3

    private Info[] informazioni = {
        new Info("Mosca impollinatrice", "Mosca (Inganno)", "",
            "Alcune orchidee hanno fiori violacei che, per il loro odore e per il loro colore, simulano la carne in putrefazione. Questi fiori attraggono le mosche che vi depongono le uova, dal momento che l'odore e il colore le inducono a considerarli come una fonte di cibo per le larve. Durante il processo di deposizione delle uova, la mosca si muove all'interno del fiore e lo impollina senza in realtà ricevere alcuna ricompensa",
            ""),
        new Info("Forma corolla", "Zigomorfa", "Attinomorfa",
            "In botanica, si definisce zigomorfo un corpo che si può dividere in due metà specularmente uguali solamente con un piano di simmetria",
            "In biologia si definisce actinomorfo o attinomorfo un corpo che presenta organi con diversi piani di simmetria raggiata"),
        new Info("I danni degli impollinatori", "Oxythyrea funesta e Peonia", "",
            "L'oxythyrea funesta è un coleottero fitofago che si nutre di polline e nettare ma che può anche rodere gli organi floreali risultando dannoso per l'agricoltura. Fiori ricchi di polline facilmente accessibile, come margherite o girasoli, non subiscono danno evidente mentre i danni principali si osservano in fiori come quelli delle peonie", "") 
    };

    // Start is called before the first frame update
    void Start()
    {
        fioriPerImpoll[2] = fiori0;

        fioriPerImpoll[0] = fiori2;

        fioriPerImpoll[1] = fiori4;
        fioriPerImpoll[3] = fiori5;
        fioriPerImpoll[4] = fiori6;
        fioriPerImpoll[5] = fiori7;
    }

    // Update is called once per frame
    void Update()
    {
        switch(StartGlossarioScript.paginaScelta)
        {
            case 0://Impollinatori
                PopolaImpollinatori();
                break;
            case 1://Info
                PopolaInfo();
                break;
            default: break;
        }
    }
    void PopolaImpollinatori()
    {
        TitoloImp.text = impollinatori[indice].Nome;
        colore.text = impollinatori[indice].Colore;
        profumo.text = impollinatori[indice].Profumo;
        periodo.text = impollinatori[indice].Periodo;
        corolla.text = impollinatori[indice].Corolla;
        ricompensa.text = impollinatori[indice].Ricompensa;

        switch (indice)
        {
            case 0: //ape
                spriteRenderersImp[0].sprite = spriteArrayImp[0];
                spriteRenderersImp[1].sprite = spriteArrayImp[1];
                NomeImp[0].text = nomeImp[0];
                NomeImp[1].text = nomeImp[1];
                break;
            case 1: //col
                spriteRenderersImp[0].sprite = spriteArrayImp[2];
                spriteRenderersImp[1].sprite = spriteArrayImp[3];
                NomeImp[0].text = nomeImp[2];
                NomeImp[1].text = nomeImp[3];
                break;
            case 2: //farf
                spriteRenderersImp[0].sprite = spriteArrayImp[4];
                spriteRenderersImp[1].sprite = spriteArrayImp[5];
                NomeImp[0].text = nomeImp[4];
                NomeImp[1].text = nomeImp[5];
                break;
            case 3: //farfN
                spriteRenderersImp[0].sprite = spriteArrayImp[6];
                NomeImp[0].text = nomeImp[6];
                break;
            case 4: //moscaR
                spriteRenderersImp[0].sprite = spriteArrayImp[7];
                NomeImp[0].text = nomeImp[7];
                break;
            case 5: //moscaI
                spriteRenderersImp[0].sprite = spriteArrayImp[8];
                NomeImp[0].text = nomeImp[8];
                break;
            default: break;
        }
        int[] fiori;
        fioriPerImpoll.TryGetValue(indice, out fiori);
        for (int i = 0; i < fiori.Length; i++)
        {
            spriteRenderersFiori[i].sprite = spriteArrayFiori[fiori[i]];
            NomeFiori[i].text = nomeFiori[fiori[i]];
        }
    }

    void PopolaInfo()
    {
        TitoloInfo.text = informazioni[indiceInfo].InfoTit;
        SottotTitoloInfo1.text = informazioni[indiceInfo].SottoTit1;
        infoText1.text = informazioni[indiceInfo].Info1;
        foreach (SpriteRenderer sprite in spriteCorolla)
        {
            sprite.enabled = false;
        }
        foreach (SpriteRenderer sprite in spriteBottom)
        {
            sprite.enabled = true;
        }
        spriteBottom[1].sprite = spriteArrayInfo[indiceInfo + 1];
        spriteBottom[0].sprite = spriteArrayInfo[indiceInfo];
        if (indiceInfo == 1)
        {
            SottotTitoloInfo2.text = informazioni[indiceInfo].SottoTit2;
            infoText2.text = informazioni[indiceInfo].Info2;
            foreach(SpriteRenderer sprite in spriteCorolla)
            {
                sprite.enabled = true;
            }
            foreach (SpriteRenderer sprite in spriteBottom)
            {
                sprite.enabled = false;
            }
        }
        scaleSprite.transform.localScale = indiceInfo == 2 ? new Vector3(15, 15, 0) : new Vector3(40, 40, 0);
    }
    private class Impollinatore
    {
        private string nome;
        private string colore;
        private string profumo;
        private string periodo;
        private string corolla;
        private string ricompensa;

        public Impollinatore(string nome, string colore, string profumo, string periodo, string corolla, string ricompensa)
        {
            this.nome = nome;
            this.colore = colore;
            this.profumo = profumo;
            this.periodo = periodo;
            this.corolla = corolla;
            this.ricompensa = ricompensa;
        }

        public string Nome
        {
            get { return nome; }
        }
        public string Colore
        {
            get { return colore; }
        }
        public string Profumo
        {
            get { return profumo; }
        }
        public string Periodo
        {
            get { return periodo; }
        }
        public string Corolla
        {
            get { return corolla; }
        }
        public string Ricompensa
        {
            get { return ricompensa; }
        }
    }

    private class Info
    {
        string infoTit;
        string sottoTit1;
        string sottoTit2;
        string info1;
        string info2;

        public Info(string infoTit, string sottoTit1, string sottoTit2, string info1, string info2)
        {
            this.infoTit = infoTit;
            this.sottoTit1 = sottoTit1;
            this.sottoTit2 = sottoTit2;
            this.info1 = info1;
            this.info2 = info2;
        }
        public string InfoTit
        {
            get { return infoTit; }
        }
        public string SottoTit1
        {
            get { return sottoTit1; }
        }
        public string SottoTit2
        {
            get { return sottoTit2; }
        }
        public string Info1
        {
            get { return info1; }
        }
        public string Info2
        {
            get { return info2; }
        }
    }
}
