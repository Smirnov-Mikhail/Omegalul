using UnityEngine;
using Assets.Scripts;

public class characterController : MonoBehaviour
{
    public float maxSpeed = 10f;
    public float jumpForce = 700f;
    bool facingRight = true;
    bool grounded = false;
    public Transform groundCheck;
    public float groundRadius = 0.2f;
    public LayerMask whatIsGround;
    public float score;
    public float move;
    public bool locked;
    public int letJump = 0;
    public int letDie = 0;

    private Animator animator;
    private HeroState State
    {
        get { return (HeroState)animator.GetInteger("State"); }
        set { animator.SetInteger("State", (int)value); }
    }

    private GameObject star;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        
        move = Input.GetAxis("Horizontal");
    }

    void Update()
    {
        if(State == HeroState.Dead)
        {
            letDie--;

            if(letDie < 0)
            {
                Application.LoadLevel(Application.loadedLevel);
            }
            return;
        }
        if (grounded && letJump == 0)
        {
            State = HeroState.Idle;
        }
        
        if(State == HeroState.Jump)
        {
            letJump = Mathf.Max(0, letJump - 1);
        }

        if (locked)
            return;

        if (grounded && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)))
        {
            State = HeroState.Jump;
            letJump = 60;
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce));
        }
        GetComponent<Rigidbody2D>().velocity = new Vector2(move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

        if (move != 0 && grounded && letJump == 0) {
            State = HeroState.Run;
        }

        if (move > 0 && !facingRight)
            Flip();
        else if (move < 0 && facingRight)
            Flip();

        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (Input.GetKey(KeyCode.R))
        {
            if (StatisticData.instance.NeedReload)
                StatisticData.instance.Rebooted = true;
            Application.LoadLevel(Application.loadedLevel);
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "dieCollider" || 
            col.gameObject.name == "saw" ||
            col.gameObject.name == "DangerBurger" ||
            col.gameObject.name == "FlyDangerBurger(Clone)")
        {
            State = HeroState.Dead;
            if(letDie <= 0)
            {
                letDie = 10;
            }
        }

        if (col.gameObject.name == "star")
        {
            score++;
            Destroy(col.gameObject);
        }

        if (col.gameObject.name == "Spring")
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 600));
        }

        if (col.gameObject.name == "needReload")
        {
            StatisticData.instance.NeedReload = true;
        }

        if (col.gameObject.name == "endLevelSaveLoad")
        {
            StatisticData.instance.FinishLevels = 1;
            Application.LoadLevel("scene0");
        }

        if (col.gameObject.name == "endLevelOutOfBounds")
        {
            StatisticData.instance.FinishLevels = 2;
            Application.LoadLevel("scene0");
        }

        if (col.gameObject.name == "endLevelOutOfBounds")
        {
            StatisticData.instance.FinishLevels = 2;
            Application.LoadLevel("scene0");
        }

        if (col.gameObject.name == "nextLevel")
        {
            switch (StatisticData.instance.FinishLevels)
            {
                case 0:
                    Application.LoadLevel("save-load");
                    return;
                case 1:
                    Application.LoadLevel("outOfBounds");
                    return;
                case 2:
                    Application.LoadLevel("loadbar_level");
                    return;
                case 3:
                    Application.LoadLevel("save-load");
                    return;
                default:
                    Application.LoadLevel(Application.loadedLevel);
                    return;

            }
            /*if (!(GameObject.Find("star"))) Application.LoadLevel ("save-load");
				}*/
        }

        if (col.gameObject.name == "finishCollider" && !StatisticData.instance.EndButtonIsActive)
            Application.LoadLevel("gameOver");
    }

    void OnGUI()
    {
        //GUI.Box(new Rect (0, 0, 100, 100), "Stars: " + score);
        switch (Application.loadedLevelName)
        {
            case "scene0":
                if (StatisticData.instance.FinishLevels == 0)
                    GUI.Box(new Rect(Screen.width / 7, Screen.height - 100, Screen.width - Screen.width * 2 / 7, 100)
                        , "Привет. Перед тобой обычный платформер.\n Не обращай внимание - иди дальше. \n WASD - двигаться. R - начать уровень заново");
                break;
            case "save-load":
                if (!StatisticData.instance.NeedReload && !StatisticData.instance.Rebooted)
                    GUI.Box(new Rect(Screen.width / 7, Screen.height - 100, Screen.width - Screen.width * 2 / 7, 100)
                        , "Вижу ты пошёл вспять и попал вперёд. Так держать.");
                else if (StatisticData.instance.NeedReload && !StatisticData.instance.Rebooted)
                    GUI.Box(new Rect(Screen.width / 7, Screen.height - 100, Screen.width - Screen.width * 2 / 7, 100)
                        , "Иногда чтобы что-то починить. Нужно перезапустить.");
                else if (StatisticData.instance.Rebooted)
                    GUI.Box(new Rect(Screen.width / 7, Screen.height - 100, Screen.width - Screen.width * 2 / 7, 100)
                        , "Не находишь забавным, что преступник \nзачастую возвращается на место преступления?");
                break;
            default:
                GUI.Box(new Rect(Screen.width / 7, Screen.height - 100, Screen.width - Screen.width * 2 / 7, 100)
                        , "Просто двигайся дальше, друг");
                break;
        }
    }
}


public enum HeroState {
    Idle,
    Run,
    Jump,
    Dead
}