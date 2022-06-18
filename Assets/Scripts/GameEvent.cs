using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEvent : UnityEvent { }
public class GameEvent<T> : UnityEvent<T> { }