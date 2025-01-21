using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Button : MonoBehaviour
{
    [SerializeField] public Image choosenPortrait;
    [SerializeField] public List<Sprite> images;
    [SerializeField] public List<GameObject> ModeImagesForButton;
    [SerializeField] protected List<GameObject> gameObjects;
    [SerializeField] public GameObject firstButtonImage, secondButtonImage;
    public GameObject currentGameObject;
    protected float mouseTreshold = 55f;
    protected Vector3 initialMousePos;
    protected float angle, centerX, centerY;
    protected int indexToActivate;
    protected bool isCoroutineRunning;
    Vector3 currentMousePos;
    public string animalName;
    public Animator animator, animator2;
    private bool canSwitch = true, upperButton = true; //upperButton hangi butonu kullanmam gerektigine karar veriyor (satir 118)

    void Start()
    {
        currentGameObject = gameObjects[0];
        animator = firstButtonImage.GetComponent<Animator>();
        animator2 = secondButtonImage.GetComponent<Animator>();

        choosenPortrait.sprite = images[0];


    }

    protected void Update()
    {
        //Ctrl bastigimiz an mouse nerdeyse orayi aliyor
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            initialMousePos = Input.mousePosition;
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            currentMousePos = Input.mousePosition; //Ctrl basiliyken su anki mouse pozisyonunu buluyor

            if (Vector3.Distance(currentMousePos, initialMousePos) >= mouseTreshold && !isCoroutineRunning)
            {
                centerX = Screen.width / 2;
                centerY = Screen.height / 2;

                angle = Mathf.Atan2(currentMousePos.y - centerY, currentMousePos.x - centerX) * Mathf.Rad2Deg;

                if (angle < 0)
                    angle += 360;

                indexToActivate = findIndexFromAngle(angle);
                Player.instance.StartCoolDownCouroutine(waitToActivate(indexToActivate));
                initialMousePos = currentMousePos;
            }
        }

        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            currentGameObject.SetActive(false);
            ChangePlayerMode();
        }

        switchToOtherButton();
    }

    //Mouse'a gore hangi yer aktif edilcek bulma fonksiyonu
    public int findIndexFromAngle(float angle)
    {
        if (angle < 45 || angle >= 315) // sag buton
            return 0;

        else if (angle >= 135 && angle < 225) // sol buton
            return 1;

        else if (angle >= 45 && angle < 135) // yukaridaki buton
            return 2;

        else if (angle >= 225 && angle < 315) // asagidaki buton
            return 3;

        return -1;
    }

    //Buton degistirmek icin belli bir sure bekliyor ve daha sonra buton degistirme islemine basliyor
    public IEnumerator waitToActivate(int indexToActivate)
    {
        isCoroutineRunning = true;
        yield return new WaitForSeconds(0.1f);
        ChangeActiveGameObject(indexToActivate);
        isCoroutineRunning = false;
    }

    //Mevcut butonu deaktive ediyor ve diger butonu aktif etmek icin activatebutton fonksiyonuna yolluyor
    public void ChangeActiveGameObject(int indexToActivate)
    {
        if (indexToActivate != -1 && indexToActivate != gameObjects.IndexOf(currentGameObject))
        {
            currentGameObject.SetActive(false);
        }

        activateButton(indexToActivate);
    }

    //Buton listesinden hangi butonu secicegini ve aktif edicegini bulma fonksiyonu
    public void activateButton(int indexToActivate)
    {
        for (int i = 0; i < gameObjects.Count; i++)
        {
            gameObjects[i].SetActive(i == indexToActivate);
        }
        currentGameObject = gameObjects[indexToActivate];

    }

    //Ctrl birakildiktan sonra karakter prefabi degistirme fonksiyonu
    public void ChangePlayerMode()
    {

        if (upperButton)
        {
            switch (currentGameObject.name)
            {
                case "Right":
                    choosenPortrait.sprite = images[1]; //s�ras�n� ayarlamad�m sonra ayarlar�z 
                    animalName = "Monkey(Clone)";
                    break;

                case "Left":
                    choosenPortrait.sprite = images[2];
                    animalName = "Snake(Clone)";
                    break;

                case "Top":
                    choosenPortrait.sprite = images[3];
                    animalName = "Kangraoo(Clone)";
                    break;

                case "Bottom":
                    choosenPortrait.sprite = images[4];
                    animalName = "Spider(Clone)";
                    break;

                default:
                    choosenPortrait.sprite = images[0];
                    animalName = null;
                    break;
            }
        }
        else
        {
            switch (currentGameObject.name)
            {
                case "Right":
                    animalName = "Cheeta(Clone)";
                    choosenPortrait.sprite = images[5];
                    break;

                case "Left":
                    animalName = "Human(Clone)";
                    choosenPortrait.sprite = images[0];
                    break;

                case "Top":
                    animalName = "Rhino(Clone)";
                    choosenPortrait.sprite = images[6];
                    break;

                case "Bottom":
                    animalName = "Bat(Clone)";
                    choosenPortrait.sprite = images[7];
                    break;

                default:
                    animalName = null;
                    choosenPortrait.sprite = images[0];
                    break;
            }

        }

        if (!Input.GetKeyDown(KeyCode.LeftControl) && animalName != null)
        {
            foreach (GameObject gameObject in Player.instance.createdModeList)
            {
                if (gameObject.name == animalName)
                {
                    gameObject.SetActive(true);
                    continue;
                }
                gameObject.SetActive(false);
            }
        }
    }



    private void switchToOtherButton()
    {
        if (!canSwitch) return;

        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            upperButton = false;
            StartCoroutine(WaitForAnimationToEnd());
            EnableAnim();
            animator.SetBool("FirstDown", true);
            animator2.SetBool("secondUp", true);

            for (int i = 0; i < ModeImagesForButton.Count; i++)
            {
                ModeImagesForButton[i].SetActive(i >= 4);
            }


        }
        else if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            upperButton = true;
            StartCoroutine(WaitForAnimationToEnd());
            EnableAnim();
            animator.SetBool("firstUp", true);
            animator2.SetBool("secondDown", true);

            for (int i = 0; i < ModeImagesForButton.Count; i++)
            {
                ModeImagesForButton[i].SetActive(i < 4);
            }

        }
    }

    private void EnableAnim()
    {
        animator.enabled = true;
        animator2.enabled = true;
    }

    private IEnumerator WaitForAnimationToEnd()
    {
        canSwitch = false;
        yield return new WaitForSecondsRealtime(0.5f);
        canSwitch = true;
    }



}
