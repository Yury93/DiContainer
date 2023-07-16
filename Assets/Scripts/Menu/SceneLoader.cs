using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


    public class SceneLoader : MonoBehaviour
    {
    public enum SceneName { Main, Gallery , ViewPic }
        [SerializeField] private Canvas canvas;
        [SerializeField] private GameObject progressBarGO;
        [SerializeField] private Image progressImage, backgroundImage;
        [SerializeField] private TextMeshProUGUI percentText;
        [SerializeField] private int delayLoad = 3;
        [SerializeField] private float fadeDuration = 2f;
        [SerializeField] private SceneName sceneName;    
        private float percentLoading;
       
        private bool loadProcess;
        public static SceneLoader Instance;

        public void Init()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Destroy(Instance.gameObject);
            }
        }

        public async void LoadScene(SceneName sceneName)
        {
            if (loadProcess == true) { return; }

            canvas.enabled = true;
            this.sceneName = sceneName;
            SetActiveBar(true);
            progressImage.fillAmount = 0;
            backgroundImage.color = Color.black;
            await LoadProcess();
        }

        private void SetActiveBar(bool show)
        {
            progressBarGO.SetActive(show);
            percentText.enabled = show;
        }
        private async Task LoadProcess()
        {
            try
            {
                loadProcess = true;
                float progress = 0.1f;
                var operation = SceneManager.LoadSceneAsync(sceneName.ToString());
                operation.allowSceneActivation = false;
                while (percentLoading <= 99)
                {
                    await Task.Delay(100);
                    progress += 0.1f;
                    progressImage.fillAmount = progress / delayLoad;

                    percentLoading = Mathf.RoundToInt((((float)delayLoad / 100) * progress) * 1000);
                    percentText.text = $"Загрузка: {percentLoading}%";
                    if (percentLoading >= 100)
                    {
                        percentLoading = 100;
                        percentText.text = $"Загрузка: {percentLoading}%";

                        if (operation.progress >= 0.9f)
                        {
                            await Task.Delay(50);
                            percentText.text = $"Загрузка завершена!";
                            await Task.Delay(100);
                            operation.allowSceneActivation = true;
                            await Disable();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
        public static void Load(SceneName sceneName)
        {
            SceneManager.LoadSceneAsync(sceneName.ToString());
        }
        private async Task Disable()
        {
            float alphaChannel = 1f;
            Color col = backgroundImage.color;
            float startTime = Time.realtimeSinceStartup;
            SetActiveBar(false);
            while (alphaChannel > 0.01f)
            {
                float t = (Time.realtimeSinceStartup - startTime) / fadeDuration;
                alphaChannel = Mathf.MoveTowards(col.a, 0f, t);
                backgroundImage.color = new Color(col.r, col.g, col.b, alphaChannel);
                await Task.Yield();
            }

            canvas.enabled = false;
            loadProcess = false;
        }
    }
