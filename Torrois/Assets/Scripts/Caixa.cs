using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class Caixa : MonoBehaviour
{
    public bool ativado;
    private float velocidade = 6f;
    public Transform pontoMov;
    public int gridCaixa;
    public int gridDoJogador;
    public int gridAnteriorDoJogador;

    public bool colidiuJogador;
    public bool colidiuCaixa;
    public bool podeMover;
    public bool andaMax;
    public bool andandoComoRainha;
    public bool estaNoCicloCaixa;

    //0 = esquerda, 1 = direita, 2 = cima, 3 = baixo
    [SerializeField] public List<bool> direcoesMov = new List<bool>(){false, false, false, false};
    [SerializeField] public List<bool> podeDirecao = new List<bool>() { true, true, true, true };
    [SerializeField] public List<bool> direcoesMovCxColl = new List<bool>() { false, false, false, false };

    private SpriteRenderer sprite;
    private BoxCollider2D boxTriggerer;

    public Transform CircleCollHolder = null;
    private CircleCollider2D circleColl = null;

    ChecarMobilidade pontoMovScript;

    public Transform PontoColidiuComigo;

    [SerializeField]
    public List<GameObject> alavancaLista = new List<GameObject>() { };

    [SerializeField]
    public List<GameObject> botaoLista = new List<GameObject>() { };

    public GameObject portaSprite;

    FMOD.Studio.EventInstance porta;
    private bool playPorta;


    //Variaveis testes especiais para colisao da rainha
    public bool rainhaProcurandoColl;

    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        boxTriggerer = GetComponent<BoxCollider2D>();
        if (gameObject.tag != "Imovel")
            pontoMov = transform.GetChild(0);
        else
        {
            podeMover = false;
            //Destroy(pontoMov.gameObject);
        }
        if (pontoMov != null)
        {
            pontoMovScript = gameObject.GetComponentInChildren<ChecarMobilidade>();
            sendParent();
            pontoMov.parent = GameObject.FindGameObjectWithTag("HolderTemporario").transform;
        }    
        if (gameObject.tag == "Torre")
        {
            andaMax = false;
            for (int i = 0; i < podeDirecao.Count; i++)
            {
                podeDirecao[i] = true;
            }
        }
        if (gameObject.tag == "Rainha")
        {
            andaMax = true;
            circleColl = CircleCollHolder.GetComponent<CircleCollider2D>();
            for (int i = 0; i < podeDirecao.Count; i++)
            {
                podeDirecao[i] = true;
            }
        }
    }


    void FixedUpdate()
    {

        if (alavancaLista.Count !=0)
            verPorta("alavanca");

        if (botaoLista.Count !=0)
            verPorta("botao");

        if (pontoMov != null)
        {
            if (PontoColidiuComigo != null && pontoMovScript.ColidiuParede == true)
            {
                //Debug.Log("Centesimo debug " + pontoMovScript.ColidiuParede);
                if (gameObject.tag == "Rainha")
                {
                    ProcurarPontoColidiu(transform.position, circleColl.radius, PontoColidiuComigo);
                    PontoColidiuComigo.GetComponent<ChecarMobilidade>().ColidiuParede = true;
                }
                else
                    PontoColidiuComigo.GetComponent<ChecarMobilidade>().ColidiuParede = true;
            }
            if (gameObject.tag != "Rainha")
            {
                gridAnteriorDoJogador = playerMoveGrid.gridAnterior;
                gridDoJogador = playerMoveGrid.gridAtual;
            }
            else
            {
                if (direcoesMov[0] != true && direcoesMov[1] != true && direcoesMov[2] != true && direcoesMov[3] != true)
                {
                    //Debug.Log(direcoesMov);
                    gridAnteriorDoJogador = playerMoveGrid.gridAnterior;
                    gridDoJogador = playerMoveGrid.gridAtual;
                }
            }
            if ((pontoMovScript.ColidiuParede && gameObject.tag != "Imovel") && !colidiuCaixa)
            {
                //Debug.Log("Caixa pode voltar");
                Voltar();
                pontoMovScript.ColidiuParede = false;
            }
            else
            {
                if (!gameObject.GetComponent<Rewinder>().isRewinding)
                    Move();
            }

        }
        
    }


    private void Move()
    {

        transform.position = Vector2.MoveTowards(transform.position, pontoMov.position, velocidade * Time.deltaTime);
        if ((colidiuJogador || andandoComoRainha || colidiuCaixa))
        {
            if (podeMover == true)
            {
                if (colidiuJogador)
                {
                    if (andaMax == false)
                    {
                        if (gridAnteriorDoJogador == gridDoJogador + 1) //Veio da direita
                        {
                            if (podeDirecao[0]) //Pode ser falso no peão
                            {
                                direcoesMov[0] = true;
                                pontoMov.position += new Vector3(-1f, 0f, 0f); //Caixa pra esquerda
                            }
                            else
                            {
                                Voltar();
                            }
                        }
                        else if (gridAnteriorDoJogador == gridDoJogador - 1) //Veio da esquerda
                        {
                            if (podeDirecao[1]) //Pode ser falso no peão
                            {
                                direcoesMov[1] = true;
                                pontoMov.position += new Vector3(+1f, 0f, 0f); //Caixa pra direita
                             
                            }
                            else
                            {
                                Voltar();
                            }
                        }
                        if (gridAnteriorDoJogador == gridDoJogador - 16) //Veio de cima
                        {
                            if (podeDirecao[3]) //Pode ser falso no peão
                            {
                                direcoesMov[3] = true;
                                pontoMov.position += new Vector3(0f, -1f, 0f); //Caixa pra baixo
                            }
                            else
                            {
                                Voltar();
                            }

                        }
                        else if (gridAnteriorDoJogador == gridDoJogador + 16) //Veio de baixo
                        {
                            if (podeDirecao[2]) //Pode ser falso no peão
                            {
                                direcoesMov[2] = true;
                                pontoMov.position += new Vector3(0f, +1f, 0f); //Caixa pra cima
                            }
                            else
                            {
                                Voltar();
                            }
                            
                        }
                    }
                    if (andaMax == true && Vector2.Distance(transform.position, pontoMov.position) <= 0.1f)
                    {
                        andandoComoRainha = true;
                        if (gridAnteriorDoJogador == gridDoJogador + 1) //Veio da direita
                        {
                            if (!pontoMovScript.ColidiuParede)
                                pontoMov.position += new Vector3(-1f, 0f, 0f); //Caixa pra esquerda
                            direcoesMov[0] = true;
                        }
                        else if (gridAnteriorDoJogador == gridDoJogador - 1) //Veio da esquerda
                        {
                            if (!pontoMovScript.ColidiuParede)
                                pontoMov.position += new Vector3(+1f, 0f, 0f); //Caixa pra direita
                            direcoesMov[1] = true;
                        }
                        if (gridAnteriorDoJogador == gridDoJogador - 16) //Veio de cima
                        {
                            if (!pontoMovScript.ColidiuParede)
                                pontoMov.position += new Vector3(0f, -1f, 0f); //Caixa pra baixo
                            direcoesMov[3] = true;
                        }
                        else if (gridAnteriorDoJogador == gridDoJogador + 16) //Veio de baixo
                        {
                            if (!pontoMovScript.ColidiuParede)
                                pontoMov.position += new Vector3(0f, +1f, 0f); //Caixa pra cima
                            direcoesMov[2] = true;
                        }
                        if (pontoMovScript.ColidiuParede)
                            andandoComoRainha = false;
                    }
                }
                if (colidiuCaixa) //SE FALHAR, TESTAR ELSE IF
                {
                    Debug.Log("Chegou na colisão");
                    if (estaNoCicloCaixa)
                    {
                        colidiuCaixa = false;
                        estaNoCicloCaixa = false;
                    }
                    if (andaMax == false)
                    {
                        if (direcoesMovCxColl[0] == true && podeDirecao[0]) //Veio da direita
                        {
                            direcoesMov[0] = true;
                            pontoMov.position += new Vector3(-1f, 0f, 0f); //Caixa pra esquerda
                        }
                        else if (direcoesMovCxColl[0] == true && podeDirecao[0] != true)
                        {
                            if (gameObject.tag == "Peon")
                            {
                                PontoColidiuComigo.GetComponent<ChecarMobilidade>().ColidiuParede = true;
                                PontoColidiuComigo = null;
                            }
                        }

                        if (direcoesMovCxColl[1] == true && podeDirecao[1]) //Veio da esquerda
                        {
                            direcoesMov[1] = true;
                            pontoMov.position += new Vector3(+1f, 0f, 0f); //Caixa pra direita
                        }
                        else if (direcoesMovCxColl[1] == true && podeDirecao[1] != true)
                        {
                            if (gameObject.tag == "Peon")
                            {
                                PontoColidiuComigo.GetComponent<ChecarMobilidade>().ColidiuParede = true;
                                PontoColidiuComigo = null;
                            }
                        }
                        if (direcoesMovCxColl[3] == true && podeDirecao[3]) //Veio de cima
                        {
                            direcoesMov[3] = true;
                            pontoMov.position += new Vector3(0f, -1f, 0f); //Caixa pra baixo
                        }
                        else if (direcoesMovCxColl[3] == true &&  podeDirecao[3] != true)
                        {
                            if (gameObject.tag == "Peon")
                            {
                                PontoColidiuComigo.GetComponent<ChecarMobilidade>().ColidiuParede = true;
                                PontoColidiuComigo = null;
                            }
                        }
                        if (direcoesMovCxColl[2] == true && podeDirecao[2]) //Veio de baixo
                        {
                            direcoesMov[2] = true;
                            pontoMov.position += new Vector3(0f, +1f, 0f); //Caixa pra cima
                        }
                        else if (direcoesMovCxColl[2] == true && podeDirecao[2] != true)
                        {
                            if (gameObject.tag == "Peon")
                            {
                                PontoColidiuComigo.GetComponent<ChecarMobilidade>().ColidiuParede = true;
                                PontoColidiuComigo = null;
                            }
                        }
                    }
                    if (andaMax == true && Vector2.Distance(transform.position, pontoMov.position) <= 0.7f) //TODO: PROVAVELMENTE NAO FUNCIONA! PRECISA CONFERIR! 
                    {
                        andandoComoRainha = true;
                        if (direcoesMovCxColl[0] == true) //Veio da direita
                        {
                            if (!pontoMovScript.ColidiuParede)
                                pontoMov.position += new Vector3(-1f, 0f, 0f); //Caixa pra esquerda
                            direcoesMov[0] = true;
                        }
                        else if (direcoesMovCxColl[1] == true) //Veio da esquerda
                        {
                            if (!pontoMovScript.ColidiuParede)
                                pontoMov.position += new Vector3(+1f, 0f, 0f); //Caixa pra direita
                            direcoesMov[1] = true;
                        }
                        if (direcoesMovCxColl[3] == true) //Veio de cima
                        {
                            if (!pontoMovScript.ColidiuParede)
                                pontoMov.position += new Vector3(0f, -1f, 0f); //Caixa pra baixo
                            direcoesMov[3] = true;
                        }
                        else if (direcoesMovCxColl[2] == true) //Veio de baixo
                        {
                            if (!pontoMovScript.ColidiuParede)
                                pontoMov.position += new Vector3(0f, +1f, 0f); //Caixa pra cima
                            direcoesMov[2] = true;
                        }
                        if (pontoMovScript.ColidiuParede)
                            andandoComoRainha = false;
                    }
                    
                }
            }
            
            if (!andandoComoRainha)
                colidiuJogador = false;
            if (!andandoComoRainha)
                colidiuCaixa = false;
        }
        if (Vector2.Distance(transform.position, pontoMov.position) == 0f) //PODE BUGAR SE COLISAO FOR MUITO RAPIDO
        {
            if (gameObject.tag != "Peon")
                PontoColidiuComigo = null;
            //Debug.Log("Direcoes iguais");
            direcoesMov[0] = false;
            direcoesMov[1] = false;
            direcoesMov[2] = false;
            direcoesMov[3] = false; 
        }
    }
    public void Voltar()
    {
        //Debug.Log("tentando voltar");
        if (direcoesMov[0] == true)
        {
            //Debug.Log("tentando voltar " + "esquerda");
            pontoMov.position += new Vector3(+1f, 0f, 0f);
        }
        if (direcoesMov[1] == true)
        {
            //Debug.Log("tentando voltar " + "direita");
            pontoMov.position += new Vector3(-1f, 0f, 0f);
        }
        if (direcoesMov[3] == true)
        {
            //Debug.Log("tentando voltar " + "cima");
            pontoMov.position += new Vector3(0f, +1f, 0f);
        }
        if (direcoesMov[2] == true)
        {
            //Debug.Log("tentando voltar " + "baixo");
            pontoMov.position += new Vector3(0f, -1f, 0f);
        }
        if (Vector3.Distance(playerMoveGrid.pontoMov.position, pontoMov.position) == 0)
        {
            playerMoveGrid.voltando = true;
        }
        if (gameObject.tag == "Rainha")
            colidiuJogador = false;
        andandoComoRainha = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "GridTile")
        {
            GridIndice ThisIndice = collision.GetComponent<GridIndice>();
            gridCaixa = ThisIndice.thisIndice;
        }

        if (collision.gameObject.tag == "Player")
        {
            //Debug.Log("Esta colidindo com a caixa");
            colidiuJogador = true;
        }
        //if (collision.gameObject.tag == "Torre" || collision.gameObject.tag == "Rainha" || collision.gameObject.tag == "Peon")
        //{
        //    Debug.Log("Caixa entrando na caixa");
        //    pontoMovScript.ColidiuParede = true;
        //}
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        //if (collision.gameObject.tag == "Player") //TENTANDO IMPEDIR BUG DO JOGADOR ENTRANDO RAPIDAMENTE NA CAIXA COLIDINDO COM OUTRA
        //{
        //    Debug.Log("Prevenindo colisão continua jogador e caixa");
        //    playerMoveGrid.voltando = true;
        //}
    }

    public void verPorta(string texto)
    {

        if (texto=="botao")
        {
            boxTriggerer.isTrigger = false;
            foreach (GameObject button in botaoLista)
            {

                if (!button.GetComponent<Botao>().ativado)
                    boxTriggerer.isTrigger = true;
            }
            if (portaSprite == null)
                sprite.enabled = boxTriggerer.isTrigger;
            else
            {
                if (!boxTriggerer.enabled)
                    AnimPorta(2.9f);
                else
                    AnimPorta(0.84f);
            }
            boxTriggerer.enabled = boxTriggerer.isTrigger;
        }

        if (texto == "alavanca")
        {
            if (boxTriggerer.isTrigger)
            {
                boxTriggerer.isTrigger = false;
                foreach (GameObject lever in alavancaLista)
                {

                    if (!lever.GetComponent<Alavanca>().ativado)
                        boxTriggerer.isTrigger = true;
                }
                if (portaSprite == null)
                    sprite.enabled = boxTriggerer.isTrigger;
                else
                {
                    if (!boxTriggerer.enabled)
                        AnimPorta(2.9f);
                    else
                        AnimPorta(0.84f);
                }
                boxTriggerer.enabled = boxTriggerer.isTrigger;
            }
            if (!boxTriggerer.isTrigger)
            {
                boxTriggerer.isTrigger = true;
                foreach (GameObject lever in alavancaLista)
                {

                    if (lever.GetComponent<Alavanca>().ativado)
                        boxTriggerer.isTrigger = false;
                }
                if (portaSprite == null)
                    sprite.enabled = boxTriggerer.isTrigger;
                else
                {
                    if (!boxTriggerer.enabled)
                        AnimPorta(2.9f);
                    else
                        AnimPorta(0.84f);
                }
            }
        }



    }

    void AnimPorta(float quantidade)
    {
        if (portaSprite != null && quantidade > 1)
        {
            Debug.Log("Porta abrindo?");
            portaSprite.transform.position = Vector2.MoveTowards
                   (portaSprite.transform.position, new Vector2(portaSprite.transform.position.x, transform.position.y + quantidade), velocidade * Time.deltaTime);
            if (playPorta)
            {
                porta = RuntimeManager.CreateInstance("event:/sfx/porta_abrindo");
                porta.start();
                playPorta = false;
            }
        }
        


        else if (portaSprite != null && quantidade < 1)
        {
            playPorta = true;
            //Debug.Log("Porta fechando?");
            portaSprite.transform.position = Vector2.MoveTowards
                (portaSprite.transform.position, new Vector2(portaSprite.transform.position.x, transform.position.y + quantidade), velocidade * Time.deltaTime);
        }
            
    }

    public void sendParent()
    {
        pontoMov.GetComponent<ChecarMobilidade>().myParent = transform;
    }

    void ProcurarPontoColidiu(Vector2 center, float radius, Transform pontoColisor) 
    {
         Collider2D[] hitColliders = Physics2D.OverlapCircleAll(center, radius);
         int i = 0;
         while (i < hitColliders.Length) 
         {
             if(hitColliders[i].gameObject == pontoColisor)
             {
                  pontoColisor.GetComponent<ChecarMobilidade>().ColidiuParede = true;
             }
             i++;
         }
    }
}

