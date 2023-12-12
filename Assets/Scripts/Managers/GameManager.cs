using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    #endregion

    public DialogueManager dialogueManager;
    public LevelUIController uIController;

    #region Word Player
    public WordHolder wordHolder;
    public Word selectedWord;
    int letterIndex;
    public Timer timer;
    public Countdown countdown;
    #endregion

    public enum GameState
    {
        MENU,
        GAMESTARTED,
        WORDSMITHING,
        DIALOGUE,
        ENDING
    }
    public GameState gameState;


    [Space]
    [Space]

    #region Game Mechanic
    public OrcBoss boss;
    public Player player;

    public float countDownTimer = 3;

    public bool hasCounted = false;
    public bool attacked = false;
    float attackStartTime;

    public float timeDelay = 2.5f;
    public float attackWordTimer = 4.0f;
    public bool spawnReady = false;
    #endregion

    int struggleSuccess;
    int struggleFailed;

    public StatusEffectHandler effectHandler;

    public bool paused = false;

    private static readonly KeyCode[] SUPPORTED_KEYS = new KeyCode[]
    {
        KeyCode.A, KeyCode.B, KeyCode.C, KeyCode.D, KeyCode.E, KeyCode.F,
        KeyCode.G, KeyCode.H, KeyCode.I, KeyCode.J, KeyCode.K, KeyCode.L,
        KeyCode.M, KeyCode.N, KeyCode.O, KeyCode.P, KeyCode.Q, KeyCode.R,
        KeyCode.S, KeyCode.T, KeyCode.U, KeyCode.V, KeyCode.W, KeyCode.X,
        KeyCode.Y, KeyCode.Z
    };
    // Start is called before the first frame update

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        player = GetComponent<Player>();
        effectHandler = GameObject.Find("Status Effect UI").GetComponent<StatusEffectHandler>();
        uIController = GameObject.Find("Canvas").GetComponent<LevelUIController>();
        dialogueManager = GetComponent<DialogueManager>();
    }

    void Start()
    {
        gameState = GameState.MENU;
        letterIndex = 0;
        hasCounted = false;
        StartCoroutine(Attack());
    }

    // Update is called once per frame
    void Update()
    {

        //Debug.Log(System.Enum.GetName(typeof(Player.PlayerState), player.pState));

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;
            dialogueManager.PauseGame(paused);
        }

        if ((player.currentHealth <= 0 || player.currentLust >= player.MaxLust) && gameState != GameState.ENDING)
        {
            //Debug.Log("Should only happen once");
            gameState = GameState.ENDING;
            GameEnding();
            dialogueManager.PlayDialogue(boss.healthLostDialogue);
        }

        if ((boss.currentHealth <= 0 || boss.currentLust >= boss.MaxLust) && gameState != GameState.ENDING)
        {
            //Debug.Log("Should only happen once");
            gameState = GameState.ENDING;
            GameEnding();
            dialogueManager.PlayDialogue(boss.lustLostDialogue);
        }

        if (gameState == GameState.WORDSMITHING)
        {
            WordleAttack();
        }
    }

    public void WordleAttack()
    {
        foreach (KeyCode vKey in SUPPORTED_KEYS)
        {
            if (selectedWord != null)
            {
                if (Input.GetKeyDown(vKey) && letterIndex < selectedWord.word.Length)
                {
                    AudioManager.Instance.PlayRandomSound();
                    if (selectedWord.word[letterIndex] == ((char)vKey))
                    {
                        selectedWord.HighlightLetter(letterIndex);
                        letterIndex++;
                        //Debug.Log("CORRECT LETTER?");
                    }
                    else if (selectedWord.word[letterIndex] != ((char)vKey) && letterIndex != selectedWord.word.Length - 1)
                    {
                        selectedWord.ClearHighlight();
                        letterIndex = 0;
                        //Debug.Log("Word not finished and we fucked up so restarting");
                    }

                    if (letterIndex >= selectedWord.word.Length)
                    {
                        boss.TakeDamage(boss.damage);
                        AudioManager.Instance.PlaySound(AudioManager.Instance.wordPop);
                        StopAllCoroutines();
                        Destroy(selectedWord.gameObject);
                        StartCoroutine(Attack());
                        //Debug.Log("Word has been finished");
                        if (player.pState == Player.PlayerState.GRABBED)
                        {
                            struggleSuccess += 1;

                            if (struggleSuccess >= boss.grabStrength)
                            {
                                struggleSuccess = 0;
                                struggleFailed = 0;
                                player.pState = Player.PlayerState.DEFAULT;
                                effectHandler.TurnOffEffect("chained");
                                boss.Grab(false);
                            }
                        }
                    }
                }
            }

        }
    }

    IEnumerator Attack()
    {
        if (!hasCounted)
        {
            countdown.CountDownStart(3f);
            yield return new WaitUntil(() => countdown.CountDownEnd());
            wordHolder.gameObject.SetActive(true);
            hasCounted = true;
            gameState = GameState.WORDSMITHING;
        }

        while (gameState != GameState.ENDING)
        {
            if (player.pState == Player.PlayerState.DEFAULT)
            {
                yield return StartCoroutine(PlayState());
            }
            else if (player.pState == Player.PlayerState.GRABBED)
            {
                yield return StartCoroutine(GrabState());
            }
            else if (player.pState == Player.PlayerState.FUCKED)
            {
                yield return StartCoroutine(FuckedState());
            }
        }
    }

    IEnumerator PlayState()
    {
        letterIndex = 0;
        var wordObject = Instantiate(wordHolder.wordPrefab, wordHolder.transform);
        Word nextWord = wordObject.GetComponent<Word>();
        selectedWord = nextWord;
        nextWord.word = boss.Attack(boss.attacks);
        if (nextWord.word == "grapple")
        {
            nextWord.word = boss.Attack(boss.grabAttacks);
            player.pState = Player.PlayerState.GRABBED;
            effectHandler.TurnOnEffect("chained");
            boss.Grab(true);
            boss.Fuck(false);
        }
        nextWord.CreateLetters();
        timer.TimerStart(4f);
        yield return new WaitUntil(() => timer.TimerEnded());

        //yield return new WaitForSeconds(4f);
        player.TakeDamage(true, 5);
        Destroy(wordObject);

    }

    IEnumerator GrabState()
    {
        letterIndex = 0;
        var wordObject = Instantiate(wordHolder.wordPrefab, wordHolder.transform);
        Word nextWord = wordObject.GetComponent<Word>();
        selectedWord = nextWord;
        nextWord.word = boss.Attack(boss.grabAttacks);
        nextWord.CreateLetters();
        timer.TimerStart(3f);

        yield return new WaitUntil(() => timer.TimerEnded());

        player.TakeDamage(false, 10);
        Destroy(wordObject);

        struggleFailed += 1;
        if (struggleFailed >= boss.grabStrength)
        {
            struggleSuccess = 0;
            struggleFailed = 0;
            player.pState = Player.PlayerState.FUCKED;
            boss.Grab(false);
            boss.Fuck(true);
        }
    }

    IEnumerator FuckedState()
    {
        letterIndex = 0;
        var wordObject = Instantiate(wordHolder.wordPrefab, wordHolder.transform);
        Word nextWord = wordObject.GetComponent<Word>();
        selectedWord = nextWord;
        nextWord.word = boss.Attack(boss.fuckAttacks);
        nextWord.CreateLetters();
        timer.TimerStart(3f);

        yield return new WaitUntil(() => timer.TimerEnded());
        Destroy(wordObject);

    }

    public void GameEnding()
    {
        StopAllCoroutines();
        timer.gameObject.SetActive(false);
        uIController.playerUI.gameObject.SetActive(false);
        uIController.enemyUI.gameObject.SetActive(false);
        effectHandler.TurnOffEffect("chained");
        boss.Grab(false);
        boss.Fuck(false);

    }

    bool IsPlayerAlive()
    {
        return player.currentHealth > 0 || player.currentLust < player.MaxLust;
    }

    bool IsBossAlive()
    {
        return boss.currentHealth > 0 || boss.currentLust < boss.MaxLust;
    }
}
