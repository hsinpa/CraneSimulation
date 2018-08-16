using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace controlpanel
{
    /// <summary>
    /// 分類用無功能.
    /// </summary>
    public interface IUIInterface
    {


    }

    public  interface IOperating
    {
        void Enter(ControlMain controlMain);

        void Exit();

        void Confirm();

        void Cancel();

        void ConUpdate();

   
    }

}
