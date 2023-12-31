using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class QuestStartObject : MonoBehaviour
{
    [SerializeField] QuestData _data;

    [SerializeField] GameObject _city, _japan;

    [SerializeField] Cinemachine.CinemachineVirtualCamera _viewCam;

    [SerializeField] Light _light;
    //[SerializeField] LensFlareComponentSRP _lesnFlare;

    private Cinemachine.LensSettings _viewCamSetting;
    private float _time;

    
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if(other.CompareTag("Player"))
        {
            DialogueManager._instance.GetQuestDialogue(_data, _data._dialogueData[0]);
            if (gameObject.CompareTag("Tutorial_1"))
            {
                GameManager._instance.FirstTuto = true;
            }
            else if (gameObject.CompareTag("Tutorial_2"))
            {
                GameManager._instance.SecondTuto = true;
            }

            if(gameObject.CompareTag("ClearFog"))
            {
                GameManager._instance._isLastQuest = true;
                RenderSettings.fog = false;
                _light.color = new Color(0.85f, 0.78f, 0.85f);
                _light.intensity = 1.2f;
                _city.SetActive(false);
                _japan.SetActive(true);

                //_lesnFlare.enabled = true; // 렌즈 플레어 효과 활성화

                _viewCamSetting = _viewCam.m_Lens;
                StartCoroutine(StartFarClipPlaneCo());
            }
            else
                gameObject.SetActive(false);
        }
    }
    IEnumerator StartFarClipPlaneCo()
    {
        float t = 0f;
        while (t < 1f)
        {
            _time += Time.deltaTime;
            t = _time / 5f;

            _viewCamSetting.FarClipPlane = Mathf.Lerp(85f, 1000f, t);

            yield return new WaitForEndOfFrame();
        }
        CameraManager._instance._playerCam.m_Lens.FarClipPlane = 1000f;
        gameObject.SetActive(false);
    }
}
