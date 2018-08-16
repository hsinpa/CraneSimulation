using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 
    建築物功能組件，建造、防禦、平台功能
    建築連接點
    建築物座標
    連接關係
    建築物建造
    脫離連接獨立功能
    
     */
namespace Building
{
    public class BuilingMain :GameUnit
    {
        int owner;
        public int weight;
        public int[][] area;
        public ICore core;

        List<LinkPoint> linkPoints;//總連接口

        List<LinkPoint> AvailablePoints;//可用連接口





    }	
}
