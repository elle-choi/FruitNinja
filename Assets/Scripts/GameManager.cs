using System.Collections; // use when using co-routines
using UnityEngine;
using UnityEngine.UI; // using built-in Unity UI
using UnityEngine.SceneManagement;

using System; // added for writing text files
using System.IO; // added for writing text files

public class GameManager : MonoBehaviour
{
    //public static GameManager Instance;

    public Text scoreText; // reference so we can update this
    //public Text finalScoreText; // TODO
    public Image fadeImage;
    //public Text numText;

    public static int score;
    private Blade blade;
    private Spawner spawner;
    private string assetPath;


    private void Awake()
    {
        //Instance = this; //TODO: delete 2 lines if not
        //DontDestroyOnLoad(gameObject); //TODO 

        blade = FindObjectOfType<Blade>();
        spawner = FindObjectOfType<Spawner>();
    }

    private void Start() // Unity's built-in function Start()
    {
        NewGame();
    }

    public void NewGame()
    {
        blade.enabled = true;
        spawner.enabled = true;

        score = 0;
        scoreText.text = score.ToString();

        ClearScene();
    }

    private void ClearScene()
    {
        Fruit[] fruits = FindObjectsOfType<Fruit>();

        foreach (Fruit fruit in fruits)
        {
            Destroy(fruit.gameObject); // make sure you do "Fruit.gameObject" or else script will be destroyed
        }

        Bomb[] bombs = FindObjectsOfType<Bomb>();

        foreach (Bomb bomb in bombs)
        {
            Destroy(bomb.gameObject); 
        }
    }

    
    public void IncreaseScore() // public to be called from other scripts
    {
        score++;
        scoreText.text = score.ToString();
    }

    /*
    public void TextFile()
    {
        var fileName = "MyFile.txt";

        if (File.Exists(fileName))
        {
            Debug.Log(fileName + " already exists.");
            return;
        }
        var sr = File.CreateText(fileName);
        sr.WriteLine("Score: ", score);
        sr.Close();
    }
    */


    /*
    static void WriteString()
    {
        string path = "Assets/Data/fruitscore.txt";

        //Write some text to the fruitscore.txt
        StreamWriter writer = new StreamWriter(path, true);

        writer.WriteLine("Score: ", score);
        writer.Close();
    }
    */
    
    public void TextFile() // TODO: check this
    {
        assetPath = Application.dataPath;

        //Output the Game data path to the console
        Debug.Log("dataPath : " + assetPath);

        // writing file to desktop
        //string docPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        // Append text to an existing file named "FruitScore.txt".
        using (StreamWriter outputFile = new StreamWriter(Path.Combine(assetPath, "FruitScore.txt"), true))
        {
            outputFile.WriteLine("Score: " + score);
        }
        Debug.Log("File Written");
    }
    
    

    /*
     * add game over here because Game Manager handles overall logic of a game
     */


    public void Explode()
    {
        blade.enabled = false;
        spawner.enabled = false;

        StartCoroutine(ExplodeSequence());
    }
    

    // Co-Routine for our Explode Game Over Scene
    private IEnumerator ExplodeSequence()
    {
        float elapsed = 0f;
        float duration = 1.5f;

        while (elapsed < duration)
        {
            float t = Mathf.Clamp01(elapsed / duration); // clamp btwn 0 and 1

            // when t is 0 (0%) is clear t is 1 (100%) is white
            // as elapsed increases, t increases and become white
            fadeImage.color = Color.Lerp(Color.clear, Color.white, t);

            Time.timeScale = 1f - t; // inverse
            elapsed += Time.unscaledDeltaTime; // add time elased since last frame

            yield return null; // wait & loop over to next frame
        }

        yield return new WaitForSecondsRealtime(1f); // wait in white screen for 1 second


        TextFile(); // TODO: check this 

        
        SceneManager.LoadScene(2);
    }

}
