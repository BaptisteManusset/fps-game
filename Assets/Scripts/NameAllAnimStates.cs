using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;
using NaughtyAttributes;

public class NameAllAnimStates : MonoBehaviour
{
  #region déclaration de variable necessaire pour les fonctions
  public Dictionary<int, string> AnimStateNames;
  AnimatorControllerLayer[] acLayers;
  ChildAnimatorState[] ch_animStates;

  Animator animator;
  AnimatorController ac;
  AnimatorStateMachine stateMachine;
  int k = 0;
  #endregion

  public int hashTest = 0; // variable utiliser par  GetStateNameTest

  void Awake()
  {
    animator = GetComponent<Animator>();
    GetAnimatorState(animator);
  }

  // Genere la liste des states disponibles dans un animator passé en parametre
  public void GetAnimatorState(Animator animator)
  {
    AnimStateNames = new Dictionary<int, string>();
    ac = animator.runtimeAnimatorController as AnimatorController;

    Debug.Log(string.Format("Nombres de Layer: {0}", animator.layerCount));

    for (int i = 0; i < animator.layerCount; i++)
    {
      Debug.Log(string.Format("Layer {0}: {1}", i, animator.GetLayerName(i)));

    }
    acLayers = ac.layers;

    foreach (AnimatorControllerLayer i in acLayers) //for each layer
    {
      Debug.Log("Layer : " + i.name);
      stateMachine = i.stateMachine;
      ch_animStates = stateMachine.states;
      foreach (ChildAnimatorState j in ch_animStates) //for each state
      {
        AnimStateNames.Add(
          j.state.nameHash,
          j.state.name
        );
        k++;
        Debug.Log($"<color=red>Added <b>{i.name} > {j.state.name} > {j.state.nameHash}</b></color>");
      }
    }
    Debug.Log($" {k} states dans {animator.layerCount} layers ont été ajouté à la liste");


  }


  // recupere le nom du state correspond au hash donnée en parametre en utilisant le tableau crée par GetAnimatorState
  string GetStateName(int hash)
  {
    if (AnimStateNames.Count > 0)
    {
      if (AnimStateNames.ContainsKey(hash))
      {
        return AnimStateNames[hash];
      }
    }
    return null;
  }


  #region fonction d'exemple ou permettant de debugger

  //affiche la liste des correspondances entre le hash et le nom de la state
  [Button]
  void DisplayDictionary()
  {
    foreach (KeyValuePair<int, string> attachStat in AnimStateNames)
    {
      Debug.Log("State Hash <b>" + attachStat.Key + "</b> correspond à <b>" + attachStat.Value + "</b>");
    }
  }


  //Exemple d'utilisation de GetStateName
  [Button]
  void GetStateNameTest()
  {
    var value = GetStateName(hashTest);

    if (value != null)
    {
      Debug.Log("L'animation state est <b>" + value + "</b> est posséde l'id <b>" + hashTest + "</b> ");
    }
    else
    {
      Debug.Log("Aucune Animation state ne correspond à l'id <b>" + hashTest + "</b> ");
    }
  }
  #endregion


}


