import API from "../../Util/API";

//Autenticação
export const GetWeather = () =>
  API.get("/WeatherForecast");