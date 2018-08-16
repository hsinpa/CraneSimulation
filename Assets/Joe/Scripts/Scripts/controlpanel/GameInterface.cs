using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using controlpanel;
using Building;

public interface GameInterface {

	
}

public interface IUnit
{
    void Enter(ControlMain controlMain);

    void Exit();
}

/// <summary>
/// 可移動.
/// </summary>
public interface IMove
{

}
