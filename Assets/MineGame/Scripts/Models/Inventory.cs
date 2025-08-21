using System;
using System.Collections.Generic;

[Serializable]
public class Inventory
{
    public List<AToy> Listitem;

    public List<AToy> collections = new();
    public List<BankaObject> bankaObjects = new();

}
