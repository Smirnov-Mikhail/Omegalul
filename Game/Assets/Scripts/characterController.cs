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


    private bool amIDead = false;
    private Animator animator;
    private Rigidbody2D rigidBody2d;

    private bool HeroDies
    {
        get { return animator.GetBool("HeroDies"); }
        set { animator.SetBool("HeroDies", value); }
    }

    private HeroState State
    {
        get { return (HeroState)animator.GetInteger("State"); }
        set { animator.SetInteger("State", (int)value); }
    }

    private GameObject star;

    private void Awake() {
        animator = GetComponent<Animator>();
        rigidBody2d = GetComponent<Rigidbody2D>();
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
        if(HeroDies)
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
            col.gameObject.name == "FlyDangerBurger(Clone)" ||
            col.gameObject.name == "Shot(Clone)")
        {
            if(!HeroDies)
            {
                rigidBody2d.velocity = Vector2.zero;
                State = HeroState.None;
                HeroDies = true;
                letDie = 100;
                return;
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

        if (col.gameObject.name == "InBalka")
        {
            StatisticData.instance.IsInBalka = true;
        }

        if (col.gameObject.name == "OpenEyes")
        {
            StatisticData.instance.OpenEyes = true;
        }

        if (col.gameObject.name == "ThroughRoof")
        {
            StatisticData.instance.ThroughRoof = true;
        }

        if (col.gameObject.name == "ThroughFloor")
        {
            StatisticData.instance.ThroughFloor = true;
        }

        if (col.gameObject.name == "InHurry")
        {
            StatisticData.instance.InHurry = true;
        }

        if (col.gameObject.name == "Headbutt")
        {
            StatisticData.instance.Headbutt = true;
        }

        if (col.gameObject.name == "endLevelSaveLoad")
        {
            StatisticData.instance.FinishLevels = 1;
            Application.LoadLevel("windowsExeption1");
        }

        if (col.gameObject.name == "endLevelOutOfBounds")
        {
            StatisticData.instance.FinishLevels = 2;
            Application.LoadLevel("windowsExeption2");
        }

        if (col.gameObject.name == "endLevelLoadBarLevel")
        {
            StatisticData.instance.FinishLevels = 3;
            Application.LoadLevel("windowsExeption3");
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
        var rect = new Rect(Screen.width * 3 / 10, Screen.height - 80, Screen.width - Screen.width * 6 / 10, 55);
        switch (Application.loadedLevelName)
        {
            case "scene0":
                if (StatisticData.instance.FinishLevels == 0)
                {
                    GUI.Box(rect, "Привет. Перед тобой обычный платформер.\n Не обращай внимание - иди дальше. \n WASD - двигаться. R - начать уровень заново");
                    break;
                }
                if (StatisticData.instance.FinishLevels == 1)
                {
                    GUI.Box(rect, "Надеюсь, следующий мир, в который я попаду,\n будет написан не Майкрософтом.");
                    break;
                }
                if (StatisticData.instance.FinishLevels == 2)
                {
                    GUI.Box(rect, "А теперь серьезный вопрос:\nЗачем мне меч и щит,\nесли я не могу сражаться?");
                    break;
                }
                if (StatisticData.instance.FinishLevels == 3)
                {
                    GUI.Box(rect, "А теперь серьезный вопрос:\nЗачем мне меч и щит,\nесли я не могу сражаться?");
                    break;
                }
                break;
            case "save-load":
                if (!StatisticData.instance.NeedReload && !StatisticData.instance.Rebooted)
                    GUI.Box(rect
                        , "Ты всегда первым делом прыгаешь в пропасть\n или только когда управляешь другими? ");
                else if (StatisticData.instance.NeedReload && !StatisticData.instance.Rebooted)
                    GUI.Box(rect
                        , "Ну да, ворота не открываются.\n И что делать будешь, программист?\n Ребутнешь?");
                else if (StatisticData.instance.Rebooted)
                    GUI.Box(rect
                        , "");
                break;
            case "outOfBounds":
                if (StatisticData.instance.ThroughFloor)
                    GUI.Box(rect, "......................\n....................\n................");
                else if (StatisticData.instance.ThroughRoof)
                    GUI.Box(rect, "Ты ведь в курсе, что это неприятно?");
                else if (StatisticData.instance.OpenEyes)
                    GUI.Box(rect, "И куда мне, по-твоему, идти?\nСзади слизняк, впереди обрыв...\nПочему я вообще следую твоим советам?");
                else if (StatisticData.instance.IsInBalka)
                    GUI.Box(rect, "Ай.");
                break;
            case "loadbar_level":
                if (StatisticData.instance.Headbutt)
                    GUI.Box(rect, "Знаешь, когда-нибудь я тебя найду...");
                else if (StatisticData.instance.InHurry)
                    GUI.Box(rect, "И куда?\nХоть бы уровень прогрузил сначала.");
                break;
            default:
                GUI.Box(rect, "Просто двигайся дальше, друг");
                break;
        }
    }
}


public enum HeroState {
    Idle,
    Run,
    Jump,
    None
}