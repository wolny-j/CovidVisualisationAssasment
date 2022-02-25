<?php
    function OpenCon()
    {
     $dbhost = "localhost";
     $dbuser = "root";
     $dbpass = "AppDev@2021";
     $db = "covid_db";
     $conn = mysqli_connect($dbhost, $dbuser, $dbpass, $db);
 
     return $conn;
    }
     
    function CloseCon($conn)
    {
     $conn -> close();
    }
     
    function UpdateDB($conn, $countryName, $newCases, $totalCases, $newDeaths, $totalDeaths)
    {
        $country2 = $conn->real_escape_string($countryName);
        //$query = "INSERT INTO Country (CountryName, NewCases, TotalCases, NewDeaths, TotalDeaths) VALUES ('$country2', '66', '66', '66', '66' )";
        $query = "UPDATE Country SET NewCases = '$newCases', TotalCases = '$totalCases', NewDeaths = '$newDeaths', TotalDeaths = '$totalDeaths'  WHERE CountryName = '$country2'";
        $run = mysqli_query($conn, $query) or die('Error: ' . mysqli_error($conn));
        
    } 

    




    ?>