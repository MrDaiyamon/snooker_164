using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
public class GameManager : MonoBehaviour
{
    [SerializeField]
    private int playerScore;
    public int PlayerScore { get { return playerScore; } set { playerScore = value; } }

    [SerializeField]
    private GameObject[] ballPositions;

    [SerializeField]
    private GameObject ballPrefab;

    [SerializeField]
    private GameObject cueBall;

    [SerializeField]
    private float xInput = 0f;

    [SerializeField]
    private GameObject cam;

    [SerializeField]
    private GameObject ballLine;

    [SerializeField]
    private TMP_Text notiText;

    public static GameManager instance;

    void Awake()
    {
        instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetBall(BallColor.Red, 1);
        SetBall(BallColor.Yellow, 2);
        SetBall(BallColor.Green, 3);
        SetBall(BallColor.Brown, 4);
        SetBall(BallColor.Blue, 5);
        SetBall(BallColor.Pink, 6);
        SetBall(BallColor.Black, 7);

        CameraBehideCueBall();

        if (Settings.fromSave)
        {
            LoadGame();
        }
    }

    // Update is called once per frame
    void Update()
    {
        RotateBall();
        
        if(Keyboard.current.spaceKey.wasPressedThisFrame)
            ShootBall();

        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
            xInput = -0.2f;
        else if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
            xInput = 0.2f;
        else
            xInput = 0f;

        if(Keyboard.current.backspaceKey.wasPressedThisFrame)
            StopBall();

        if (Keyboard.current.leftShiftKey.isPressed && Keyboard.current.sKey.wasPressedThisFrame)
            SaveGame();
    }

    private void SetBall(BallColor col, int i)
    {
        GameObject obj = Instantiate(ballPrefab,
                    ballPositions[i].transform.position,
                    Quaternion.identity);

        Ball b= obj.GetComponent<Ball>();
        b.SetColorAndPoint(col);
    }

    private void ShootBall()
    {
        Rigidbody rb = cueBall.GetComponent<Rigidbody>();
        rb.AddRelativeForce(Vector3.forward * 60, ForceMode.Impulse);
        ballLine.SetActive(false);

        cam.transform.parent = null;
        cam.transform.position = new Vector3(0f, 30f, -42f);
        cam.transform.eulerAngles = new Vector3(45f, 0f, 0f);
    }

    private void RotateBall()
    {
        if (cueBall != null)
            cueBall.transform.Rotate(0f, xInput, 0f);
    }

    private void StopBall()
    {
        Rigidbody rb = cueBall.GetComponent<Rigidbody>();
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        cueBall.transform.eulerAngles = new Vector3(0f, 0f, 0f);
        ballLine.SetActive(true);
        CameraBehideCueBall();
    }

    private void CameraBehideCueBall()
    {
        cam.transform.parent = cueBall.transform;
        cam.transform.position = cueBall.transform.position + new Vector3(0f, 7f, -15f);
        cam.transform.eulerAngles = new Vector3(30f, 0f, 0f);
    }

    public void ShowNotiText(int n)
    {
        playerScore += n;
        notiText.text = $"This ball:{n}\nTotal Score is {playerScore}";
    }
    public void ShowString(string s)
    {
        notiText.text = s + $"\nTotal Score is {playerScore}" ;
    }
    private void SaveGame()
    {
        StopBall();

        if (cueBall != null)
        {
            PlayerPrefs.SetFloat("cueBallX", cueBall.transform.position.x);
            PlayerPrefs.SetFloat("cueBallY", cueBall.transform.position.y);
            PlayerPrefs.SetFloat("cueBallZ", cueBall.transform.position.z);
        }
    }

    private void LoadGame()
    {
        if (cueBall != null)
        {
            float x = PlayerPrefs.GetFloat("cueBallX", cueBall.transform.position.x);
            float y = PlayerPrefs.GetFloat("cueBallY", cueBall.transform.position.y);
            float z = PlayerPrefs.GetFloat("cueBallZ", cueBall.transform.position.z);
            cueBall.transform.position = new Vector3(x, y, z);
        }
    }
}
