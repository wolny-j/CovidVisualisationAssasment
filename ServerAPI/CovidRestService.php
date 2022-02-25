<?php
    require "dbinfo.php";
    require "RestService.php";
    require "Country.php";

 
class CovidRestService extends RestService 
{
	private $countries;
    
	public function __construct() 
	{
		parent::__construct("countries");
	}

	public function performGet($url, $parameters, $requestBody, $accept) 
	{

				header('Content-Type: application/json; charset=utf-8');
				header('no-cache,no-store');
				$this->getAllCountries();
				echo json_encode($this->countries);
		
	}

	public function performPost($url, $parameters, $requestBody, $accept) 
	{
		global $dbserver, $dbusername, $dbpassword, $dbdatabase;
		$newCountry = $this->extractBookFromJSON($requestBody);
		$connection = new mysqli($dbserver, $dbusername, $dbpassword, $dbdatabase);
		if (!$connection->connect_error)
		{
			$sql = "insert into Country (CountryName, NewCases, TotalCases, NewDeaths, TotalDeaths) values (?, ?, ?, ?, ?)";

			$statement = $connection->prepare($sql);
			$countryName = $newCountry->getCountryName();
			$newCases = $newCountry->getNewCases();
			$totalCases = $newCountry->getTotalCases();
			$newDeaths = $newCountry->getNewDeaths();
			$totalCases = $newCountry->getTotalDeaths();
			$statement->bind_param('siiii', $countryName, $newCases, $totalCases, $newDeaths, $totalDeaths);
			$result = $statement->execute();
			if ($result == FALSE)
			{
				$errorMessage = $statement->error;
			}
			$statement->close();
			$connection->close();
			if ($result == TRUE)
			{
				$this->noContentResponse();
			}
			else
			{
				$this->errorResponse($errorMessage);
			}
		}
	}

	public function performPut($url, $parameters, $requestBody, $accept) 
	{
		global $dbserver, $dbusername, $dbpassword, $dbdatabase;
		echo $requestBody;
		$newCountry = $this->extractCountryFromJSON($requestBody);
		$connection = new mysqli($dbserver, $dbusername, $dbpassword, $dbdatabase);
		if (!$connection->connect_error)
		{
			$sql = "update Country set NewCases = ?, TotalCases = ?, NewDeaths = ?, TotalDeaths = ? where CountryName = ?";
			

			$statement = $connection->prepare($sql);
			$countryName = $newCountry->getCountryName();
			$newCases = $newCountry->getNewCases();
			$totalCases = $newCountry->getTotalCases();
			$newDeaths = $newCountry->getNewDeaths();
			$totalDeaths = $newCountry->getTotalDeaths();
			//$sql = "UPDATE Country SET NewCases = '$newCases', TotalCases = '$totalCases', NewDeaths = '$newDeaths', TotalDeaths = '$totalDeaths'  WHERE CountryName = '$countryName'";
			$statement->bind_param('iiiis', $newCases, $totalCases, $newDeaths, $totalDeaths, $countryName);
			$result = $statement->execute();
			//$run = mysqli_query($connection, $sql) or die('Error: ' . mysqli_error($conn));
			if ($result == FALSE)
			{
				$errorMessage = $statement->error;
			}
			$statement->close();
			$connection->close();
			if ($result == TRUE)
			{
				$this->noContentResponse();				
			}
			else
			{
				$this->errorResponse($errorMessage);
			}
		}
	}

	public function performDelete($url, $parameters, $requestBody, $accept) 
    {
		global $dbserver, $dbusername, $dbpassword, $dbdatabase;
		
		if (count($parameters) == 2)
		{
			$connection = new mysqli($dbserver, $dbusername, $dbpassword, $dbdatabase);
			if (!$connection->connect_error)
			{
				$countryName = $parameters[1];
				$sql = "delete from Country where CountryName = ?";
				$statement = $connection->prepare($sql);
				$statement->bind_param('s', $countryName);
				$result = $statement->execute();
				if ($result == FALSE)
				{
					$errorMessage = $statement->error;
				}
				$statement->close();
				$connection->close();
				if ($result == TRUE)
				{

					$this->noContentResponse();
				}
				else
				{
					$this->errorResponse($errorMessage);
				}
			}
		}
    }

    private function getAllCountries()
    {
		global $dbserver, $dbusername, $dbpassword, $dbdatabase;
	
		$connection = new mysqli($dbserver, $dbusername, $dbpassword, $dbdatabase);
		if (!$connection->connect_error)
		{
			$query = "SELECT * FROM Country";
			if ($result = $connection->query($query))
			{
				while($row = $result->fetch_assoc())
                {
                    $this->countries[] = new Country($row["CountryName"], $row["NewCases"], $row["TotalCases"], $row["NewDeaths"], $row["TotalDeaths"]);
                }
				$result->close();
			}
			$connection->close();
		}
	}	 

    private function extractCountryFromJSON($requestBody)
    {
		// This function is needed because of the perculiar way json_decode works. 
		// By default, it will decode an object into a object of type stdClass.  There is no
		// way in PHP of casting a stdClass object to another object type.  So we use the
		// approach of decoding the JSON into an associative array (that's what the second
		// parameter set to true means in the call to json_decode). Then we create a new
		// Book object using the elements of the associative array.  Note that we are not
		// doing any error checking here to ensure that all of the items needed to create a new
		// book object are provided in the JSON - we really should be.
		$countryArray = json_decode($requestBody, true);
		$country = new Country($countryArray['CountryName'],
						 $countryArray['NewCases'],
						 $countryArray['TotalCases'],
						 $countryArray['NewDeaths'],
						 $countryArray['TotalDeaths']);
		unset($countryArray);
		return $country;
	}
}
?>
