using UnityEngine;
using UnityEngine.SceneManagement;

public class Control_a : MonoBehaviour
{
    //                               acc            gyro         magnet 
    public float[,] get_input = { { 0, 0, 0 } , { 0, 0, 0 } , { 0, 0, 0 }};
    [System.NonSerialized]public float temp_c = 0;
    public bool get_butt = false;
    //private bool isGrounded = false;
    private Vector3 start_pos;
    private Transform pev_tran;
    [SerializeField] private bool use_keybroad = true;
    [SerializeField] public bool use_controller = true;
    [Range(1.5f, 20f)]public float thrust = 1.5f;
    public Rigidbody rb;

    [SerializeField]public bool switch1 = false;
    [SerializeField] public bool switch2 = false;
    private int collide_count = 0;
    [SerializeField] private GameObject pausePanel;
    public int level = 0;

    // Start is called before the first frame update
    void Start()
    {
        //pausePanel.SetActive(false);
        level = PlayerPrefs.GetInt("level");
        to_level(level);
        Debug.Log(level);
        if (level == 0)
        {
            PauseGame();
        }
        else{
            use_controller = true;
            Debug.Log("use_controller is "+ PlayerPrefs.GetInt("use_controller"));
        }
        start_pos = transform.position;
        Application.targetFrameRate = 60;
        rb = GetComponent<Rigidbody>();
        pev_tran = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            if (!pausePanel.activeInHierarchy)
            {
                PauseGame();
            }
            if (pausePanel.activeInHierarchy)
            {
                ContinueGame();
            }
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (Input.GetKey(KeyCode.R))
        {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            to_level(level);
        }
        else if (Input.GetKey(KeyCode.Alpha1))
        {
            //SceneManager.LoadScene("start_level");
            level = 0;
            PlayerPrefs.SetInt("level", level);
            to_level(level);
        }
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            //SceneManager.LoadScene("level1");
            level = 1;
            PlayerPrefs.SetInt("level", level);
            to_level(level);
        }
        else if (Input.GetKey(KeyCode.Alpha3))
        {
            //SceneManager.LoadScene("level2");
            level = 2;
            PlayerPrefs.SetInt("level", level);
            to_level(level);
        }
        else if (Input.GetKey(KeyCode.Alpha4))
        {
            //SceneManager.LoadScene("level3");
            level = 3;
            PlayerPrefs.SetInt("level", level);
            to_level(level);
        }
        else if (Input.GetKey(KeyCode.Alpha5))
        {
            //SceneManager.LoadScene("level4");
            level = 4;
            PlayerPrefs.SetInt("level", level);
            to_level(level);
        }
        else if (Input.GetKey(KeyCode.Alpha6))
        {
            //SceneManager.LoadScene("level5");
            level = 5;
            PlayerPrefs.SetInt("level", level);
            to_level(level);
        }
    }

    void FixedUpdate()
    {
        /* transform.Rotate(get_acc[0], 0, get_acc[1], Space.World); */
        //transform.position = transform.position + Camera.main.transform.forward * 5 * Time.deltaTime;

        if (use_keybroad)
        {
            Playby_keybroad();
        }
        if (use_keybroad)
        {
            Playby_controller();
        }
        
    }

    void Playby_controller() {
        if (collide_count > 0)
        {
            rb.AddForce(get_input[0, 0] * (get_butt ? thrust : 1) * 10f, 0, get_input[0, 1] * (get_butt ? thrust : 1) * -10f);
            Debug.Log(get_butt);
            //Debug.Log(get_input[0, 0].ToString()+" | "+ get_input[0, 1].ToString());
        }
        //rb.AddForce(Camera.main.transform.forward * 1 * get_input[0, 0]);
    }

    void Playby_keybroad()
    {
        if (collide_count > 0)
        {
            //rb.AddForce(Camera.main.transform.forward * 10 * (get_butt ? thrust : 1) * ((Input.GetKey(KeyCode.W) ? 1 : 0) + (Input.GetKey(KeyCode.S) ? -1 : 0)));
            //rb.AddForce(Camera.main.transform.right * 10 * (get_butt ? thrust : 1) * ((Input.GetKey(KeyCode.D) ? 1 : 0) + (Input.GetKey(KeyCode.A) ? -1 : 0)));
            //get_butt = Input.GetKey(KeyCode.Space) ? true : false;
            //Debug.Log(Camera.main.transform.forward);
            rb.AddForce(((Input.GetKey(KeyCode.A) ? 1 : 0) + ((Input.GetKey(KeyCode.D) ? -1 : 0))) * 10 * (Input.GetKey(KeyCode.Space) ? 10 : 1),
                         0,
                         ((Input.GetKey(KeyCode.S) ? 1 : 0) + ((Input.GetKey(KeyCode.W) ? -1 : 0))) * 10 * (Input.GetKey(KeyCode.Space) ? 10 : 1));
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "to_lv1")
        {
            PlayerPrefs.SetInt("level", 1);
            level = 1;
            PlayerPrefs.SetInt("level", level);
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (other.gameObject.tag == "to_lv2")
        {
            PlayerPrefs.SetInt("level", 2);
            level = 2;
            PlayerPrefs.SetInt("level", level);
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (other.gameObject.tag == "to_lv3")
        {
            PlayerPrefs.SetInt("level", 3);
            level = 3;
            PlayerPrefs.SetInt("level", level);
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (other.gameObject.tag == "to_lv4")
        {
            PlayerPrefs.SetInt("level", 4);
            level = 4;
            PlayerPrefs.SetInt("level", level);
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (other.gameObject.tag == "to_lv5")
        {
            PlayerPrefs.SetInt("level", 5);
            level = 5;
            PlayerPrefs.SetInt("level", level);
           // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (other.gameObject.tag == "Finish")
        {
            //if (SceneManager.GetActiveScene().name == "start_level") { SceneManager.LoadScene("level1"); }
            //if (SceneManager.GetActiveScene().name == "level1") { SceneManager.LoadScene("level2"); }
            //if (SceneManager.GetActiveScene().name == "level2") { SceneManager.LoadScene("level3"); }
           // if (SceneManager.GetActiveScene().name == "level3") { SceneManager.LoadScene("level4"); }
           // if (SceneManager.GetActiveScene().name == "level4") { SceneManager.LoadScene("level5"); }
           // if (SceneManager.GetActiveScene().name == "level5") { Application.Quit(); }
           Application.Quit();
        }
        if (other.gameObject.tag == "Gravity_swarp")
        {
            Physics.gravity = new Vector3(0, -10.0F, 0);
            Debug.Log("Gravity swarped");
        }

        else if (other.gameObject.tag == "Gravity_swarp_back")
        {
            Physics.gravity = new Vector3(0, 10.0F, 0);
            Debug.Log("Gravity swarped back");
        }
        if (other.gameObject.tag == "Dead_mesh") {
            to_level(level);
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            //transform.position = start_pos;
            //GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        collide_count++;
    }

    void OnCollisionExit(Collision other)
    {
        collide_count--;
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        //Disable scripts that still work while timescale is set to 0
    }
    public void ContinueGame()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        //enable the scripts again
    }

    public void toggle_pressed(bool newval)
    {
        use_controller = !use_controller;
        PlayerPrefs.SetInt("use_controller", use_controller ? 1: 0);
        //Debug.Log("new val is " + use_controller);
    }

    public void butt_pressed()
    {
        ContinueGame();
    }
    private void to_level(int lv)
    {
        if (lv == 0)
        {
            transform.position = GameObject.Find("start_lv0").transform.position;
        }
        if (lv == 1)
        {
            transform.position = GameObject.Find("start_lv1").transform.position;
        }
        if (lv == 2)
        {
            transform.position = GameObject.Find("start_lv2").transform.position;
        }
        if (lv == 3)
        {
            transform.position = GameObject.Find("start_lv3").transform.position;
        }
        if (lv == 4)
        {
            transform.position = GameObject.Find("start_lv4").transform.position;
        }
        if (lv == 5)
        {
            transform.position = GameObject.Find("start_lv5").transform.position;
        }
    }
}
