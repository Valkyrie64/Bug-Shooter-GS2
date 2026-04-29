using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using UnityEditor;

public class RebindUI : MonoBehaviour
{

    [SerializeField] private InputActionReference inputActionReference;

    [SerializeField] private bool excludeMouse = true;
    [Range(0, 15)]
    [SerializeField] private int selectedBinding;
    [SerializeField] private InputBinding.DisplayStringOptions displayStringOptions;
    [Header("Binding Info - DO NOT EDIT")]
    [SerializeField] private InputBinding inputBinding;
    private int bindingIndex;

    private string actionName;

    [Header("UI Fields")]
    
    public bool overrideAction;
    public string customActionText;
    [SerializeField] private TMP_Text actionText;
    [SerializeField] private Button rebindButton;
    public TMP_Text rebindText;
    [SerializeField] private Button resetButton;
    
    
    
    
    private void Awake()
    {
        UpdateBindings();
    }
    private void UpdateActionLabel()
    {
        if (actionText!=null)
        {
            if (overrideAction)
            {
                actionText.text = customActionText;
            }
            else
            {
                actionText.text = actionName;
            }
        }
    }

    public void OnReMenu()
    {
        if (inputActionReference != null) 
        {
            ConnorInputManager.LoadBindingOverride(actionName);
            GetBindingInfo();
            UpdateUI();
        }
    }

    private void OnEnable()
    {
        rebindButton.onClick.AddListener(() => DoRebind());
        resetButton.onClick.AddListener(() => ResetBinding());

        if (inputActionReference != null) 
        {
            ConnorInputManager.LoadBindingOverride(actionName);
            GetBindingInfo();
            UpdateUI();
        }

        ConnorInputManager.rebindComplete += UpdateUI;
        ConnorInputManager.rebindCancelled+= UpdateUI;
    }

    private void OnDisable()
    {
        ConnorInputManager.rebindComplete -= UpdateUI;
        ConnorInputManager.rebindCancelled-= UpdateUI;
    }


    private void OnValidate()
    {
        UpdateBindings();
    }

    public void UpdateBindings()
    {
        if (inputActionReference == null)
            return;
        GetBindingInfo();
        UpdateUI();
        UpdateActionLabel();
    }

    private void GetBindingInfo()
    {
        if(inputActionReference.action!= null)
        {
            actionName = inputActionReference.action.name;
        }
        if (inputActionReference.action.bindings.Count>selectedBinding)
        {
            inputBinding = inputActionReference.action.bindings[selectedBinding];
            bindingIndex = selectedBinding;
        }
    }
    public void UpdateUI()
    {
        
        UpdateActionLabel();
        if (rebindText!=null)
        {
            if (Application.isPlaying)
            {
                rebindText.text = ConnorInputManager.GetBindingName(actionName, bindingIndex);
                //rebindText.GetComponent<ChangeSpriteText>().UpdateTextIcons(1,true);
                //ReplaceButtons replaceButtons = FindObjectOfType<ReplaceButtons>();
                PlayerInputActions _PlayerInput = new PlayerInputActions();
                InputAction action = _PlayerInput.asset.FindAction(actionName);
                //Debug.Log(action);
                //if (replaceButtons != null)
                {
                    //replaceButtons.OnUpdateBindingDisplay(this, InputManager.GetBindingName(actionName, bindingIndex));
                }
            }
            else
            {
                rebindText.text = inputActionReference.action.GetBindingDisplayString(bindingIndex);
                //ReplaceButtons replaceButtons = FindObjectOfType<ReplaceButtons>();
                PlayerInputActions _PlayerInput = new PlayerInputActions();
                InputAction action = _PlayerInput.asset.FindAction(actionName);
                //if (replaceButtons != null)
                {
                   //replaceButtons.OnUpdateBindingDisplay(this, action.bindings[bindingIndex].path);
                }
            }
        }
    }

    private void DoRebind()
    {
        ConnorInputManager.StartRebind(actionName, bindingIndex, rebindText, excludeMouse);
    }

    private void ResetBinding()
    {
        ConnorInputManager.ResetBinding(actionName, bindingIndex);
        UpdateUI();
    }
}
