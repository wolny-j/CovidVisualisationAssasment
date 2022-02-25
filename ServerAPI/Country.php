<?php
class Country
{
    public $CountryName;
    public $NewCases;
    public $TotalCases;
    public $NewDeaths;
    public $TotalDeaths;
    
    //County function constructor
    public function __construct($countryName, $newCases, $totalCases, $newDeaths, $totalDeaths)
    {
        $this->CountryName = $countryName;
        $this->NewCases = $newCases;
        $this->TotalCases = $totalCases;
        $this->NewDeaths = $newDeaths;
        $this->TotalDeaths = $totalDeaths;
    }

       //All getters
    public function getCountryName()
    {
        return $this->CountryName;
    }

    public function getNewCases()
    {
        return $this->NewCases;
    }

    public function getTotalCases()
    {
        return $this->TotalCases;
    }

    public function getNewDeaths()
    {
        return $this->NewDeaths;
    }

    public function getTotalDeaths()
    {
        return $this->TotalDeaths;
    }
}
?>