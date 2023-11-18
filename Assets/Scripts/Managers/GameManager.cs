using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public DialogueManager dialogueManager;


    #region Word Player
    public WordHolder wordHolder;
    public Word selectedWord;
    int letterIndex;

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

    #region Singleton
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }
            return instance;
        }
    }
    #endregion

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
        DontDestroyOnLoad(gameObject);
        player = GetComponent<Player>();
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
        if (SceneManager.GetActiveScene().name.Contains("Level") && gameState == GameState.MENU)
        {
            Debug.Log("In Scene");
        }

        WordleAttack();
    }

    public void WordleAttack()
    {
        foreach (KeyCode vKey in SUPPORTED_KEYS)
        {
            if (gameState == GameState.WORDSMITHING && selectedWord != null)
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
                        boss.TakeDamage(20);
                        StopAllCoroutines();
                        Destroy(selectedWord.gameObject);
                        StartCoroutine(Attack());
                        Debug.Log("Word has been finished");
                        if (player.pState == Player.PlayerState.GRABBED)
                        {
                            struggleSuccess += 1;

                            if (struggleSuccess > boss.grabStrength)
                            {
                                player.pState = Player.PlayerState.DEFAULT;
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
            Debug.Log("COUNTING");
            yield return new WaitForSeconds(3f);
            hasCounted = true;
            gameState = GameState.WORDSMITHING;
        }

        while (IsPlayerAlive() || IsBossAlive())
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
            player.pState = Player.PlayerState.GRABBED;
            boss.Grab(true);
            StopAllCoroutines();
        }
        nextWord.CreateLetters();

        yield return new WaitForSeconds(4f);
        player.TakeDamage(true, 20);
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

        yield return new WaitForSeconds(3f);
        player.TakeDamage(true, 20);
        struggleFailed += 1;
        Destroy(wordObject);
        if (struggleFailed >= boss.grabStrength)
        {
            player.pState = Player.PlayerState.FUCKED;
        }
    }

    IEnumerator FuckedState()
    {
        letterIndex = 0;
        var wordObject = Instantiate(wordHolder.wordPrefab, wordHolder.transform);
        Word nextWord = wordObject.GetComponent<Word>();
        selectedWord = nextWord;
        nextWord.word = boss.Attack(boss.grabAttacks);
        nextWord.CreateLetters();
        yield return new WaitForSeconds(3f);
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
