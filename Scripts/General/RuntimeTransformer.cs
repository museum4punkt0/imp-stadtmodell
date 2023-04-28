using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RuntimeTransformer : MonoBehaviour
{
    public List<Transform> Objects;
    public static RuntimeTransformer Instance;
    public Transform targetTransform;
    public TransformerMode mode;
    public Slider xSlider;
    public Slider ySlider;
    public Slider zSlider;
    public Text xLabel;
    public Text yLabel;
    public Text zLabel;
    public Text targetLabel;
    public Text toggleModeButtonText;
    public bool uniformScale;
    public bool ignoreSet;

    public void Next()
    {
        var index = Objects.IndexOf(targetTransform);
        index++;
        if (index >= Objects.Count) index = 0;
        targetTransform = Objects[index];
        targetLabel.text = targetTransform.gameObject.name;
        RefreshUI();    
    }

    public void SetUniform(bool b)
    {
        uniformScale = b;
    }
    

    public void ToggleMode()
    {
        switch (mode)
        {
            case TransformerMode.Pos:
                mode = TransformerMode.Rot;
                break;
            case TransformerMode.Rot:
                mode = TransformerMode.Sca;
                break;
            case TransformerMode.Sca:
                mode = TransformerMode.Pos;
                break;
        }
        toggleModeButtonText.text = mode + "";
        RefreshUI();
    }

    public void SetX(float f)
    {
        if (!ignoreSet)
        {
            xLabel.text = f.ToString("F4");

            switch (mode)
            {
                case TransformerMode.Pos:
                    targetTransform.localPosition = new Vector3(f, targetTransform.localPosition.y, targetTransform.localPosition.z);
                    break;
                case TransformerMode.Rot:
                    targetTransform.localRotation = Quaternion.Euler(f, ySlider.value, zSlider.value);
                    break;
                case TransformerMode.Sca:
                    if (uniformScale)
                        targetTransform.localScale = new Vector3(f, f / targetTransform.localScale.x * targetTransform.localScale.y, f / targetTransform.localScale.x * targetTransform.localScale.z);
                    else
                        targetTransform.localScale = new Vector3(f, targetTransform.localScale.y, targetTransform.localScale.z);
                    break;
            }
        }
    }

    public void SetY(float f)
    {
        if (!ignoreSet)
        { 
        yLabel.text = f.ToString("F4");

        switch (mode)
        {
            case TransformerMode.Pos:
                targetTransform.localPosition = new Vector3(targetTransform.localPosition.x, f, targetTransform.localPosition.z);
                break;
            case TransformerMode.Rot:
                targetTransform.localRotation = Quaternion.Euler(xSlider.value, f, zSlider.value);
                break;
            case TransformerMode.Sca:
                targetTransform.localScale = new Vector3( targetTransform.localScale.x, f, targetTransform.localScale.z);
                break;
        }
        }
    }

    public void SetZ(float f)
    {
        if (!ignoreSet)
        {

            zLabel.text = f.ToString("F4");

        switch (mode)
        {
            case TransformerMode.Pos:
                targetTransform.localPosition = new Vector3(targetTransform.localPosition.x, targetTransform.localPosition.y, f);
                break;
            case TransformerMode.Rot:
                targetTransform.localRotation = Quaternion.Euler(xSlider.value, ySlider.value, f);
                break;
            case TransformerMode.Sca:
                targetTransform.localScale = new Vector3(targetTransform.localScale.x, targetTransform.localScale.y, f);
                break;
            }
        }
    }

    public void RefreshUI()
    {
        ignoreSet = true;
        switch (mode)
        {
            case TransformerMode.Pos:
                var posLimit = (targetTransform.parent != null) ? targetTransform.parent.InverseTransformVector(Vector3.one).magnitude*0.4f : 0.4f;
                xSlider.minValue = targetTransform.localPosition.x - posLimit;
                xSlider.maxValue = targetTransform.localPosition.x + posLimit;
                xSlider.value = targetTransform.localPosition.x;

                ySlider.minValue = targetTransform.localPosition.y - posLimit;
                ySlider.maxValue = targetTransform.localPosition.y + posLimit;
                ySlider.value = targetTransform.localPosition.y;

                zSlider.minValue = targetTransform.localPosition.z - posLimit;
                zSlider.maxValue = targetTransform.localPosition.z + posLimit;
                zSlider.value = targetTransform.localPosition.z;

                xLabel.text = targetTransform.localPosition.x.ToString("F4") ;
                yLabel.text = targetTransform.localPosition.y.ToString("F4");
                zLabel.text = targetTransform.localPosition.z.ToString("F4");

                break;
            case TransformerMode.Rot:

                var angleLimit = 90;
                xSlider.minValue = targetTransform.localRotation.eulerAngles.x-angleLimit;
                xSlider.maxValue = targetTransform.localRotation.eulerAngles.x + angleLimit;
                xSlider.value = targetTransform.localRotation.eulerAngles.x;

                ySlider.minValue = targetTransform.localRotation.eulerAngles.y - angleLimit;
                ySlider.maxValue = targetTransform.localRotation.eulerAngles.y + angleLimit;
                ySlider.value = targetTransform.localRotation.eulerAngles.y;

                zSlider.minValue = targetTransform.localRotation.eulerAngles.z - angleLimit;
                zSlider.maxValue = targetTransform.localRotation.eulerAngles.z + angleLimit;
                zSlider.value = targetTransform.localRotation.eulerAngles.z;
                xLabel.text = targetTransform.localRotation.eulerAngles.x.ToString("F3");
                yLabel.text = targetTransform.localRotation.eulerAngles.y.ToString("F3");
                zLabel.text = targetTransform.localRotation.eulerAngles.z.ToString("F3");
                break;
            case TransformerMode.Sca:
                var scaLimit = targetTransform.localScale.x * 0.2f;
                xSlider.minValue = targetTransform.localScale.x - scaLimit;
                xSlider.maxValue = targetTransform.localScale.x + scaLimit;
                xSlider.value = targetTransform.localScale.x;

                ySlider.minValue = targetTransform.localScale.y - scaLimit;
                ySlider.maxValue = targetTransform.localScale.y + scaLimit;
                ySlider.value = targetTransform.localScale.y;

                zSlider.minValue = targetTransform.localScale.z - scaLimit;
                zSlider.maxValue = targetTransform.localScale.z + scaLimit;
                zSlider.value = targetTransform.localScale.z;

                xLabel.text = targetTransform.localScale.x.ToString("F3");
                yLabel.text = targetTransform.localScale.y.ToString("F3");
                zLabel.text = targetTransform.localScale.z.ToString("F3");
                break;
        }
        ignoreSet = false;
    }

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public enum TransformerMode
    {
        Pos,
        Rot,
        Sca
    }
}

