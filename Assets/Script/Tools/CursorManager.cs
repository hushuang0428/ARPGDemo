using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;




public class CursorManager : Singleton<CursorManager>
{
    List<Cursor> cursors;

}




public class Cursor
{
    private int cursorId; 
    private string cursorName;

    public Cursor(int ID,string name)
    {
        this.cursorId = ID;
        this.cursorName = name;
    }

}
