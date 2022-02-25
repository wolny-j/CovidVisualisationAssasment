using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Country //Class containing all info about Country
{
    string _countryName;
    int _newCases;
    int _allCases;
    int _newDeaths;
    int _allDeaths;
    int _Xcords;
    int _Ycords;


    //Constructors
    public Country()
    {
        _countryName = "";
        _newCases = 0;
        _allCases = 0;
        _newDeaths = 0;
        _allDeaths = 0;
        _Xcords = 0;
        _Ycords = 0;
    }

    public Country(string countryName, int newCases, int allCases, int newDeaths, int allDeaths)
    {
        _countryName = countryName;
        _newCases = newCases;
        _allCases = allCases;
        _newDeaths = newDeaths;
        _allDeaths = allDeaths;
    }

    public Country(string countryName, int newCases, int allCases, int newDeaths, int allDeaths, int XCords, int YCords)
    {
        _countryName = countryName;
        _newCases = newCases;
        _allCases = allCases;
        _newDeaths = newDeaths;
        _allDeaths = allDeaths;
        _Xcords = XCords;
        _Ycords = YCords;
    }

    //Getters and Setters
    public string GetName()
    {
        return _countryName;
    }
    public void SetName(string countryName)
    {
        _countryName = countryName;
    }
    public int GetNewCases()
    {
        return _newCases;
    }
    public void SetNewCases(int newCases)
    {
        _newCases = newCases;
    }

    public int GetAllCases()
    {
        return _allCases;
    }
    public void SetAllCases(int allCases)
    {
        _allCases = allCases;
    }
    public int GetNewDeaths()
    {
        return _newDeaths;
    }
    public void SetNewDeaths(int newDeaths)
    {
        _newDeaths = newDeaths;
    }

    public int GetAllDeaths()
    {
        return _allDeaths;
    }
    public void SetAllDeaths (int allDeaths)
    {
        _allDeaths = allDeaths;
    }
    public int GetX()
    {
        return _Xcords;
    }
    public int GetY()
    {
        return _Ycords;
    }
}
