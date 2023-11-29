using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private Sprite bgImage;
    [SerializeField]

    public Sprite[] puzzles;
    public List<Sprite> gamePuzzles = new List<Sprite>();
    public List<Button> btns = new List<Button>();
    private bool firstGuess, secondGuess;
    private int countCorrectGuess;
    public TMPro.TextMeshProUGUI endGameText;

    private int gameGuesses;
    private int firstGuessIndex, secondGuessIndex;

    private string firstGuessPuzzle, secondGuessPuzzle;
    void Awake()
    {
        puzzles = Resources.LoadAll<Sprite>("Sprites/CartaVerso");
    }
    void Start()
    {
        

    GetButtons();
    AddListeners();
    AddGamePuzzles();
    Shuffle(gamePuzzles);
    gameGuesses = gamePuzzles.Count / 2;
    }

    void GetButtons()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("PuzzleButton");

        for (int i = 0; i < objects.Length; i++)
        {
            btns.Add(objects[i].GetComponent<Button>());
            btns[i].image.sprite = bgImage;
        }
    }

    void AddGamePuzzles()
    {

        int looper = btns.Count;
        int index = 0;

        for (int i = 0; i < looper; i++)
        {

            if (index == looper)
            {
                index = 0;
            }

            gamePuzzles.Add(puzzles[index]);

            index++;
        }
    }


    void AddListeners()
    {
        foreach (Button btn in btns)
        {
            Debug.Log("Click");
            btn.onClick.AddListener(() => PickAPuzzle());
        }
    }

    public void PickAPuzzle()
    {

        if (!firstGuess)
        {

            firstGuess = true;

            firstGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject?.name);

            firstGuessPuzzle = gamePuzzles[firstGuessIndex].name;

            btns[firstGuessIndex].image.sprite = gamePuzzles[firstGuessIndex];
            StartCoroutine(RotateCard(btns[firstGuessIndex].transform, 180f, 360f));

        }
        else if (!secondGuess)
        {

            secondGuess = true;

            secondGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject?.name);

            secondGuessPuzzle = gamePuzzles[secondGuessIndex].name;

            btns[secondGuessIndex].image.sprite = gamePuzzles[secondGuessIndex];
            StartCoroutine(RotateCard(btns[secondGuessIndex].transform, 180f, 360f));

            StartCoroutine(CheckIfThePuzzleMatch());
        }
    }

    IEnumerator CheckIfThePuzzleMatch()
    {

        Debug.Log("First: " + firstGuessPuzzle + "Second: " + secondGuessPuzzle);

        yield return new WaitForSeconds(1f);

        if ((firstGuessPuzzle == "CartaVerso_6" && secondGuessPuzzle == "CartaVerso_4") ||
             (secondGuessPuzzle == "CartaVerso_6" && firstGuessPuzzle == "CartaVerso_4"))
        {

            // Banana + Potassio 
            yield return new WaitForSeconds(.5f);

            btns[firstGuessIndex].interactable = false;
            btns[secondGuessIndex].interactable = false;

            btns[firstGuessIndex].image.color = new Color(0, 0, 0, 0);
            btns[secondGuessIndex].image.color = new Color(0, 0, 0, 0);

            CheckIfTheGameIsFinished();

        }
        else if ((firstGuessPuzzle == "CartaVerso_5" && secondGuessPuzzle == "CartaVerso_7") ||
             (secondGuessPuzzle == "CartaVerso_5" && firstGuessPuzzle == "CartaVerso_7"))
        {

            // Maça + Fibra 
            yield return new WaitForSeconds(.5f);

            btns[firstGuessIndex].interactable = false;
            btns[secondGuessIndex].interactable = false;

            btns[firstGuessIndex].image.color = new Color(0, 0, 0, 0);
            btns[secondGuessIndex].image.color = new Color(0, 0, 0, 0);

            CheckIfTheGameIsFinished();

        }
        else if ((firstGuessPuzzle == "CartaVerso_0" && secondGuessPuzzle == "CartaVerso_2") ||
             (secondGuessPuzzle == "CartaVerso_0" && firstGuessPuzzle == "CartaVerso_2"))
        {

            // Morango + Vitamina C 
            yield return new WaitForSeconds(.5f);

            btns[firstGuessIndex].interactable = false;
            btns[secondGuessIndex].interactable = false;

            btns[firstGuessIndex].image.color = new Color(0, 0, 0, 0);
            btns[secondGuessIndex].image.color = new Color(0, 0, 0, 0);

            CheckIfTheGameIsFinished();

        }
        else if ((firstGuessPuzzle == "CartaVerso_1" && secondGuessPuzzle == "CartaVerso_3") ||
             (secondGuessPuzzle == "CartaVerso_1" && firstGuessPuzzle == "CartaVerso_3"))
        {

            // Cereja + Antioxidante
            yield return new WaitForSeconds(.5f);

            btns[firstGuessIndex].interactable = false;
            btns[secondGuessIndex].interactable = false;

            btns[firstGuessIndex].image.color = new Color(0, 0, 0, 0);
            btns[secondGuessIndex].image.color = new Color(0, 0, 0, 0);

            CheckIfTheGameIsFinished();

        }
        else
        {

            yield return new WaitForSeconds(.5f);

            btns[firstGuessIndex].image.sprite = bgImage;
            btns[secondGuessIndex].image.sprite = bgImage;

        }

        yield return new WaitForSeconds(.5f);

        firstGuess = secondGuess = false;
    }

    void CheckIfTheGameIsFinished()
    {

        countCorrectGuess++;

        if (countCorrectGuess == gameGuesses)
        {
            endGameText.text = "Parabéns! Você completou o jogo!";

            Invoke("RestartGame", 5f);
        }
    }

    void Shuffle(List<Sprite> list)
    {

        for (int i = 0; i < list.Count; i++)
        {

            Sprite temp = list[i];
            int randomIndex = Random.Range(0, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
    private IEnumerator RotateCard(Transform cardTransform, float startRotation, float targetRotation)
    {
        float elapsedTime = 0f;
        float duration = 0.5f;

        cardTransform.rotation = Quaternion.Euler(0f, startRotation, 0f);

        while (elapsedTime < duration)
        {
            cardTransform.rotation = Quaternion.Euler(0f, Mathf.Lerp(startRotation, targetRotation, elapsedTime / duration), 0f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        cardTransform.rotation = Quaternion.Euler(0f, targetRotation, 0f);
    }
    void RestartGame()
    {
        foreach (Button btn in btns)
        {
            btn.image.sprite = bgImage;
            btn.interactable = true;
            btn.image.color = Color.white;
        }
        countCorrectGuess = 0;

        endGameText.text = "";
    }
}
