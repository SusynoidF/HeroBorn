using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public interface Imanager
 {
     
     string State { get; set; }
   
     void Initialize();
 }
 public abstract class BaseManager
 {
    protected string _state;
     public abstract string state { get; set; }
     // 3
     public abstract void Initialize();
 }
  public class CombatManager: BaseManager
 {
     // 2
     public override string state
     { get { return _state; }
         set { _state = value; }
     }
     // 3
     public override void Initialize()
     {
         _state = "Manager initialized..";
         Debug.Log(_state);
     }
 }
 